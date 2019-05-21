using Hospital.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Areas.Admin.ViewModels
{
    public class DoctorApplicationUserAccountVM: ApplicationUserAccountDataVM
    {
        [Display(Name = "Hasło")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Specjalizacja")]
        [Required]
        public string Specialization { get; set; }

        public List<string> SpecializationList { get; set; }
    }
}
 