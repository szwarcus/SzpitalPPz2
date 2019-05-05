using System.Collections.Generic;
using System.Threading.Tasks;
using Hospital.Model.Identity;
using Hospital.Service.OutDTOs;
using Hospital.Service.InDTOs;
namespace Hospital.Service.Abstract
{
    public interface IUserService
    {
        Task<List<ApplicationUserDTO>> GetAllUsersByRole(string roleName);
        Task<bool> ChangeEmail(ChangeEmailInDTO model);
        Task<bool> ChangePhoneNumber(ChangePhoneInDTO model);
        Task<bool> ChangePassword(ChangePasswordInDTO model);
    }
}