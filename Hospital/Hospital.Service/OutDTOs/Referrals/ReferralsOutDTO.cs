namespace Hospital.Service.OutDTOs.Referrals
{
    using System.Collections.Generic;

    public class ReferralsOutDTO
    {
        public List<ReferralOutDTO> PresentReferrals { get; set; } = new List<ReferralOutDTO>();

        public List<ReferralOutDTO> OverdueReferrals { get; set; } = new List<ReferralOutDTO>();
    }
}