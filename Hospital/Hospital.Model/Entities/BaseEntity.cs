using System;

namespace Hospital.Model.Entities
{
    public class BaseEntity
    {
        public Int64 Id { get; set; }
        public DateTime Created { get; private set; }

        public BaseEntity()
        {
            Created = DateTime.UtcNow;
        }
    }
}