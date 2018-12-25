using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Areas.Patient.ViewModels
{
    public class HomeVM
    {
        [Required(ErrorMessage = "Data wizyty jest wymagana")]
        [DataType(DataType.Date)]
        [Display(Name = "Data wizyty")]
        public DateTime VisitDate { get; set; }

        public string Description { get; set; }

        public List<DoctorVM> Doctors { get; set; }

        public HomeVM()
        {
            Doctors = new List<DoctorVM>();
            VisitDate = DateTime.UtcNow.AddDays(1);
        }
    }

    public class DoctorVM
    {
        public string DoctorUserId { get; set; }
    }
}