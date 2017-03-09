using System;
using System.Collections.Generic;
using Cinema.IVH7B4.Domain.Abstract;
using Cinema.IVH7B4.Domain.Entities;
using Cinema.IVH7B4.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CInema.IVH7B4.UnitTests {
    [TestClass]
    public class FilmOverViewControllerTest {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestfilmOverview() {
            // create a roomlayout to add to room to add to showing
            RoomLayout testLayout = new RoomLayout {
                BackX = 1,
                BackY = 1,
                FrontX = 1,
                FrontY = 1,
                LayoutID = 1
            };

            // create a room that's using above layout
            Room testRoom = new Room {
                Layout = testLayout,
                LayoutID = 1,
                RoomID = 1,
                RoomNumber = 1
            };

            Mock<IFilmOverviewRepository> mock = new Mock<IFilmOverviewRepository>();
            mock.Setup(f => f.getFilmList()).Returns(new List<Film>() {
                new Film
                    {
                        Age = 1,
                        Description = "",
                        FilmID = 1,
                        FilmType = 1,
                        Is3D = true,
                        Language = "",
                        LanguageSubs = "",
                        Length = 1,
                        LocationID = 1,
                        Name = "",
                    },
                    new Film
                    {
                        Age = 1,
                        Description = "",
                        FilmID = 2,
                        FilmType = 1,
                        Is3D = false,
                        Language = "",
                        LanguageSubs = "",
                        Length = 1,
                        LocationID = 1,
                        Name = "",
                    }
            });
            mock.Setup(f => f.getShowingList()).Returns(new List<Showing> {
                new Showing {
                    BeginDateTime = new DateTime(2017, 2, 15, 17, 00, 00),
                    EndDateTime = new DateTime(2017, 2, 15, 19, 00, 00),
                    Film = mock.Object.getFilmList()[0],
                    FilmID = mock.Object.getFilmList()[0].FilmID,
                    Room = testRoom,
                    RoomID = testRoom.RoomID,
                    ShowingID = 1
                },
                new Showing
                {
                    BeginDateTime = new DateTime(2017, 4, 01, 17, 00, 00),
                    EndDateTime = new DateTime(2017, 4, 01, 19, 00, 00),
                    Film = mock.Object.getFilmList()[1],
                    FilmID = mock.Object.getFilmList()[1].FilmID,
                    Room = testRoom,
                    RoomID = testRoom.RoomID,
                    ShowingID = 2
                },
                new Showing
                {
                    BeginDateTime = new DateTime(2017, 4, 21, 17, 00, 00),
                    EndDateTime = new DateTime(2017, 4, 21, 19, 00, 00),
                    Film = mock.Object.getFilmList()[1],
                    FilmID = mock.Object.getFilmList()[1].FilmID,
                    Room = testRoom,
                    RoomID = testRoom.RoomID,
                    ShowingID = 3
                }
            });

            FilmOverviewController controller = new FilmOverviewController(mock.Object);
            int FilmId = 1;
            controller.renderFilm(FilmId);
        }
    }
}
