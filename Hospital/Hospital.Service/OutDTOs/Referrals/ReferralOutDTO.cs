namespace Hospital.Service.OutDTOs.Referrals
{
    using System;

    public class ReferralOutDTO
    {
        public string DoctorName { get; set; }

        public string DoctorSpecialization { get; set; }

        public DateTime ValidityTerm { get; set; }

        public string Description { get; set; }

        public string SpecializationName { get; set; }
    }
}