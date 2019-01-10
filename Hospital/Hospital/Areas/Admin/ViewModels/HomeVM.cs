using Hospital.Service.OutDTOs;
using System.Collections.Generic;

namespace Hospital.Areas.Admin.ViewModels
{
    public class HomeVM
    {
        public StatisticsVM StatisticsVM { get; set; }
        public List<ApplicationUserDTO> applicationUsers{ get; set;}
    }
}