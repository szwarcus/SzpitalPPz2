namespace Hospital.Areas.Patient.ViewModels.Home.Referrals
{
    using System;

    public class BaseInfoReferralVM
    {
        public string DoctorName { get; set; }

        public string DoctorSpecialization { get; set; }

        public DateTime ValidityTerm { get; set; }

        public string Description { get; set; }

        public string SpecializationName { get; set; }
    }
}