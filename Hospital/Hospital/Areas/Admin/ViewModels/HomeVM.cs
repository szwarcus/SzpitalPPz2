using Hospital.Model.Identity;
using Hospital.Service.OutDTOs;
using System.Collections.Generic;

namespace Hospital.Areas.Admin.ViewModels
{
    public class HomeVM
    {
        public StatisticsVM StatisticsVM { get; set; }
        public List<ApplicationUserDTO> applicationUsersDTO{ get; set;}
        public ApplicationUser applicationUser;
        public List<MedicamentVM> medicamentsVM { get; set; }

    }
}