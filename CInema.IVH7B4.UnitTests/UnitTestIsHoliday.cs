using System;
using Cinema.IVH7B4.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CInema.IVH7B4.UnitTests {
    [TestClass]
    public class UnitTestIsHoliday {

        //Kerstvakantie
        [TestMethod]
        public void IsChristmasHolidayStartTest() {
            Assert.IsTrue(new CinemaViewModel().IsHoliday(new DateTime(2017, 12, 23)));
        }

        [TestMethod]
        public void IsChristmasHolidayDayBeforeTest() {
            Assert.IsFalse(new CinemaViewModel().IsHoliday(new DateTime(2017, 12, 22)));
        }

        [TestMethod]
        public void IsChristmasHolidayEndTest() {
            Assert.IsTrue(new CinemaViewModel().IsHoliday(new DateTime(2018, 1, 7)));
        }

        [TestMethod]
        public void IsChristmasHolidayDayAfterTest() {
            Assert.IsFalse(new CinemaViewModel().IsHoliday(new DateTime(2018, 1, 8)));
        }

        //Krokuavakantie/Carnavalvakantie
        [TestMethod]
        public void IsCarnavalHolidayStartTest() {
            Assert.IsTrue(new CinemaViewModel().IsHoliday(new DateTime(2017, 2, 25)));
        }

        [TestMethod]
        public void IsCarnavalHolidayDayBeforeTest() {
            Assert.IsFalse(new CinemaViewModel().IsHoliday(new DateTime(2017, 2, 24)));
        }

        [TestMethod]
        public void IsCarnavalHolidayEndTest() {
            Assert.IsTrue(new CinemaViewModel().IsHoliday(new DateTime(2017, 3, 5)));
        }

        [TestMethod]
        public void IsCarnavalHolidayDayAfterTest() {
            Assert.IsFalse(new CinemaViewModel().IsHoliday(new DateTime(2017, 3, 6)));
        }

        //Meivakantie
        [TestMethod]
        public void IsMeilHolidayStartTest() {
            Assert.IsTrue(new CinemaViewModel().IsHoliday(new DateTime(2017, 4, 22)));
        }

        [TestMethod]
        public void IsMeiHolidayDayBeforeTest() {
            Assert.IsFalse(new CinemaViewModel().IsHoliday(new DateTime(2017, 4, 21)));
        }

        [TestMethod]
        public void IsMeilHolidayEndTest() {
            Assert.IsTrue(new CinemaViewModel().IsHoliday(new DateTime(2017, 4, 30)));
        }

        [TestMethod]
        public void IsMeiHolidayDayAfterTest() {
            Assert.IsFalse(new CinemaViewModel().IsHoliday(new DateTime(2017, 5, 1)));
        }

        //Zomervakantie
        [TestMethod]
        public void IsSummerHolidayStartTest() {
            Assert.IsTrue(new CinemaViewModel().IsHoliday(new DateTime(2017, 7, 15)));
        }

        [TestMethod]
        public void IsSummerHolidayDayBeforeTest() {
            Assert.IsFalse(new CinemaViewModel().IsHoliday(new DateTime(2017, 7, 14)));
        }

        [TestMethod]
        public void IsSummerHolidayEndTest() {
            Assert.IsTrue(new CinemaViewModel().IsHoliday(new DateTime(2017, 8, 27)));
        }

        [TestMethod]
        public void IsSummerHolidayDayAfterTest() {
            Assert.IsFalse(new CinemaViewModel().IsHoliday(new DateTime(2017, 8, 28)));
        }

        //Herfstvakantie
        [TestMethod]
        public void IsFallHolidayStartTest() {
            Assert.IsTrue(new CinemaViewModel().IsHoliday(new DateTime(2017, 10, 14)));
        }

        [TestMethod]
        public void IsFallHolidayDayBeforeTest() {
            Assert.IsFalse(new CinemaViewModel().IsHoliday(new DateTime(2017, 10, 13)));
        }

        [TestMethod]
        public void IsFallHolidayEndTest() {
            Assert.IsTrue(new CinemaViewModel().IsHoliday(new DateTime(2017, 10, 22)));
        }

        [TestMethod]
        public void IsFallHolidayDayAfterTest() {
            Assert.IsFalse(new CinemaViewModel().IsHoliday(new DateTime(2017, 10, 23)));
        }
    }
}
