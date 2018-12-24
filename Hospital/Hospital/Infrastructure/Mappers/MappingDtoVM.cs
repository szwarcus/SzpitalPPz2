using AutoMapper;
using Hospital.Service.InDTOs;
using Hospital.ViewModel;

namespace Hospital.Mappers.Infrastructure
{
    public class MappingDtoVM : Profile
    {
        public MappingDtoVM()
        {
            CreateMap<RegisterVM, RegisterPatientInDTO>();
        }
    }
}