namespace Hospital.Areas.Patient.ViewModels
{
    using System;

    public class BaseInfoVisitVM
    {
        public string Specialization { get; set; }

        public string DoctorName { get; set; }

        public DateTime Date { get; set; }
    }
}