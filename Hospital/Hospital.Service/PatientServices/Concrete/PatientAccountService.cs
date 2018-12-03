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
        private IUserRepository userRepository;
        private IRepository<Patient> patientRepository;
        private IMapper mapper;

        public PatientAccountService(IMapper mapper,
                                     IUserRepository userRepository,
                                     IRepository<Patient> patientRepository)
        {
            this.userRepository = userRepository;
            this.patientRepository = patientRepository;
            this.mapper = mapper;
        }

        public async Task<bool> Register(RegisterPatientInDTO model)
        {
            if (model == null)
            {
                return false;
            }

            var appUser = mapper.Map<ApplicationUser>(model);
            
            var user = await userRepository.CreateAsync(appUser, model.Password);

            if (user == null)
            {
                return false;
            }

            var patient = new Patient { User = user };

            await patientRepository.Insert(patient);

            return true;
        }


        public async Task<bool> ConfirmEmail(ConfirmEmailPatientInDTO model)
        {      
            if (model.user == null)
                return false;

            return await userRepository.ConfirmEmailAsync(model.user, model.token);
        }

    }
}