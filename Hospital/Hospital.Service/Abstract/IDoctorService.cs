using System.Collections.Generic;
using System.Threading.Tasks;
using Hospital.Service.OutDTOs;

namespace Hospital.Service.Abstract
{
    public interface IDoctorService
    {
        Task<List<DoctorOutDTO>> GetAllActiveDoctors();
    }
}