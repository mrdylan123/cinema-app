using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cinema.IVH7B4.Domain.Concrete;
using Moq;
using Cinema.IVH7B4.Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Web.Management;
using Cinema.IVH7B4.Domain.Abstract;
using Cinema.IVH7B4.WebUI.Models;

namespace CInema.IVH7B4.UnitTests
{
    [TestClass]
    public class UnitTestFilmOverviewRepository
    {
        private FilmOverviewRepository getTestObject()
        {
            return new FilmOverviewRepository();
        }

        private List<Showing> getTestShowingsList()
        {
            return new List<Showing>()
            {
                new Showing()
                {
                    BeginDateTime = new DateTime(2017, 3, 11, 22, 40, 40),
                    EndDateTime = new DateTime(2017, 3, 11, 23, 40, 40),
                    FilmID = 1,
                    Room = new Room()
                    {
                        RoomNumber = 5
                    }
                },
                new Showing()
                {
                    BeginDateTime = new DateTime(2017, 3, 10, 22, 40, 40),
                    EndDateTime = new DateTime(2017, 3, 10, 23, 50, 40),
                    FilmID = 2,
                    Room = new Room()
                    {
                        RoomNumber = 3
                    }
                }
            };
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestConvertDateTime()
        {
            //arrange
            Mock<DateStringTranslation> mock2 = new Mock<DateStringTranslation>();
            Mock<FilmOverviewRepository> mock1 = new Mock<FilmOverviewRepository>();
            mock1.Setup(s => s.getShowingList()).Returns(getTestShowingsList());

            //act
            string result1 = mock2.Object.convertDateTime(1)[0];
            string expected1 = "Zaterdag 11/3/2017   Begintijd: 22:40   Eindtijd: 23:40 Zaalnummer: 5";

            string result2 = mock2.Object.convertDateTime(2)[0];
            string expected2 = "Vrijdag 10/3/2017   Begintijd: 22:40   Eindtijd: 23:50 Zaalnummer: 3";
            
            //assert
            Assert.AreEqual(expected1, result1);
            Assert.AreEqual(expected2, result2);
        }

        [TestMethod]
        public void TestGetNextWeekDay()
        {
            //arrange
            //Mock<FilmOverviewRepository> mock = new Mock<FilmOverviewRepository>();

            //act
            string expected = DateTime.Now.AddDays(((int)DayOfWeek.Monday - (int)DateTime.Now.DayOfWeek + 7) % 7).ToString();
            string result = FilmOverviewRepository.getNextWeekday(DateTime.Now, DayOfWeek.Monday).ToString();

            //assert
            Assert.AreEqual(expected, result); 
        }
    }
}
