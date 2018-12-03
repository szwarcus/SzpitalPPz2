using System.Threading.Tasks;
using Hospital.Model.Identity;
using Hospital.Service.PatientServices.InDTOs;

namespace Hospital.Service.PatientServices.Abstract
{
    public interface IPatientAccountService
    {
        Task<bool> Register(RegisterPatientInDTO model);
        Task<bool> ConfirmEmail(ConfirmEmailPatientInDTO model);

    }
}