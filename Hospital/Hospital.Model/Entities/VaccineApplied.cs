using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model.Entities
{
    public class VaccineApplied: BaseEntity
    {
        public DateTime DateTime { get; set; }
        public long VaccineId { get; set; }
        public Vaccine Vaccine{ get; set; }
        public long NurseId { get; set; }
        public virtual Nurse Nurse { get; set; }
        public long PatientId { get; set; }
        public virtual Patient Patient { get; set; }
       
    }
}
