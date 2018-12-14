using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model.Entities
{
   public class PrescriptionMedicament
    {
        public long PrescriptionId { get; set; }
        public Prescription Prescription { get; set; }

        public long MedicamentId { get; set; }
        public Medicament Medicament { get; set; }
    }
}
