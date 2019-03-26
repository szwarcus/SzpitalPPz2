using System.Threading.Tasks;
using Hospital.Service.InDTOs;
using Hospital.Service.OutDTOs;

namespace Hospital.Service.Abstract
{
    public interface IPatientAccountService
    {
        Task<bool> Register(RegisterPatientInDTO model);
        Task<bool> ConfirmEmail(ConfirmEmailPatientInDTO model);
        Task<PatientOutDTO> GetPatientById(long id);
    }
}