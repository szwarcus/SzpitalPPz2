namespace Hospital.Areas.Patient.ViewModels.Home.Index
{
    using System.Collections.Generic;

    public class BaseInfoPastAndLastVisitsVM
    {
        public BaseInfoPastAndLastVisitsVM()
        {
            RealizedVisits = new List<BaseInfoVisitVM>();
            IrrevocableVisits = new List<BaseInfoVisitVM>();
            UpcomingVisits = new List<BaseInfoVisitVM>();
        }

        public List<BaseInfoVisitVM> RealizedVisits { get; set; }

        public List<BaseInfoVisitVM> IrrevocableVisits { get; set; }

        public List<BaseInfoVisitVM> UpcomingVisits { get; set; }
    }
}