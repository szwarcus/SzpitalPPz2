namespace Hospital.Areas.Patient.ViewModels.Home.Visits
{
    using System;

    public class DescriptionVisitVM
    {
        public int Id { get; set; }

        public string Specialization { get; set; }

        public string DoctorName { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }
    }
}