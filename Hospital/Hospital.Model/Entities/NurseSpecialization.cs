using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model.Entities
{
    public class NurseSpecialization : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Nurse> Nurses { get; set; }
    }
}
