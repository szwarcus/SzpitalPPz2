using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Service.InDTOs
{
    public class ChangePasswordInDTO
    {
        public string UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
