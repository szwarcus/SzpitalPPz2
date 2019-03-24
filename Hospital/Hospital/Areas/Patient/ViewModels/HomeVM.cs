namespace Hospital.Areas.Patient.ViewModels
{
    public class HomeIndexVM
    {
        public HomeIndexVM()
        {
            Visits = new BaseInfoPastAndLastVisitsVM();
            ArrangeVisit = new ArrangeVisitVM();
        }

        public BaseInfoPastAndLastVisitsVM Visits { get; set; }

        public ArrangeVisitVM ArrangeVisit { get; set; }
    }
}