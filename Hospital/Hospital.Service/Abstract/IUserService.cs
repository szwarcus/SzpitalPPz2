using Hospital.Service.InDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Service.Abstract
{
    public interface IUserService
    {
        Task<bool> ChangeEmail(ChangeEmailInDTO model);
        Task<bool> ChangePhoneNumber(ChangePhoneInDTO model);
        Task<bool> ChangePassword(ChangePasswordInDTO model);
    }
}
