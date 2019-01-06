using System.Collections.Generic;
using System.Threading.Tasks;
using Hospital.Service.OutDTOs;

namespace Hospital.Service.Abstract
{
    public interface IPatientService
    {
        Task<List<UserOutDTO>> GetAllActivePatients();
    }
}