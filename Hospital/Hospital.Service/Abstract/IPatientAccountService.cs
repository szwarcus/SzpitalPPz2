using System.Threading.Tasks;
using Hospital.Service.InDTOs;

namespace Hospital.Service.Abstract
{
    public interface IPatientAccountService
    {
        Task<bool> Register(RegisterPatientInDTO model);
        Task<bool> ConfirmEmail(ConfirmEmailPatientInDTO model);
    }
}