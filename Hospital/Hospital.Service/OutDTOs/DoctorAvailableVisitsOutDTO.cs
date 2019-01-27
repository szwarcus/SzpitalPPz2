using System;
using System.Collections.Generic;

namespace Hospital.Service.OutDTOs
{
    public class DoctorAvailableVisitsOutDTO
    {
        public long DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<TimeSpan> AvailableVisits { get; set; }

        public DoctorAvailableVisitsOutDTO()
        {
            AvailableVisits = new List<TimeSpan>();
        }
    }
}