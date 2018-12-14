using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model.Entities
{
    public class Prescription: BaseEntity
    {
        public string Comments { get; set; }
        public DateTime DueDate{ get; set; }

        public long VisitId { get; set; }
        public Visit Visit { get; set; }

        public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    }
}
