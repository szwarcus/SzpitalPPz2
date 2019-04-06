namespace Hospital.Areas.Patient.ViewModels.Home.ArrangeVisit
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ArrangeVisitVM
    {
        [Required(ErrorMessage = "Data wizyty jest wymagana")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy HH:mm}")]
        [Display(Name = "Data wizyty")]
        public DateTime VisitDate { get; set; }

        public string Description { get; set; }

        public List<SelectListItem> Specializations { get; set; }

        public ArrangeVisitVM()
        {
            VisitDate = DateTime.UtcNow.AddDays(1);
            Specializations = new List<SelectListItem>();
        }
    }
}