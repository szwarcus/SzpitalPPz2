using System.Threading.Tasks;
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

        public PatientAccountService(IUserRepository userRepository,
                                     IRepository<Patient> patientRepository)
        {
            _userRepository = userRepository;
            _patientRepository = patientRepository;
        }

        public async Task<bool> Register(RegisterPatientInDTO model)
        {
            var result = false;

            if (model == null)
            {
                return result;
            }

            var user = new ApplicationUser(model.SystemRole)
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };

            //result = await _userRepository.CreateAsync(user, model.Password);

            //if (!result)
            //{
            //    return result;
            //}

            var patient = new Patient();
            patient.User = user;

            await _patientRepository.Insert(patient);

            return true;
        }
    }
}
