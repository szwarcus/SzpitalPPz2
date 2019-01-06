﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hospital.Model.Entities;
using Hospital.Repository.Abstract;
using Hospital.Service.Abstract;
using Hospital.Service.OutDTOs;

namespace Hospital.Service.Concrete
{
    public class DoctorService : IDoctorService
    {
        private IMapper _mapper;

        private IRepository<Doctor> _doctorRepository;

        public DoctorService(IMapper mapper,
                             IRepository<Doctor> doctorRepository)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<List<UserOutDTO>> GetAllActiveDoctors()
        {
            var result = await _doctorRepository.GetAllAsync<UserOutDTO>(x => new UserOutDTO
                                                                                {
                                                                                    UserID = x.Id,
                                                                                    FirstName = x.User.FirstName,
                                                                                    LastName = x.User.LastName
                                                                                });

            return result;
        }
    }
}