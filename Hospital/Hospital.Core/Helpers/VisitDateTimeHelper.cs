using System;
using System.Collections.Generic;

namespace Hospital.Core.Helpers
{
    public static class VisitDateTimeHelper
    {
        // represents value of visits number, 19:30 - 8:00 = 11:30
        // (11:30 / 30 minutes) + 1 = 24 
        private static int _numberOfVisitsInOneDay = 24;
        private static int _visitDurationTimeInMinutes = 30;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitHour">Visit hour</param>
        /// <returns>Number of visit in the current day which is set in visitDateTime. 
        ///          0 if visitDateTime is incorrect</returns>
        public static int DateTimeToVisitNumberInDay(TimeSpan visitHour)
        {
            var firstVisitInDay = new TimeSpan(8, 0, 0);

            var differentTime = visitHour.Subtract(firstVisitInDay);

            if (differentTime.TotalMinutes < 0 || differentTime.TotalMinutes >= 720)
            {
                return 0;
            }

            return ((int)differentTime.TotalMinutes / _visitDurationTimeInMinutes) + 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="day">Represents day for returns available hours in that day</param>
        /// <param name="arrangedNumberOfVisitsInDay">Represents list of arranged number of visits in day</param>
        /// <param name="startWorkHours">Represents hour and minutes for start of work in that day </param>
        /// <param name="endWorkHours">Represents hour and minutes for end of work in that day</param>
        /// <returns>Available visits hours in requested day</returns>
        public static List<TimeSpan> GetAvailableDateTimesInDay(DateTime day, List<int> arrangedNumberOfVisitsInDay,
                                                                TimeSpan startWorkHours, TimeSpan endWorkHours)
        {
            var result = new List<TimeSpan>();
            var firstVisitDateTime = new TimeSpan(8, 0, 0);

            var availableNumberOfVisits = new List<int>();
            for (int i=1; i <= _numberOfVisitsInOneDay; i++)
            {
                availableNumberOfVisits.Add(i);
            }

            // removing unavailable number of visits from database
            arrangedNumberOfVisitsInDay.ForEach(x => availableNumberOfVisits.Remove(x));

            // removing unavailable number of visits between 8:00 am to startWorkHours
            var numberOfFirstVisit = DateTimeToVisitNumberInDay(startWorkHours);
            
            for (int i=1; i < numberOfFirstVisit; i++)
            {
                availableNumberOfVisits.Remove(i);
            }

            var numberOfLastVisit = DateTimeToVisitNumberInDay(endWorkHours) - 1;
            for (int i = numberOfLastVisit + 1; i <= _numberOfVisitsInOneDay; i++)
            {
                availableNumberOfVisits.Remove(i);
            }

            availableNumberOfVisits.ForEach(availableNumberOfVisit =>
            {
                var diffTimeSpan = TimeSpan.FromMinutes((availableNumberOfVisit - 1) * _visitDurationTimeInMinutes);
                result.Add(firstVisitDateTime.Add(diffTimeSpan));
            });

            return result;
        }
    }
}