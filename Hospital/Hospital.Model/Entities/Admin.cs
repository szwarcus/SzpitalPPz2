using Hospital.Model.Identity;

namespace Hospital.Model.Entities
{
    public class Admin : BaseEntity
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}