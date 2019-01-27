﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Hospital.Model.Entities;
using Hospital.Service.OutDTOs;

namespace Hospital.Service.Abstract
{
    public interface IDoctorService
    {
        Task<List<DoctorOutDTO>> GetAllActiveDoctors();       
        Task<Doctor> GetDoctorById(string DoctorID);
        Task<List<Visit>> GetAllDoctorVisitsByDoctorID(long DoctorID);
    }
}