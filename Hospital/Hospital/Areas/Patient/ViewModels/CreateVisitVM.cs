using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Areas.Patient.ViewModels
{
    public class CreateVisitVM
    {
        [DataType(DataType.Date)]
        [Display(Name = "Data wizyty")]
        public DateTime VisitDate { get; set; }

        public string Description { get; set; }

        public long DoctorId { get; set; }

        public string SpecializationName { get; set; }
    }
}