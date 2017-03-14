﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Cinema.IVH7B4.WebUI.Controllers;
using Cinema.IVH7B4.Domain.Entities;
using Cinema.IVH7B4.Domain.Concrete;
using System.Collections.Generic;
using Cinema.IVH7B4.WebUI.Models;
using Cinema.IVH7B4.Domain.Abstract;
using System.Web.Mvc;

namespace CInema.IVH7B4.UnitTests
{
    [TestClass]
    public class UnitTestFilmOverviewController
    {
        private static List<Film> getFilmTestList()
        {
            return new List<Film>{
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
        public void TestFilmOverview()
        {
            //arrange
            Mock<IFilmOverviewRepository> mockRepo = new Mock<IFilmOverviewRepository>();
            FilmOverviewController controller = new FilmOverviewController(mockRepo.Object);
            mockRepo.Setup(s => s.getFilmList()).Returns(getFilmTestList());
            mockRepo.Setup(s => s.getShowingList()).Returns(getTestShowingsList());

            //act
            ActionResult result = controller.filmOverview();

            int expected1 = 1;
            int result1 = controller.ViewBag.firstDateTime.Count;

            int expected2 = 1;
            int result2 = controller.ViewBag.filmList.Count;

            CinemaViewModel model = (CinemaViewModel)controller.TempData["model"];

            //assert
            Assert.IsNotNull(result);
            Assert.IsNull(model);
            Assert.AreEqual(expected1, result1);
            Assert.AreEqual(expected2, result2);
        }
    }
}
