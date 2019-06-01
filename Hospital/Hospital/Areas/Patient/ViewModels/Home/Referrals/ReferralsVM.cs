namespace Hospital.Areas.Patient.ViewModels.Home.Referrals
{
    using System.Collections.Generic;

    public class ReferralsVM
    {
        public List<BaseInfoReferralVM> PresentReferrals = new List<BaseInfoReferralVM>();

        public List<BaseInfoReferralVM> OverdueReferrals = new List<BaseInfoReferralVM>();
    }
}