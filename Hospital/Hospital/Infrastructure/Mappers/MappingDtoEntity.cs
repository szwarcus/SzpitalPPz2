using AutoMapper;
using Hospital.Model.Identity;
using Hospital.Service.InDTOs;

namespace Hospital.Mappers.Infrastructure
{
    public class MappingDtoEntity : Profile
    {
        public MappingDtoEntity()
        {
            CreateMap<RegisterPatientInDTO, ApplicationUser>()
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(x => x.Email));
        }
    }
}