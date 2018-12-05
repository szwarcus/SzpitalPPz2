using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Hospital.ViewModel
{
    public class ResetPasswordVM
    {
        [Required]
        public string UserId { get; set; }
        [Required(ErrorMessage ="Podaj nowe hasło")]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage ="Potwierdź nowe hasło")]
        [Display(Name = "Potwierdź hasło")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Token { get; set; }

    }
}