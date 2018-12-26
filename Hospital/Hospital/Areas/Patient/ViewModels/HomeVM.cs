using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.Areas.Patient.ViewModels
{
    public class HomeVM
    {
        [Required(ErrorMessage = "Data wizyty jest wymagana")]
        [DataType(DataType.Date)]
        [Display(Name = "Data wizyty")]
        public DateTime VisitDate { get; set; }

        public string Description { get; set; }

        public List<SelectListItem> Doctors { get; set; }

        public HomeVM()
        {
            Doctors = new List<SelectListItem>();
            VisitDate = DateTime.UtcNow.AddDays(1);
        }
    }
}