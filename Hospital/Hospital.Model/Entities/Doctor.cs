using System.Collections.Generic;
using Hospital.Model.Identity;

namespace Hospital.Model.Entities
{
    public class Doctor : BaseEntity
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public long SpecializationId { get; set; }
        public virtual Specialization Specialization { get; set; }

        public long HarmonogramId { get; set; }
        public virtual Harmonogram Harmonogram { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }
        /*
         * to do
         * certificate to do
         */
    }
}