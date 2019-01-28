using System;

namespace Hospital.Model.Entities
{
    public class Visit : BaseEntity
    {
        // values start from 1 for 8:00 am and ends at 19:30 with time interval equals 30 mins
        // eg. 8:00 - 1, 8:30 - 2 etc.
        public int NumberInDay { get; set; } 

        public long DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }

        public long PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        public DateTime Date { get; set; }
        public string Description { get; set; }

        public Prescription Prescription { get; set; }
    }
}