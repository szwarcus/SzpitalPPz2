using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Areas.Doctor.ViewModels
{
    public class ChangeEmailVM
    {
        [Required(ErrorMessage = "Podaj nowy adres")]
        [Display(Name = "Nowy adres")]
        [DataType(DataType.EmailAddress)]
        public string NewEmail{ get; set; }
        [Required(ErrorMessage = "Podaj hasło")]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Password{ get; set; }
    }
}
