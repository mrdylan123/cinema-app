using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cinema.IVH7B4.Domain.Entities;
using System.Collections.Generic;
using Moq;
using Cinema.IVH7B4.Domain.Concrete;
using Cinema.IVH7B4.WebUI.Models;

namespace CInema.IVH7B4.UnitTests
{
    [TestClass]
    public class UnitTestFilmOverviewLogic
    {

        //arrange
        private static List<Film> filmTestList = new List<Film>{
                new Film()
        {
                    FilmID = 1,
                    Name = "testName",
                    Language = "testLanguage",
                    LanguageSubs = "testLanguageSubs",
                    Age = 12,
                    FilmType = 1,
                    Description = "testDescription",
                    Image = null,
                    Length = 120,
                    Is3D = true,
                    Trailer = "testURL",
                    LocationID = 1
                }
            };

        List<Showing> showingTestList = new List<Showing>()
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

        [TestMethod]
        public void TestConvertDateTime()
        {

            //act
            string result = FilmOverviewLogic.convertDateTimeFirstFilm(filmTestList, showingTestList)[0];
            string expected = "Zaterdag 11/3/2017   Begintijd: 22:40   Eindtijd: 23:40 Zaalnummer: 5";

            //assert
            Assert.AreEqual(expected, result);
        }
    }
}
