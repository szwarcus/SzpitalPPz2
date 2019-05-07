namespace Hospital.Service.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Hospital.Core.Helpers;
    using Hospital.Model.Entities;
    using Hospital.Repository.Abstract;
    using Hospital.Service.Abstract;
    using Hospital.Service.InDTOs;
    using Hospital.Service.OutDTOs;
    using Microsoft.EntityFrameworkCore;

    public class VisitService : IVisitService
    {
        private IMapper _mapper;

        private IRepository<Visit> _visitRepository;
        private IRepository<Patient> _patientRepository;
        private IRepository<Doctor> _doctorRepository;
        private IRepository<Medicament> _medicamentRepository;
        private IRepository<Nurse> _nurseRepository;

        public VisitService(IMapper mapper,
                            IRepository<Visit> visitRepository,
                            IRepository<Patient> patientRepository,
                            IRepository<Doctor> doctorRepository,
                            IRepository<Nurse> nurseRepository,
                            IRepository<Medicament> medicamentRepository)
        {
            _visitRepository = visitRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _nurseRepository = nurseRepository;
            _medicamentRepository = medicamentRepository;
            _mapper = mapper;
        }

        public async Task<PastAndNextVisitsOutDTO> GetBaseInfoVisitsInPastAndNextDaysAsync(string userId)
        {
            PastAndNextVisitsOutDTO result = null;
            var days = 30;
            var minDay = DateTime.UtcNow.AddDays(days * (-1));
            var maxDay = DateTime.UtcNow.AddDays(days);

            if (userId == null)
            {
                return result;
            }

            var patientList = await _patientRepository.GetAsync<Patient>(x => x,
                                                                         filter: x => x.UserId == userId);
            if (patientList.Count == 0)
            {
                return result;
            }

            result = new PastAndNextVisitsOutDTO();
            var patient = patientList.First();
            var visits = await _visitRepository.GetAllAsync<VisitOutDTO>(x => new VisitOutDTO
            {
                DoctorName = $"{x.Doctor.User.FirstName} {x.Doctor.User.LastName}",
                Date = x.Date,
                Specialization = x.Doctor.Specialization.Name,
                Description = x.Description
            },
                                                                         filter: x => x.PatientId == patient.Id
                                                                                      && (x.Date > minDay && x.Date < maxDay),
                                                                         includes: x => x.Include(y => y.Doctor).ThenInclude(y => y.User)
                                                                                         .Include(y => y.Doctor).ThenInclude(y => y.Specialization)
                                                                         );

            visits.ForEach(visit =>
            {
                if (visit.Date > DateTime.UtcNow)
                {
                    result.UpcomingVisits.Add(visit);
                }
                else
                {
                    result.RealizedVisits.Add(visit);
                }
            });

            result.UpcomingVisits = result.UpcomingVisits.OrderBy(visit => visit.Date).ToList();
            result.RealizedVisits = result.RealizedVisits.OrderByDescending(visit => visit.Date).ToList();

            return result;
        }

        public async Task<bool> ArrangeVisit(ArrangeVisitInDTO model)
        {
            if (model == null || model.Date <= DateTime.UtcNow)
            {
                return false;
            }

            var patientList = await _patientRepository.GetAsync<Patient>(x => x,
                                                                         filter: x => x.UserId == model.PatientUserId);
            if (patientList.Count == 0)
            {
                return false;
            }

            var doctorList = await _doctorRepository.GetAsync<Doctor>(x => x,
                                                                      filter: x => x.Id == model.DoctorId);
            if (doctorList.Count == 0)
            {
                return false;
            }

            var hour = new TimeSpan(model.Date.Hour, model.Date.Minute, model.Date.Second);
            var numberInDay = VisitDateTimeHelper.DateTimeToVisitNumberInDay(hour);

            var visit = new Visit
            {
                PatientId = patientList[0].Id,
                DoctorId = doctorList[0].Id,
                Date = model.Date,
                Description = model.Description,
                NumberInDay = numberInDay,
                State = Core.Enums.StateVisit.Created
                //Prescription = new Prescription { } // to do
            };

            await _visitRepository.InsertAsync(visit);

            return true;
        }

        public async Task UpdateVisit(UpdateVisitInDTO model)
        {
            var visits = await _visitRepository.GetAsync(x => x, x => x.Id == model.Id);
            var visit = visits.FirstOrDefault();
            visit.Description = model.Description;
            visit.State = model.State;

            await _visitRepository.UpdateAsync(visit);
        }

        public async Task<Visit> GetById(long Id)
        {
            var visits = await _visitRepository.GetAsync(x => x, x => x.Id == Id, null, x => x.Include(z => z.Prescription));
            return visits.FirstOrDefault();
        }

        public async Task<List<VisitOutDTO>> GetCompletedVisits(long userId)
        {
            var visits = await _visitRepository.GetAllAsync(x => new VisitOutDTO
            {
                DoctorName = $"{x.Doctor.User.FirstName} {x.Doctor.User.LastName}",
                Date = x.Date,
                Specialization = x.Doctor.Specialization.Name,
                Description = x.Description
            }, x => x.PatientId == userId, null, x => x.Include(y => y.Doctor).ThenInclude(y => y.User)
                                                                                         .Include(y => y.Doctor).ThenInclude(y => y.Specialization));

            return visits;
        }

        public async Task<VisitDetailsOutDTO> GetVisitDetails(long visitId, DateTime date)
        {
            var visits = await _visitRepository.GetAllAsync(x => x, x => x.PatientId == visitId && x.Date == date, null, x => x.Include(y => y.Patient).ThenInclude(y => y.User).Include(x1 => x1.Prescription).ThenInclude(x1 => x1.PrescriptionMedicaments));
            var visit = visits.FirstOrDefault();

            List<string> medicamens = new List<string>();
            foreach (var item in visit.Prescription.PrescriptionMedicaments)
            {
                var med = await _medicamentRepository.GetAsync(x => x.Name, x => x.Id == item.MedicamentId);
                medicamens.Add(med.FirstOrDefault());
            }
            var result = new VisitDetailsOutDTO()
            {
                PatientFirstName = visit.Patient.User.FirstName,
                PatientLastName = visit.Patient.User.LastName,
                VisitDescription = visit.Description,
                Medicaments = medicamens,
                Dosage = visit.Prescription.Comments
                
            };
            return result;
        }
    }
}