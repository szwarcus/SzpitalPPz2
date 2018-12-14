using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model.Entities
{
    public class Vaccine: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Dosage { get; set; }
        public virtual ICollection<VaccineApplied> VaccineApplieds { get; set; }
    }
}
