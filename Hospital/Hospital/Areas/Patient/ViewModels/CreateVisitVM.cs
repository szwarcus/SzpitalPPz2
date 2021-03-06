﻿namespace Hospital.Areas.Patient.ViewModels
{
    using System;

    public class CreateVisitVM
    {
        public DateTime VisitDay { get; set; }

        public TimeSpan VisitHour { get; set; }

        public string Description { get; set; }

        public long DoctorId { get; set; }

        public string SpecializationName { get; set; }
    }
}