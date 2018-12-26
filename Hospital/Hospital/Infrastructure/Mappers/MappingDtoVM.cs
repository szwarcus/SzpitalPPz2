using AutoMapper;
using Hospital.Areas.Patient.ViewModels;
using Hospital.Service.InDTOs;
using Hospital.ViewModel;

namespace Hospital.Mappers.Infrastructure
{
    public class MappingDtoVM : Profile
    {
        public MappingDtoVM()
        {
            CreateMap<RegisterVM, RegisterPatientInDTO>();
            CreateMap<ArrangeVisitVM, ArrangeVisitInDTO>()
                .ForMember(dest => dest.Date, opts => opts.MapFrom(dest => dest.VisitDate));
        }
    }
}