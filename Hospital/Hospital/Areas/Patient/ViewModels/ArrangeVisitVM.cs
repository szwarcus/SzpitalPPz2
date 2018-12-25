using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Areas.Patient.ViewModels
{
    public class ArrangeVisitVM
    {
        [Required(ErrorMessage = "Data wizyty jest wymagana")]
        [DataType(DataType.Date)]
        [Display(Name = "Data wizyty")]
        public DateTime VisitDate { get; set; }

        public string Description { get; set; }

        public string DoctorUserId { get; set; }
    }
}
