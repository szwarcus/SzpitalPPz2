using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.Model.Entities;
using Hospital.Repository.Abstract;
using Hospital.Service.Abstract;
using Hospital.Service.OutDTOs;

namespace Hospital.Service.Concrete
{
    public class SpecializationService : ISpecializationService
    {
        private IMapper _mapper;
        private IRepository<Specialization> _specializationRepository;

        public SpecializationService(IMapper mapper,
                                     IRepository<Specialization> specializationRepository)
        {
            _specializationRepository = specializationRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<SpecializationOutDto>> GetAllAsync()
        {
            return await _specializationRepository.GetAllAsync<SpecializationOutDto>(x => new SpecializationOutDto { Name = x.Name });
        }
    }
}