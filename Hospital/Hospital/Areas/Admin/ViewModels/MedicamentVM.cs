using Hospital.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Areas.Admin.ViewModels
{
    public class MedicamentVM
    {
        [Display(Name = "Nazwa")]
        [Required]
        public string Name { get; set; }


        [Display(Name = "Opis")]
        [Required]
        public string Description { get; set; }
     
    
    }
}
