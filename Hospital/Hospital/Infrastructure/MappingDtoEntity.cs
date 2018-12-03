using AutoMapper;
using Hospital.Model.Identity;
using Hospital.Service.PatientServices.InDTOs;

namespace Hospital.Infrastructure
{
    public class MappingDtoEntity : Profile
    {
        public MappingDtoEntity()
        {
            CreateMap<RegisterPatientInDTO, ApplicationUser>()
                .ConstructUsing(x => new ApplicationUser(x.SystemRole))
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(x => x.Email));
        }
    }
}