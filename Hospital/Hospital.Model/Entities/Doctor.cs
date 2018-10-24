using Hospital.Model.Identity;

namespace Hospital.Model.Entities
{
    public class Doctor
    {
        public int DoctorId { get; set; }

        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        // specialization to add
    }
}
