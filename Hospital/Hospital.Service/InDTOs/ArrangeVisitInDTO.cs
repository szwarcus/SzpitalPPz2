using System;

namespace Hospital.Service.InDTOs
{
    public class ArrangeVisitInDTO
    {
        public long DoctorId { get; set; }
        public string PatientUserId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        // to do
        //public Prescription Prescription { get; set; } 
    }
}