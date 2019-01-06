using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.Model.Entities;
using Hospital.Repository.Abstract;
using Hospital.Service.Abstract;
using Hospital.Service.OutDTOs;

namespace Hospital.Service.Concrete
{
    public class PatientService : IPatientService
    {
        private IMapper _mapper;

        private IRepository<Patient> _patientRepository;

        public PatientService(IMapper mapper,
                             IRepository<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<List<UserOutDTO>> GetAllActivePatients()
        {
            var result = await _patientRepository.GetAllAsync<UserOutDTO>(x => new UserOutDTO
                                                                                {
                                                                                    UserID = x.Id,
                                                                                    FirstName = x.User.FirstName,
                                                                                    LastName = x.User.LastName
                                                                                });

            return result;
        }
    }
}