using Hospital.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Areas.Admin.ViewModels
{
    public class ApplicationUserAccountDataVM
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

        public string PhoneNumber { get; set; }

        [Display(Name = "Data Urodzin")]
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "PESEL")]

        public string PESEL { get; set; }

        [Display(Name = "Kod Pocztowy")]

        public string PostalCode { get; set; }

        [Display(Name = "Miasto")]

        public string City { get; set; }

        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [Display(Name = "Województwo")]
        public string Province { get; set; }

    
    }
}
