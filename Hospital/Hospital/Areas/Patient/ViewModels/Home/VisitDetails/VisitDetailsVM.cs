namespace Hospital.Areas.Patient.ViewModels.Home.VisitDetails
{
    using System.Collections.Generic;
    using Hospital.Areas.Patient.ViewModels.Home.Index;
    using Hospital.Models.Refferal;

    public class VisitDetailsVM
    {
        public BaseInfoVisitVM BaseInfo { get; set; }

        public List<Referral> Referrals { get; set; }

        public List<string> ResponseFromQuestions { get; set; }

        public VisitDetailsVM()
        {
            BaseInfo = new BaseInfoVisitVM();
            Referrals = new List<Referral>();
            ResponseFromQuestions = new List<string>();
        }
    }
}