﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model.Entities
{
    public class Medicament: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    }
}
