﻿using AutoMapper;
using Hospital.Areas.Doctor.ViewModels;
using Hospital.Areas.Patient.ViewModels;
using Hospital.Service.InDTOs;
using Hospital.ViewModel;

namespace Hospital.Mappers.Infrastructure
{
    public class MappingDtoVM : Profile
    {
        public MappingDtoVM()
        {
            CreateMap<RegisterPatientVM, RegisterPatientInDTO>();
            CreateMap<CreateVisitVM, ArrangeVisitInDTO>()
                .ForMember(dest => dest.Date, opts => opts.MapFrom(dest => dest.VisitDate));
            CreateMap<ChangePasswordVM, ChangePasswordInDTO>();
        }
    }
}