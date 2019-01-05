using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital.ViewModel
{
    public class RegisterBaseVM
    {
        [Required(ErrorMessage = "Podaj Imię")]
        [DataType(DataType.Text)]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Podaj Nazwisko")]
        [DataType(DataType.Text)]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Podaj Email")]
        [Display(Name = "Adres Email")]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }
        [Required(ErrorMessage = "Podaj hasło")]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Podaj datę urodzin")]
        [DataType(DataType.Date)]
        [Display(Name = "Data Urodzin")]
        public DateTime DateBirth { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Numer Telefonu")]
        public string Phone { get; set; }
    }
}
