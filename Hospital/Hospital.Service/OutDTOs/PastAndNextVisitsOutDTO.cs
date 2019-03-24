using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Service.OutDTOs
{
    public class PastAndNextVisitsOutDTO
    {
        public PastAndNextVisitsOutDTO()
        {
            RealizedVisits = new List<VisitOutDTO>();
            IrrevocableVisits = new List<VisitOutDTO>();
            UpcomingVisits = new List<VisitOutDTO>();
        }

        public List<VisitOutDTO> RealizedVisits { get; set; }

        public List<VisitOutDTO> IrrevocableVisits { get; set; }

        public List<VisitOutDTO> UpcomingVisits { get; set; }
    }
}
