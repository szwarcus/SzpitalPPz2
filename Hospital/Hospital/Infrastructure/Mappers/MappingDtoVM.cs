using AutoMapper;
using Hospital.Areas.Admin.ViewModels;
using Hospital.Areas.Doctor.ViewModels;
using Hospital.Areas.Patient.ViewModels;
using Hospital.Service.InDTOs;
using Hospital.Service.OutDTOs;
using Hospital.ViewModel;
using System;

namespace Hospital.Mappers.Infrastructure
{
    public class MappingDtoVM : Profile
    {
        public MappingDtoVM()
        {
            CreateMap<RegisterPatientVM, RegisterPatientInDTO>();
            CreateMap<CreateVisitVM, ArrangeVisitInDTO>()
                .ForMember(dest => dest.Date, opts => opts.MapFrom(x => new DateTime(x.VisitDay.Year,
                                                                                     x.VisitDay.Month,
                                                                                     x.VisitDay.Day,
                                                                                     x.VisitHour.Hours,
                                                                                     x.VisitHour.Minutes,
                                                                                     x.VisitHour.Seconds)));
            CreateMap<ChangePasswordVM, ChangePasswordInDTO>();
            CreateMap<ApplicationUserAccountDataVM, ApplicationUserDTO>();
            CreateMap<BaseInfoPastAndLastVisitsVM, PastAndNextVisitsOutDTO>();
            CreateMap<MedicamentVM, MedicamentOutDTO>();

        }
    }
}