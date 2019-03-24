namespace Hospital.Service.OutDTOs
{
    using System;

    public class VisitOutDTO
    {
        public string Specialization { get; set; }

        public string DoctorName { get; set; }

        public DateTime Date { get; set; }
    }
}