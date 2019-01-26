using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Service.InDTOs
{
   public class ChangeEmailInDTO
    {
        public string UserId { get; set; }
        public string NewEmail { get; set; }
        public string Token { get; set; }
    }
}
