using Hospital.Model.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model.Entities
{
    public class Nurse: BaseEntity
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public long NurseSpecializationId { get; set; }
        public virtual NurseSpecialization NurseSpecialization { get; set; }

        public virtual ICollection<VaccineApplied> VaccineApplieds { get; set; }
    }
}
