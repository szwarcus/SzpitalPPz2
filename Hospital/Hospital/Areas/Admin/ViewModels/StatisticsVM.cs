using System.Collections.Generic;

namespace Hospital.Areas.Admin.ViewModels
{
    public class StatisticsVM
    {
        public Dictionary<string, int> statisticsDictionary;
        public StatisticsVM()
        {
            statisticsDictionary = new Dictionary<string, int>();
        }
    }

  
}