using System.Threading.Tasks;
using AutoMapper;
using Hospital.Model.Entities;
using Hospital.Model.Identity;
using Hospital.Repository.Abstract;
using Hospital.Service.PatientServices.Abstract;
using Hospital.Service.PatientServices.InDTOs;

namespace Hospital.Service.PatientServices.Concrete
{
    public class PatientAccountService : IPatientAccountService
    {
        private IUserRepository _userRepository;
        private IRepository<Patient> _patientRepository;
        private IMapper _mapper;

        public PatientAccountService(IMapper mapper,
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

            await _patientRepository.Insert(patient);

            return true;
        }


        public async Task<bool> ConfirmEmail(ConfirmEmailPatientInDTO model)
        {      
            if (model.user == null)
                return false;

            return await _userRepository.ConfirmEmailAsync(model.user, model.token);
        }

    }
}