using AutoMapper;
using Hospital.Service.PatientServices.InDTOs;
using Hospital.ViewModel;

namespace Hospital.Infrastructure
{
    public class MappingDtoVM : Profile
    {
        public MappingDtoVM()
        {
            CreateMap<RegisterVM, RegisterPatientInDTO>();
        }
    }
}