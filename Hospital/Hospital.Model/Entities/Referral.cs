using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Model.Entities
{
    public class Referral : BaseEntity
    {
        public long VisitId { get; set; }
        public virtual Visit Visit { get; set; }
        public string Description { get; set; }
        public long SpecializationId { get; set; }
        public virtual Specialization Specialization { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
