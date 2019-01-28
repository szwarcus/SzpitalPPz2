using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.Core.Helpers;
using Hospital.Model.Entities;
using Hospital.Repository.Abstract;
using Hospital.Service.Abstract;
using Hospital.Service.InDTOs;

namespace Hospital.Service.Concrete
{
    public class VisitService : IVisitService
    {
        private IMapper _mapper;

        private IRepository<Visit> _visitRepository;
        private IRepository<Patient> _patientRepository;
        private IRepository<Doctor> _doctorRepository;
        private IRepository<Nurse> _nurseRepository;

        public VisitService(IMapper mapper,
                            IRepository<Visit> visitRepository,
                            IRepository<Patient> patientRepository,
                            IRepository<Doctor> doctorRepository,
                            IRepository<Nurse> nurseRepository)
        {
            _visitRepository = visitRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _nurseRepository = nurseRepository;
            _mapper = mapper;
        }

        public async Task<bool> ArrangeVisit(ArrangeVisitInDTO model)
        { 
            if (model == null || model.Date <= DateTime.UtcNow) return false;

            var patientList = await _patientRepository.GetAsync<Patient>(x => x, 
                                                                         filter: x => x.UserId == model.PatientUserId);
            if (patientList.Count == 0) return false;

            var doctorList = await _doctorRepository.GetAsync<Doctor>(x => x, 
                                                                      filter: x => x.Id == model.DoctorId);
            if (doctorList.Count == 0) return false;

            var hour = new TimeSpan(model.Date.Hour, model.Date.Minute, model.Date.Second);
            var numberInDay = VisitDateTimeHelper.DateTimeToVisitNumberInDay(hour);

            var visit = new Visit
            {
                PatientId = patientList[0].Id,
                DoctorId = doctorList[0].Id,
                Date = model.Date,
                Description = model.Description,
                NumberInDay = numberInDay
                //Prescription = new Prescription { } // to do
            };

            await _visitRepository.InsertAsync(visit);

            return true;
        }
    }
}
