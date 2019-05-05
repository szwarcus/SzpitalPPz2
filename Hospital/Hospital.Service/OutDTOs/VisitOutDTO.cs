namespace Hospital.Service.OutDTOs
{
    using System;
    using Hospital.Core.Enums;

    public class VisitOutDTO
    {
        public long Id { get; set; }

        public string Specialization { get; set; }

        public string DoctorName { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public StateVisit State { get; set; }
    }
}