using System;

namespace Hospital.Model.Entities
{
    public class Harmonogram : BaseEntity
    {
        public virtual Doctor Doctor { get; set; }

        public TimeSpan MondayStart { get; set; }
        public TimeSpan MondayEnd { get; set; }
        public TimeSpan TuesdayStart { get; set; }
        public TimeSpan TuesdayEnd { get; set; }
        public TimeSpan WednesdayStart { get; set; }
        public TimeSpan WednesdayEnd { get; set; }
        public TimeSpan ThursdayStart { get; set; }
        public TimeSpan ThursdayEnd { get; set; }
        public TimeSpan FridayStart { get; set; }
        public TimeSpan FridayEnd { get; set; }
    }
}