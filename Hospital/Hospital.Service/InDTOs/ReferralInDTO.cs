using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Service.InDTOs
{
   public class ReferralInDTO
    {
        public long VisitId { get; set; }
        public string Description { get; set; }
        public long SpecializationId { get; set; }
    }
}
