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
using System.Web;
using System.Web.Routing;

namespace CInema.IVH7B4.UnitTests
{
    [TestClass]
    public class UnitTestFilmOverviewController
    {
            List<Film> testList = new List<Film>{
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
        

        private List<Showing> getTestShowingsList()
        {
            return new List<Showing>()
            {
                new Showing()
                {
                    ShowingID = 1,
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
                    ShowingID = 2,
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
            mockRepo.Setup(s => s.getFilmList()).Returns(testList);
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

        [TestMethod]
        public void TestRenderFilm()
        {
            //arrange
            Mock<IFilmOverviewRepository> mockRepo = new Mock<IFilmOverviewRepository>();
            FilmOverviewController controller = new FilmOverviewController(mockRepo.Object);
            mockRepo.Setup(s => s.getFilmList()).Returns(testList);
            mockRepo.Setup(s => s.getShowingList()).Returns(getTestShowingsList());
            mockRepo.Setup(s => s.convertDateTime(1)).Returns(new List<string> { "testString"});

            //act
            string expected = "filmOverview";
            ViewResult result = controller.renderFilm(1) as ViewResult;

            string expected2 = "testName";
            string result2 = controller.ViewBag.currentFilm.Name;

            string expected3 = "Zaterdag 11/3/2017   Begintijd 22:40   Eindtijd 23:40 Zaalnummer 5";
            string result3 = controller.ViewBag.firstDateTime[0];

            int expected4 = 1;
            int result4 = controller.ViewBag.filmList.Count;

            string expected5 = "testString";
            string result5 = controller.ViewBag.dateTime[0].str;

            //assert
            Assert.AreEqual(expected, result.ViewName);
            Assert.AreEqual(expected2, result2);
            Assert.AreEqual(expected3, result3);
            Assert.AreEqual(expected4, result4);
            Assert.AreEqual(expected5, result5);
        }

        [TestMethod]
        public void SelectShowing()
        {
            //arrange
            Mock<IFilmOverviewRepository> mockRepo = new Mock<IFilmOverviewRepository>();
            Mock<CinemaViewModel> mockViewModel = new Mock<CinemaViewModel>();
            FilmOverviewController controller = new FilmOverviewController(mockRepo.Object);
            mockRepo.Setup(s => s.getShowingbyId(1)).Returns(getTestShowingsList());
            mockViewModel.Object.SelectedFilm = new Film() { FilmID = 1 };
            controller.TempData["model"] = mockViewModel.Object;

            //act
            var result = (RedirectToRouteResult)controller.SelectShowing(1);
            
            //assert
            Assert.AreEqual("RateOverView", result.RouteValues["action"]);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestFilmRender()
        {
            var mockRepo = new Mock<IFilmOverviewRepository>();
            FilmOverviewController controller = new FilmOverviewController(mockRepo.Object);
            int showingID = 1;
            Mock<IFilmOverviewRepository> mock = new Mock<IFilmOverviewRepository>();
            mock.Setup(f => f.getShowingbyId(showingID));

            var mockHttpContext = new Mock<HttpContextBase>();
            mockHttpContext.Setup(s => s.Session["Film"]).Returns(new Film
            {
                Age = 16,
                Description = "Test",
                FilmID = 2,
                FilmType = 1,
                Is3D = true,
                Language = "NL",
                LanguageSubs = "NL",
                Length = 180,
                LocationID = 1,
                Name = "Test"
            });

            RequestContext rc = new RequestContext(mockHttpContext.Object, new RouteData());
            controller.ControllerContext = new ControllerContext(rc, controller);
            var result = controller.renderFilm(showingID);

            Assert.AreEqual(result.ViewName, "FilmOverview");
        }
    }
}
