using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Areas.Doctor.ViewModels
{
    public class ChangePasswordVM
    {
        [Required(ErrorMessage = "Podaj aktualne hasło")]
        [Display(Name = "Aktualne hasło")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Podaj nowe hasło")]
        [Display(Name = "Nowe hasło")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Potwierdź hasło")]
        [Display(Name = "Potwierdź hasło")]
        [DataType(DataType.Password)]
        [CompareAttribute("NewPassword", ErrorMessage = "Podane hasła nie są takie same!")]
        public string ConfirmNewPassword { get; set; }
    }
}
