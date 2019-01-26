﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Areas.Patient.ViewModels
{
    public class ArrangeVisitVM
    {
        [Required(ErrorMessage = "Data wizyty jest wymagana")]
        //[DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
        [Display(Name = "Data wizyty")]
        public DateTime VisitDate { get; set; }

        public string Description { get; set; }

        public long DoctorId { get; set; }

        //public List<SelectListItem> Doctors { get; set; }

        public List<string> SpecializationNames { get; set; }

        public ArrangeVisitVM()
        {
            VisitDate = DateTime.UtcNow.AddDays(1);
            //Doctors = new List<SelectListItem>();
        }
    }
}
