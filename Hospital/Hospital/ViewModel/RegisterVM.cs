using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModel
{
    public class RegisterVM
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Pseudonim")]
        public string Nick { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name="Imię")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Adres Email")]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }
        [Required]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data Urodzin")]
        public DateTime DateBirth { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Numer Telefonu")]
        public string Phone { get; set; }

        
    }
}
