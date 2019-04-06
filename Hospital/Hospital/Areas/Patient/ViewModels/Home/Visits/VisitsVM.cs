namespace Hospital.Areas.Patient.ViewModels.Home.Visits
{
    public class VisitsVM
    {
        public DescriptionPastAndLastVisitsVM Visits { get; set; }

        public VisitsVM()
        {
            Visits = new DescriptionPastAndLastVisitsVM();
        }
    }
}