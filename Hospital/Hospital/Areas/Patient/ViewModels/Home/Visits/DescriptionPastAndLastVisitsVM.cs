namespace Hospital.Areas.Patient.ViewModels.Home.Visits
{
    using System.Collections.Generic;

    public class DescriptionPastAndLastVisitsVM
    {
        public List<DescriptionVisitVM> RealizedVisits { get; set; }

        public List<DescriptionVisitVM> IrrevocableVisits { get; set; }

        public List<DescriptionVisitVM> UpcomingVisits { get; set; }

        public DescriptionPastAndLastVisitsVM()
        {
            RealizedVisits = new List<DescriptionVisitVM>();
            IrrevocableVisits = new List<DescriptionVisitVM>();
            UpcomingVisits = new List<DescriptionVisitVM>();
        }
    }
}