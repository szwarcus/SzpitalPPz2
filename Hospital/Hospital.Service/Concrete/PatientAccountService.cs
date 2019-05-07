using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.Model.Entities;
using Hospital.Model.Identity;
using Hospital.Repository.Abstract;
using Hospital.Service.Abstract;
using Hospital.Service.InDTOs;
using Hospital.Service.OutDTOs;

namespace Hospital.Service.Concrete
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
    }
}