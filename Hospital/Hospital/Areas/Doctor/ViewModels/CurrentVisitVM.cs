using Hospital.Model.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Areas.Doctor.ViewModels
{
    public class CurrentVisitVM
    {
        public DateTime DueDate { get; set; }
        public string DescriptionVisit { get; set; }
        public string DescriptionPrescription { get; set; }
        public string FirstName {get; set;}
        public string LastName { get; set; }

        public List<Medicament> Medicaments { get; set; }
        public List<string> selectedMedicaments { get; set; }

        public CurrentVisitVM()
        {
            Medicaments = new List<Medicament>();
        }
    }
}
