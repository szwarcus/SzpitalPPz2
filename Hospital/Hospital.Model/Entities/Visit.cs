using System;

namespace Hospital.Model.Entities
{
    public class Visit : BaseEntity
    {
        public long DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        public long PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}