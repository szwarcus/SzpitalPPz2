using Hospital.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Service.InDTOs
{
   public class PrescriptionInDTO
    {
        public string Comments { get; set; }
        public long VisitId { get; set; }
        public ICollection<Medicament> Medicaments { get; set; }
    }
}
