using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    public class ForgotPasswordVM
    {
        [Required(ErrorMessage = "Podaj adres Email")]
        [Display(Name = "Adres Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
