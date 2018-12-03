using System.Collections.Generic;
using Hospital.Model.Identity;

namespace Hospital.Model.Entities
{
    public class Patient : BaseEntity
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }
    }
}