using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cinema.IVH7B4.Domain.Abstract;
using Cinema.IVH7B4.Domain.Entities;
using Cinema.IVH7B4.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CInema.IVH7B4.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRequests()
        {
            var request = new Mock<HttpRequestBase>();
            request.Setup(r => r.HttpMethod).Returns("GET");

            var httpContext = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();

            httpContext.Setup(c => c.Request).Returns(request.Object);
            httpContext.Setup(c => c.Session).Returns(session.Object);

            session.Setup(c => c.Add("test", "something here"));

            Mock<IFilmRepository> mock1 = new Mock<IFilmRepository>();

            Mock<IShowingRepository> mock2 = new Mock<IShowingRepository>();

            var controller = new FilterController(mock1.Object, mock2.Object);

            controller.ControllerContext = new ControllerContext(
                new RequestContext(httpContext.Object, new RouteData()), controller);

            session.VerifyAll(); // function is trying to add the desired item to the session in the constructor

            //TODO: Add assertion   
        }
    }

}


/**

    [TestMethod]
    public void TestGetFilteredFilms()
    {
        //Arrange

        // make a list to later fill with films
        List<Film> filmList = new List<Film>();

        // make a list to later fill with showings
        List<Showing> showingList = new List<Showing>();

        // first make a location to use in film
        Location testLocation = new Location
        {
            City = "",
            Country = "",
            kidDiscount = 0.0m,
            LocationID = 1,
            Name = "",
            Province = "",
            Rooms = 1,
            SeniorDiscount = 0.0m,
            StudentDiscount = 0.0m,
            TicketPriceLong = 0.0m,
            TicketPriceNormal = 0.0m,
        };

        Mock<IFilmRepository> mock1 = new Mock<IFilmRepository>();
        mock1.Setup(f => f.GetFilms()).Returns(filmList = new List<Film> 
            {
                new Film
                {
                    Age = 1,
                    Description = "",
                    FilmID = 1,
                    FilmType = 1,
                    Image = null,
                    Is3D = true,
                    Language = "",
                    LanguageSubs = "",
                    Length = 1,
                    Location = testLocation,
                    LocationID = 1,
                    Name = "",
                },
                new Film
                {
                    Age = 1,
                    Description = "",
                    FilmID = 2,
                    FilmType = 1,
                    Image = null,
                    Is3D = false,
                    Language = "",
                    LanguageSubs = "",
                    Length = 1,
                    Location = testLocation,
                    LocationID = 1,
                    Name = "",
                },
                new Film
                {
                    Age = 1,
                    Description = "",
                    FilmID = 3,
                    FilmType = 1,
                    Image = null,
                    Is3D = false,
                    Language = "",
                    LanguageSubs = "",
                    Length = 1,
                    Location = testLocation,
                    LocationID = 1,
                    Name = ""
                }
            }
        );

        // now create a roomlayout to add to room to add to showing
        RoomLayout testLayout = new RoomLayout
        {
            BackX = 1,
            BackY = 1,
            FrontX = 1,
            FrontY = 1,
            LayoutID = 1
        };

        // now create a room that's using above layout
        Room testRoom = new Room
        {
            Layout = testLayout,
            LayoutID = 1,
            Location = testLocation,
            LocationID = 1,
            RoomID = 1,
            RoomNumber = 1
        };

        // create a list of showings
        Mock<IShowingRepository> mock2 = new Mock<IShowingRepository>();
        mock2.Setup(s => s.GetShowings()).Returns(new Showing[]
        {
            // include a showing in the past to test if this one is filtered out
            new Showing
            {
                BeginDateTime = new DateTime(2017, 2, 15, 17, 00, 00),
                EndDateTime = new DateTime(2017, 2, 15, 19, 00, 00),
                Film = filmList[0],
                FilmID = filmList[0].FilmID,
                Room = testRoom,
                RoomID = testRoom.RoomID,
                ShowingID = 1
            },
            new Showing
            {
                BeginDateTime = new DateTime(2017, 3, 9, 17, 00, 00),
                EndDateTime = new DateTime(2017, 3, 9, 19, 00, 00),
                Film = filmList[0],
                FilmID = filmList[0].FilmID,
                Room = testRoom,
                RoomID = testRoom.RoomID,
                ShowingID = 2
            },
            new Showing
            {
                BeginDateTime = new DateTime(2017, 4, 21, 17, 00, 00),
                EndDateTime = new DateTime(2017, 4, 21, 19, 00, 00),
                Film = filmList[1],
                FilmID = filmList[1].FilmID,
                Room = testRoom,
                RoomID = testRoom.RoomID,
                ShowingID = 3
            }
        });

        // create the controller class that will be tested
        FilterController controller = new FilterController(mock1.Object, mock2.Object);

        // create a datetime object that will be used as a chosen filter
        DateTime selection = new DateTime(2017, 3, 11, 00, 00, 00);

        // act - call the method from the controller
        controller.GetFilteredFilms();
        ControllerContext context = new ControllerContext();

        context.HttpContext.Session.
        int resultAmount = showingList.Count;
        int expectedAmount = 1;

        // assert - see if the resultamount (amount of showings remaining in showingList)
        //          is equal to the expectedamount of 1
        Assert.AreEqual(resultAmount, expectedAmount);
    }
}
}
**/
