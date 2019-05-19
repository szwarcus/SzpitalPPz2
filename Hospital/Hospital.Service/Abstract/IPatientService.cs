using System.Threading.Tasks;
using Hospital.Service.InDTOs;
using Hospital.Service.OutDTOs;
using Hospital.Service.OutDTOs.Prescriptions;

namespace Hospital.Service.Abstract
{
    public interface IPatientService
    {
        Task<bool> Register(RegisterPatientInDTO model);
        Task<bool> ConfirmEmail(ConfirmEmailPatientInDTO model);
        Task<PatientOutDTO> GetPatientById(long id);
        Task<PrescriptionsOutDTO> GetPrescriptions(string userId, int count = 25);
    }
}