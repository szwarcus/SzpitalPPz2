namespace Hospital.Areas.Patient.ViewModels
{
    public class HomeIndexVM
    {
        public ArrangeVisitVM ArrangeVisitVM { get; set; }

        public HomeIndexVM()
        {
            ArrangeVisitVM = new ArrangeVisitVM();
        }
    }
}