using Hospital.Model.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Service.PatientServices.InDTOs
{
   public class ConfirmEmailPatientInDTO
    {
        public string token { get; set; }
        public ApplicationUser user { get; set; }
    }
}
