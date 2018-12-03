﻿using Hospital.Model.Identity;

namespace Hospital.Model.Entities
{
    public class Doctor : BaseEntity
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        // specialization to add
    }
}