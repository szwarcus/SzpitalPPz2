using Hospital.Core.Enums;
using Hospital.Model.Entities;
using Hospital.Service.OutDTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Areas.Doctor.ViewModels
{
    public class CurrentVisitVM
    {
        public long VisitID { get; set; }
        [Required(ErrorMessage = "Opis jest wymagany!")]
        [Display(Name = "Przebieg wizyty")]
        public string DescriptionVisit { get; set; }
        public string DescriptionPrescription { get; set; }
        public string DescriptionReferral { get; set; }
        public string FirstName {get; set;}
        public string LastName { get; set; }
        public DateTime Birth { get; set; }
        public StateVisit State { get; set; }
        public long PatientID { get; set; }

        public List<Medicament> Medicaments { get; set; }
        public List<string> selectedMedicaments { get; set; }
        public List<VisitOutDTO> Visits { get; set; }
        public List<SelectListItem> Specializations { get; set; }
        public string selectedSpecialization { get; set; }

        public CurrentVisitVM()
        {
            Medicaments = new List<Medicament>();
            Visits = new List<VisitOutDTO>();
            Specializations = new List<SelectListItem>();
        }
    }
}
