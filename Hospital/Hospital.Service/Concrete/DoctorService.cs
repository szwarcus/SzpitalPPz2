using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.Model.Entities;
using Hospital.Repository.Abstract;
using Hospital.Service.Abstract;
using Hospital.Service.OutDTOs;
using System.Linq;
namespace Hospital.Service.Concrete
{
    public class DoctorService : IDoctorService
    {
        private IMapper _mapper;

        private IRepository<Doctor> _doctorRepository;
        private IRepository<Visit> _visitRepository;

        public DoctorService(IMapper mapper,
                             IRepository<Doctor> doctorRepository, IRepository<Visit> visitRepository)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
            _visitRepository = visitRepository;
        }

        public async Task<List<DoctorOutDTO>> GetAllActiveDoctors()
        {
            var result = await _doctorRepository.GetAllAsync<DoctorOutDTO>(x => new DoctorOutDTO
                                                                                {
                                                                                    UserID = x.Id,
                                                                                    FirstName = x.User.FirstName,
                                                                                    LastName = x.User.LastName
                                                                                });


            return result;
        } 
        public async Task<Doctor> GetDoctorById(string DoctorID)
        {
            var result = await _doctorRepository.GetAsync<Doctor>(x => x, x => x.UserId == DoctorID);
            return result.FirstOrDefault();
        }
        public async Task<List<Visit>> GetAllDoctorVisitsByDoctorID(long DoctorID)
        {
            var result = await _visitRepository.GetAsync<Visit>(x => x, x => x.DoctorId == DoctorID);
            return result;
        }
    }
}