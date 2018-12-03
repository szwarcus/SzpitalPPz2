using System.Collections.Generic;

namespace Hospital.Model.Entities
{
    public class Specialization : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
    }
}