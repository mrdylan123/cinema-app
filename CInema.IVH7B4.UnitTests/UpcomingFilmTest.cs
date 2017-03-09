using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Cinema.IVH7B4.Domain.Abstract;
using Cinema.IVH7B4.Domain.Concrete;
using Cinema.IVH7B4.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject.Infrastructure.Language;

namespace CInema.IVH7B4.UnitTests {
    /// <summary>
    /// Summary description for UpcomingFilmTest
    /// </summary>
    [TestClass]
    public class UpcomingFilmTest {
        private bool AreShowingIDListsEqual(List<int> l1, List<int> l2) {
            if (l1.Count != l2.Count) {
                return false;
            }

            for (int i = 0; i < l1.Count; i++) {
                if (l1[i] != l2[i]) {
                    return false;
                }
            }

            return true;
        }

        [TestMethod]
        public void TestFilterByDate() {

            //Arrange -- get a list of showings to filter

            // first make a location to use in film
            Location testLocation = new Location {
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

            // then make a list of films that will be used in the showing
            List<Film> testFilmList = new List<Film>
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
                ;

            // now create a roomlayout to add to room to add to showing
            RoomLayout testLayout = new RoomLayout {
                BackX = 1,
                BackY = 1,
                FrontX = 1,
                FrontY = 1,
                LayoutID = 1
            };

            // now create a room that's using above layout
            Room testRoom = new Room {
                Layout = testLayout,
                LayoutID = 1,
                RoomID = 1,
                RoomNumber = 1
            };

            // create a list of showings to test the filter
            List<Showing> showingsList = new List<Showing>
            {
                //Include a showing in the past to test if this one is filtered out
                new Showing
                {
                    BeginDateTime = new DateTime(2017, 2, 15, 17, 00, 00),
                    EndDateTime = new DateTime(2017, 2, 15, 19, 00, 00),
                    Film = testFilmList[0],
                    FilmID = testFilmList[0].FilmID,
                    Room = testRoom,
                    RoomID = testRoom.RoomID,
                    ShowingID = 1
                },
                new Showing
                {
                    BeginDateTime = new DateTime(2017, 4, 01, 17, 00, 00),
                    EndDateTime = new DateTime(2017, 4, 01, 19, 00, 00),
                    Film = testFilmList[0],
                    FilmID = testFilmList[0].FilmID,
                    Room = testRoom,
                    RoomID = testRoom.RoomID,
                    ShowingID = 2
                },
                new Showing
                {
                    BeginDateTime = new DateTime(2017, 4, 21, 17, 00, 00),
                    EndDateTime = new DateTime(2017, 4, 21, 19, 00, 00),
                    Film = testFilmList[1],
                    FilmID = testFilmList[1].FilmID,
                    Room = testRoom,
                    RoomID = testRoom.RoomID,
                    ShowingID = 3
                }
            };

            // create a datetime object that gets the current date and time to filter out showings from the past
            DateTime now = DateTime.Now;

            // Act -- filter out showing from the past
            List<Showing> testFilteredShowings = showingsList.ToEnumerable()
                .Where(s => s.BeginDateTime > now).ToList();

            // create a list that contains the ShowingID's in the filtered list
            List<int> ShowingIDsByOrder = new List<int>
            {
                testFilteredShowings[0].ShowingID,
                testFilteredShowings[1].ShowingID
            };

            // create a list that contains the expected remaining ShowingID's in the filtered list
            List<int> expectedShowingIDsByOrder = new List<int>
            {
                2,
                3
            };

            // Assert -- see if the expectedresult of ShowingID's have the same value as the list of int's in the expectedShowingIDsByOrder
            Assert.IsTrue(AreShowingIDListsEqual(ShowingIDsByOrder, expectedShowingIDsByOrder));

        }

        [TestMethod]
        public void TestOrderByDate() {

            //Arrange -- get a list of showings to filter

            // first make a location to use in film
            Location testLocation = new Location {
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

            // then make a list of films that will be used in the showing
            List<Film> testFilmList = new List<Film>
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
                ;

            // now create a roomlayout to add to room to add to showing
            RoomLayout testLayout = new RoomLayout {
                BackX = 1,
                BackY = 1,
                FrontX = 1,
                FrontY = 1,
                LayoutID = 1
            };

            // now create a room that's using above layout
            Room testRoom = new Room {
                Layout = testLayout,
                LayoutID = 1,
                RoomID = 1,
                RoomNumber = 1
            };

            // create a list of showings to test the filter
            List<Showing> showingsList = new List<Showing>
            {
                new Showing
                {
                    BeginDateTime = new DateTime(2017, 4, 01, 17, 00, 00),
                    EndDateTime = new DateTime(2017, 4, 01, 19, 00, 00),
                    Film = testFilmList[0],
                    FilmID = testFilmList[0].FilmID,
                    Room = testRoom,
                    RoomID = testRoom.RoomID,
                    ShowingID = 2
                },
                new Showing
                {
                    BeginDateTime = new DateTime(2017, 4, 21, 17, 00, 00),
                    EndDateTime = new DateTime(2017, 4, 21, 19, 00, 00),
                    Film = testFilmList[1],
                    FilmID = testFilmList[1].FilmID,
                    Room = testRoom,
                    RoomID = testRoom.RoomID,
                    ShowingID = 3
                },
                // create a showing with a ShowingID greater than the last one, but with an earlier beginDateTime to see if it will be ordered in the list before the one with a lower ShowingID
                new Showing
                {
                    BeginDateTime = new DateTime(2017, 4, 11, 17, 00, 00),
                    EndDateTime = new DateTime(2017, 11, 04, 19, 00, 00),
                    Film = testFilmList[2],
                    FilmID = testFilmList[2].FilmID,
                    Room = testRoom,
                    RoomID = testRoom.RoomID,
                    ShowingID = 4
                }
            };

            // Act -- order the list by date
            List<Showing> testOrderedFilteredShowings = showingsList.ToEnumerable()
                .OrderBy(s => s.BeginDateTime).ToList();

            // create a list that contains the ShowingID's in the ordered filtered list
            List<int> ShowingIDsByOrder = new List<int>
            {
                testOrderedFilteredShowings[0].ShowingID,
                testOrderedFilteredShowings[1].ShowingID,
                testOrderedFilteredShowings[2].ShowingID
            };

            // create a list that contains the expected remaining ShowingID's in the ordered filtered list
            List<int> expectedShowingIDsByOrder = new List<int>
            {
                2,
                4,
                3
            };

            // Assert -- see if the expectedresult of ShowingID's have the same value as the list of int's in the expectedShowingIDsByOrder
            Assert.IsTrue(AreShowingIDListsEqual(ShowingIDsByOrder, expectedShowingIDsByOrder));
        }

        [TestMethod]
        public void TestFilterOutSameFilm() {

            //Arrange -- get a list of showings to filter

            // first make a location to use in film
            Location testLocation = new Location {
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

            // then make a list of films that will be used in the showing
            List<Film> testFilmList = new List<Film>
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
                ;

            // now create a roomlayout to add to room to add to showing
            RoomLayout testLayout = new RoomLayout {
                BackX = 1,
                BackY = 1,
                FrontX = 1,
                FrontY = 1,
                LayoutID = 1
            };

            // now create a room that's using above layout
            Room testRoom = new Room {
                Layout = testLayout,
                LayoutID = 1,
                RoomID = 1,
                RoomNumber = 1
            };

            // create a list of showings to test the filter
            List<Showing> showingsList = new List<Showing>
            {
                new Showing
                {
                    BeginDateTime = new DateTime(2017, 4, 01, 17, 00, 00),
                    EndDateTime = new DateTime(2017, 4, 01, 19, 00, 00),
                    Film = testFilmList[0],
                    FilmID = testFilmList[0].FilmID,
                    Room = testRoom,
                    RoomID = testRoom.RoomID,
                    ShowingID = 1
                },
                // add a showing with the same film, to see if this one will be filtered out
                new Showing
                {
                    BeginDateTime = new DateTime(2017, 4, 21, 17, 00, 00),
                    EndDateTime = new DateTime(2017, 4, 21, 19, 00, 00),
                    Film = testFilmList[0],
                    FilmID = testFilmList[0].FilmID,
                    Room = testRoom,
                    RoomID = testRoom.RoomID,
                    ShowingID = 2
                }
            };

            // Act -- filter out Showings with the same FilmID
            List<Showing> UniqueShowings = showingsList.ToEnumerable()
                .GroupBy(s => s.FilmID).Select(group => group.First()).ToList();

            // create a list that contains the ShowingID's in the ordered filtered list
            List<int> ShowingIDsByOrder = new List<int>
            {
                UniqueShowings[0].ShowingID,
            };

            // create a list that contains the expected remaining ShowingID's in the ordered filtered list
            List<int> expectedShowingIDsByOrder = new List<int>
            {
                1
            };

            // Assert -- see if the expectedresult of ShowingID's have the same value as the list of int's in the expectedShowingIDsByOrder
            Assert.IsTrue(AreShowingIDListsEqual(ShowingIDsByOrder, expectedShowingIDsByOrder));
        }

        [TestMethod]
        public void TestTakeOutOfList() {
            // Arrange -- create a list
            List<int> testList = new List<int>
            {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                0
            };

            // Act -- take out five 
            List<int> testResult = testList.ToEnumerable()
                .Take(5).ToList();

            // create a list with the expected results from above
            List<int> expectedResults = new List<int>
            {
                1,
                2,
                3,
                4,
                5
            };
            // Assert -- see if the expectedresult of ints have the same value as the testresult ints
            Assert.IsTrue(AreShowingIDListsEqual(testResult, expectedResults));
        }
        [TestMethod]
        public void UpcomingFilmstest() {

        }
    }
}