using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Service.InDTOs
{
    public class ChangePhoneInDTO
    {
        public string UserID { get; set; }
        public string NewPhoneNumber { get; set; }
        public string Token { get; set; }
    }
}
