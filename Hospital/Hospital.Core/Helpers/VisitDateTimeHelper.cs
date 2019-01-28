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

        public static int DateTimeToVisitNumberInDay(DateTime visitDateTime)
        {
            DateTime firstVisitInDay = new DateTime(visitDateTime.Year,
                                                    visitDateTime.Month,
                                                    visitDateTime.Day,
                                                    8, 0, 0);

            var differentTime = visitDateTime.Subtract(firstVisitInDay);

            return ((int)differentTime.TotalMinutes / _visitDurationTimeInMinutes) + 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="day">Represents day for returns available hours in that day</param>
        /// <param name="arrangedNumberOfVisitsInDay">Represents list of arranged number of visits in day</param>
        /// <returns>Available visits hours in requested day</returns>
        public static List<DateTime> GetAvailableDateTimesInDay(DateTime day, List<int> arrangedNumberOfVisitsInDay)
        {
            var result = new List<DateTime>();
            var firstVisitDateTime = new DateTime(day.Year,
                                                  day.Month,
                                                  day.Day,
                                                  8, 0, 0);

            var availableNumberOfVisits = new List<int>();
            for (int i=1; i <= _numberOfVisitsInOneDay; i++)
            {
                availableNumberOfVisits.Add(i);
            }

            arrangedNumberOfVisitsInDay.ForEach(x => availableNumberOfVisits.Remove(x));

            availableNumberOfVisits.ForEach(availableNumberOfVisit =>
            {
                DateTime date = firstVisitDateTime.AddMinutes((availableNumberOfVisit - 1) * _visitDurationTimeInMinutes);

                result.Add(date);
            });

            return result;
        }
    }
}