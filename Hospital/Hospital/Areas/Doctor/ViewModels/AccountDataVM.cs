using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Areas.Doctor.ViewModels
{
    public class AccountDataVM
    {
        [Display(Name = "Imię")]
        [Required]
        public string FirstName { get; set; }


        [Display(Name = "Nazwisko")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Adres Email")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Numer Telefonu")]
        [Required]
        public string Phone { get; set; }

        [Display(Name = "Data Urodzin")]
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "PESEl")]
        [Required]
        public string PESEL { get; set; }

        [Display(Name = "Kod Pocztowy")]
        [Required]
        public string PostalCode { get; set; }

        [Display(Name = "Miasto")]
        [Required]
        public string City { get; set; }

        [Display(Name = "Ulia")]
        [Required]
        public string Street { get; set; }

        [Display(Name = "Województwo")]
        [Required]
        public string Province { get; set; }
    }
}
