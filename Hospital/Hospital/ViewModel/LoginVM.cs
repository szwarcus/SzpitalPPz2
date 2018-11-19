using System.ComponentModel.DataAnnotations;

namespace Hospital.ViewModel
{
    public class LoginVM
    {
        [Required]
        [Display(Name ="Adres Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}