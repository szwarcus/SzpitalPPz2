using Hospital.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Service.OutDTOs
{
    public class VisitOutDTO
    {
        public long Id { get; set; }
        public long PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        public DateTime Date { get; set; }
        public string Description { get; set; }

        public Prescription Prescription { get; set; }
    }
}
