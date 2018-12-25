using System;

namespace Hospital.Service.InDTOs
{
    public class ArrangeVisitInDTO
    {
        public long SpecialistId { get; set; }
        public long PatientId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        // to do
        //public Prescription Prescription { get; set; } 
    }
}