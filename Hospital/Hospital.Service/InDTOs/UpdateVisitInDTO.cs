using Hospital.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Service.InDTOs
{
   public class UpdateVisitInDTO
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public StateVisit State { get; set; }
    }
}
