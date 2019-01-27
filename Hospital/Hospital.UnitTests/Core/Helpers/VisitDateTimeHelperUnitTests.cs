using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hospital.Core.Helpers;

namespace Hospital.UnitTests.Core.Helpers
{
    [TestClass]
    public class VisitDateTimeHelperUnitTests
    {
        [TestMethod]
        public void DateTimeToVisitNumberInDay_IsCorrect()
        {
            TimeSpan hour = new TimeSpan(10, 30, 0);

            var result = VisitDateTimeHelper.DateTimeToVisitNumberInDay(hour);

            // sixth visit in day from 8:00 am and 30 minutes interval between next visits
            Assert.AreEqual(result, 6);
        }

        [TestMethod]
        public void DateTimeToVisitNumberInDay_IncorrectDate()
        {
            var dateBefore = new TimeSpan(7, 30, 0);
            var dateAfter = new TimeSpan(20, 0, 0);

            var resultBefore = VisitDateTimeHelper.DateTimeToVisitNumberInDay(dateBefore);
            var resultAfter = VisitDateTimeHelper.DateTimeToVisitNumberInDay(dateAfter);

            Assert.AreEqual(resultBefore, 0);
            Assert.AreEqual(resultAfter, 0);
        }

        [TestMethod]
        public void GetAvailableDateTimesInDay_IncorrectDate()
        {
            DateTime day = new DateTime(2019, 01, 01);

            // only hours and minutes are important here
            var startWorkHour = new TimeSpan(10, 0, 0);
            var endWorkHour = new TimeSpan(14, 0, 0);

            // available visit dates between 10:00 am and 2:00 pm are: 5, 6, 7, 8, 9, 10, 11, 12
            List<int> numbersOfArrangedVisits = new List<int> { 6, 8, 10, 11 };

            var result = VisitDateTimeHelper.GetAvailableDateTimesInDay(day, numbersOfArrangedVisits,
                                                                        startWorkHour, endWorkHour);

            Assert.AreEqual(result.Count, 4);

            // first visit
            Assert.AreEqual(result[0].Hours, 10);
            Assert.AreEqual(result[0].Minutes, 0);

            // second visit
            Assert.AreEqual(result[1].Hours, 11);
            Assert.AreEqual(result[1].Minutes, 0);

            // third visit
            Assert.AreEqual(result[2].Hours, 12);
            Assert.AreEqual(result[2].Minutes, 0);

            // fourth visit
            Assert.AreEqual(result[3].Hours, 13);
            Assert.AreEqual(result[3].Minutes, 30);
        }
    }
}