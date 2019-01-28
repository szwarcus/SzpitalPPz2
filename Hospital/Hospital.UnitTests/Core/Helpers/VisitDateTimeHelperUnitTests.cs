using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hospital.Core.Helpers;

namespace Hospital.UnitTests.Core.Helpers
{
    [TestClass]
    public class VisitDateTimeHelperUnitTests
    {
        private static int _visitMinutes = 30;

        [TestMethod]
        public void DateTimeToVisitNumberInDay_IsCorrect()
        {
            TimeSpan hour = new TimeSpan(10, 30, 0);

            var result = VisitDateTimeHelper.DateTimeToVisitNumberInDay(hour);

            // sixth visit in day from 8:00 am and 30 minutes interval between next visits
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void DateTimeToVisitNumberInDay_IncorrectDate()
        {
            var dateBefore = new TimeSpan(7, 30, 0);
            var dateAfter = new TimeSpan(20, 30, 0);

            var resultBefore = VisitDateTimeHelper.DateTimeToVisitNumberInDay(dateBefore);
            var resultAfter = VisitDateTimeHelper.DateTimeToVisitNumberInDay(dateAfter);

            Assert.AreEqual(0, resultBefore);
            Assert.AreEqual(0, resultAfter);
        }

        [TestMethod]
        public void GetAvailableDateTimesInDay_IsCorrect()
        {
            DateTime day = new DateTime(2019, 01, 01);

            // only hours and minutes are important here
            var startWorkHour = new TimeSpan(10, 0, 0);
            var endWorkHour = new TimeSpan(14, 0, 0);

            // available visit dates between 10:00 am and 2:00 pm are: 5, 6, 7, 8, 9, 10, 11, 12
            List<int> numbersOfArrangedVisits = new List<int> { 6, 8, 10, 11 };

            var result = VisitDateTimeHelper.GetAvailableDateTimesInDay(day, numbersOfArrangedVisits,
                                                                        startWorkHour, endWorkHour);

            Assert.AreEqual(4, result.Count);

            // first visit
            Assert.AreEqual(10, result[0].Hours);
            Assert.AreEqual(0, result[0].Minutes);

            // second visit
            Assert.AreEqual(11, result[1].Hours);
            Assert.AreEqual(0, result[1].Minutes);

            // third visit
            Assert.AreEqual(12, result[2].Hours);
            Assert.AreEqual(0, result[2].Minutes);

            // fourth visit
            Assert.AreEqual(13, result[3].Hours);
            Assert.AreEqual(30, result[3].Minutes);
        }

        [TestMethod]
        public void GetAvailableDateTimesInDay_CheckAllDateTimesFor12WorkHours()
        {
            DateTime day = new DateTime(2019, 01, 01);

            // only hours and minutes are important here
            var startWorkHour = new TimeSpan(8, 0, 0);
            var endWorkHour = new TimeSpan(20, 0, 0);
            var numbersOfArrangedVisits = new List<int>();

            var result = VisitDateTimeHelper.GetAvailableDateTimesInDay(day, numbersOfArrangedVisits,
                                                                        startWorkHour, endWorkHour);

            Assert.AreEqual(24, result.Count);
            for (int i = 0; i < 24; i++)
            {
                var diff = TimeSpan.FromMinutes(i * _visitMinutes);
                Assert.AreEqual(startWorkHour.Add(diff), result[i]);
            }
        }

        [TestMethod]
        public void GetAvailableDateTimesInDay_IncorrectHarmonogram()
        {
            DateTime day = new DateTime(2019, 01, 01);

            // only hours and minutes are important here
            var startWorkHour = new TimeSpan(7, 30, 0);
            var endWorkHour = new TimeSpan(20, 0, 0);
            var numbersOfArrangedVisits = new List<int>();

            var result = VisitDateTimeHelper.GetAvailableDateTimesInDay(day, numbersOfArrangedVisits,
                                                                        startWorkHour, endWorkHour);

            Assert.AreEqual(0, result.Count);
        }
    }
}