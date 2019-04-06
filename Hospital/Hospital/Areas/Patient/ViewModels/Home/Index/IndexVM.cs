namespace Hospital.Areas.Patient.ViewModels.Home.Index
{
    public class IndexVM
    {
        public BaseInfoPastAndLastVisitsVM Visits { get; set; }

        public IndexVM()
        {
            Visits = new BaseInfoPastAndLastVisitsVM();
        }
    }
}