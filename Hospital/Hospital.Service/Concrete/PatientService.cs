namespace Hospital.Service.Concrete
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Hospital.Model.Entities;
    using Hospital.Model.Identity;
    using Hospital.Repository.Abstract;
    using Hospital.Service.Abstract;
    using Hospital.Service.InDTOs;
    using Hospital.Service.OutDTOs;
    using Hospital.Service.OutDTOs.Prescriptions;
    using Hospital.Service.OutDTOs.Referrals;

    public class PatientService : IPatientService
    {
        private IUserRepository _userRepository;
        private IRepository<Patient> _patientRepository;
        private IMapper _mapper;

        public PatientService(IMapper mapper,
                              IUserRepository userRepository,
                              IRepository<Patient> patientRepository)
        {
            _userRepository = userRepository;
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<bool> Register(RegisterPatientInDTO model)
        {
            if (model == null)
            {
                return false;
            }

            var appUser = _mapper.Map<ApplicationUser>(model);
            
            var user = await _userRepository.CreateAsync(appUser, model.SystemRole, model.Password);

            if (user == null)
            {
                return false;
            }

            var patient = new Patient { User = user };

            await _patientRepository.InsertAsync(patient);

            return true;
        }

        public async Task<bool> ConfirmEmail(ConfirmEmailPatientInDTO model)
        {      
            if (model.user == null)
                return false;

            return await _userRepository.ConfirmEmailAsync(model.user, model.token);
        }

        public async Task<PatientOutDTO> GetPatientById(long id)
        {
            var user = await _patientRepository.GetAsync(x => x.User, x => x.Id == id);
            var result = new PatientOutDTO()
            {
                FirstName = user.FirstOrDefault().FirstName,
                LastName = user.FirstOrDefault().LastName,
                Birth = user.FirstOrDefault().DateOfBirth,
                UserID = id
            };
            return result;
        }

        public async Task<PrescriptionsOutDTO> GetPrescriptions(string userId)
        {
            var result = new PrescriptionsOutDTO();

            var patients = await _patientRepository.GetAsync(
                select: x => x,
                filter: x => x.UserId.Equals(userId),
                includes: x => x.Include(y => y.Visits).ThenInclude(y => y.Prescription)
                                .Include(y => y.Visits).ThenInclude(y => y.Doctor).ThenInclude(y => y.User));

            var patient = patients.FirstOrDefault();

            foreach (var visit in patient.Visits)
            {
                var prescription = visit.Prescription;

                if (prescription != null)
                {
                    var prescriptionToAdd = new PrescriptionOutDTO
                    {
                        Comments = prescription.Comments,
                        DoctorName = $"{visit.Doctor.User.FirstName} {visit.Doctor.User.LastName}",
                        DueDate = prescription.DueDate,
                    };

                    result.Prescriptions.Add(prescriptionToAdd);
                }
            }

            result.Prescriptions = result.Prescriptions.OrderByDescending(x => x.DueDate).ToList();

            return result;
        }

        public async Task<ReferralsOutDTO> GetReferrals(string userId)
        {
            var result = new ReferralsOutDTO();

            var users = await _patientRepository.GetAsync(
                x => x,
                filter: x => x.UserId.Equals(userId),
                includes: x => x.Include(y => y.Visits).ThenInclude(y => y.Referral).ThenInclude(y => y.Specialization)
                                .Include(y => y.Visits).ThenInclude(y => y.Doctor).ThenInclude(y => y.User)
                                .Include(y => y.Visits).ThenInclude(y => y.Doctor).ThenInclude(y => y.Specialization));

            var user = users.FirstOrDefault();

            if (user == null)
            {
                return result;
            }

            foreach (var visit in user.Visits)
            {
                var referral = visit.Referral;

                if (referral != null)
                {
                    var referralToAdd = new ReferralOutDTO
                    {
                        Description = referral.Description,
                        DoctorName = $"{visit.Doctor.User.FirstName} {visit.Doctor.User.LastName}",
                        DoctorSpecialization = visit.Doctor.Specialization.Name,
                        SpecializationName = referral.Specialization.Name,
                        ValidityTerm = referral.ExpiryDate
                    };

                    if (referral.ExpiryDate <= DateTime.UtcNow)
                    {
                        result.OverdueReferrals.Add(referralToAdd);
                    }
                    else
                    {
                        result.PresentReferrals.Add(referralToAdd);
                    }
                }
            }

            result.OverdueReferrals = result.OverdueReferrals.OrderByDescending(x => x.ValidityTerm).ToList();
            result.PresentReferrals = result.PresentReferrals.OrderByDescending(x => x.ValidityTerm).ToList();

            return result;
        }
    }
}