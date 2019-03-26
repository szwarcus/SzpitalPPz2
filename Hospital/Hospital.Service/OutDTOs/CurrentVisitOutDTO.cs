using Hospital.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Service.OutDTOs
{
   public class CurrentVisitOutDTO
    {
        public long Id { get; set; }
        public long PatientId { get; set; }
        public string Description { get; set; }

        public Prescription Prescription { get; set; }
    }
}
