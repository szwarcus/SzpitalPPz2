using Hospital.Model.Identity;

namespace Hospital.Service.InDTOs
{
   public class ConfirmEmailPatientInDTO
    {
        public string token { get; set; }
        public ApplicationUser user { get; set; }
    }
}