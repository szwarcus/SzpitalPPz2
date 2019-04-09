using Hospital.Service.OutDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Areas.Doctor.ViewModels
{
    public class TimeTableVM
    {
        public DateTime DateTime { get; set; }
        public List<CurrentVisitOutDTO> Visits { get; set; }
    }
}
