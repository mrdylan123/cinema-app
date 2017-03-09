using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinema.IVH7B4.Domain.Entities;
using Cinema.IVH7B4.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject.Infrastructure.Language;

namespace CInema.IVH7B4.UnitTests {
    [TestClass]
    public class UnitTestFilterModel {
        [TestMethod]
        public void TestGetShowingsAfterDate() {
            // Arrange

            // First we have to create an IEnumerable of showings to use in the method that will be tested
            List<Showing> showingslist = new List<Showing> {
                // This showing is before the datetime given
                new Showing {
                    BeginDateTime = new DateTime(2017, 2, 15, 17, 00, 00),
                },
                // This showing is on the same day, but an hour before the datetime given
                new Showing {
                    BeginDateTime = new DateTime(2017, 4, 01, 17, 00, 00),
                },
                // This showing is in the future, so should still be in the list
                new Showing {
                    BeginDateTime = new DateTime(2017, 4, 21, 17, 00, 00),
                    ShowingID = 1
                }
            };

            IEnumerable<Showing> showings = showingslist.ToEnumerable();

            // Create a datetime object that will be used to filter the above list to only contain showings after this date
            DateTime time = new DateTime(2017, 4, 01, 18, 00, 00);

            // Act
            IEnumerable<Showing> resultlist = FilterLogic.GetShowingsAfterDate(showings, time);
            int result = resultlist.Count();
            int expectedresult = 1;

            // Assert
            Assert.AreEqual(result, expectedresult);
        }

        [TestMethod]
        public void TestOrderByDate() {
            // Arrange

            // First we have to create an IEnumerable of showings to use in the method that will be tested
            List<Showing> showingslist = new List<Showing> {
                // This showing is the furthest away so put this one in first
                new Showing {
                    BeginDateTime = new DateTime(2017, 4, 21, 17, 00, 00),
                    ShowingID = 3
                },
                // This showing should stay in the middle
                new Showing {
                    BeginDateTime = new DateTime(2017, 4, 01, 17, 00, 00),
                    ShowingID = 2
                },
                // This showing should be placed first after the method is called
                new Showing {
                    BeginDateTime = new DateTime(2017, 2, 15, 17, 00, 00),
                    ShowingID = 1
                },
            };

            IEnumerable<Showing> showings = showingslist.ToEnumerable();

            // Act
            IEnumerable<Showing> results = FilterLogic.OrderByDate(showings);
            List<Showing> resultList = results.ToList();

            List<int> expectedresult = new List<int> {
                1,
                2,
                3
            };
            List<int> result = new List<int> {
                resultList[0].ShowingID,
                resultList[1].ShowingID,
                resultList[2].ShowingID
            };

            // Assert
            Assert.AreEqual(expectedresult[0], result[0]);
            Assert.AreEqual(expectedresult[1], result[1]);
            Assert.AreEqual(expectedresult[2], result[2]);
        }

        [TestMethod]
        public void TestTodaysShowings() {
            // Arrange
                // First create a datetime object that represents the current day, to see if showings with this BeginDateTime date will stay in the list and others will go
                DateTime userinput = DateTime.Now;
                DateTime currentday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                // Now we create an IEnumerable of showings to use in the method that will be tested
                List<Showing> showingslist = new List<Showing> {
                    // This showing is on the current day and should stay
                    new Showing {
                        BeginDateTime = currentday,
                        ShowingID = 1
                    },
                    // This showing is on a future day and should not remain in the list after the method is called
                    new Showing {
                        BeginDateTime = new DateTime(2017, 4, 01, 17, 00, 00),
                        ShowingID = 2
                    },
                    // This showing is on a past day and should not remain in the list after the method is called
                    new Showing {
                        BeginDateTime = new DateTime(2017, 2, 15, 17, 00, 00),
                        ShowingID = 3
                    },
                };

                IEnumerable<Showing> showings = showingslist.ToEnumerable();

            // Act
            IEnumerable<Showing> resultNumerable = FilterLogic.TodaysShowings(showings, userinput);
            List<Showing> resultList = resultNumerable.ToList();
            int expectedResult = 1;
            int result = resultList[0].ShowingID;

            // Assert
            Assert.AreEqual(expectedResult, result);
        }


        [TestMethod]
        public void TestTakeTen() {
            // Arrange

            // First we have to create an IEnumerable of showings to use in the method that will be tested
            List<Showing> showingslist = new List<Showing> {
                // Put showings in the list with ID's 1 to 11, and check if only 1 to 10 stayed(first 10)
                new Showing {
                    ShowingID = 1
                },
                new Showing {
                    ShowingID = 2
                },
                new Showing {
                    ShowingID = 3
                },
                new Showing {
                    ShowingID = 4
                },
                new Showing {
                    ShowingID = 5
                },
                new Showing {
                    ShowingID = 6
                },
                new Showing {
                    ShowingID = 7
                },
                new Showing {
                    ShowingID = 8
                },
                new Showing {
                    ShowingID = 9
                },
                new Showing {
                    ShowingID = 10
                },
                new Showing {
                    ShowingID = 11
                }
            };

            IEnumerable<Showing> showings = showingslist.ToEnumerable();

            // Act
            List<Showing> resultList = FilterLogic.TakeTen(showings);
            List<int> expectedResult = new List<int> {
                1,
                2,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10
            };
            List<int> results = new List<int> {
                resultList[0].ShowingID,
                resultList[1].ShowingID,
                resultList[2].ShowingID,
                resultList[3].ShowingID,
                resultList[4].ShowingID,
                resultList[5].ShowingID,
                resultList[6].ShowingID,
                resultList[7].ShowingID,
                resultList[8].ShowingID,
                resultList[9].ShowingID
            };

            // Assert
            Assert.AreEqual(expectedResult[0], results[0]);
            Assert.AreEqual(expectedResult[9], results[9]);
        }

        [TestMethod]
        public void TestAddFilms() {
            // Arrange

            // First we have to create a list of films to use in the showingslist
            List<Film> filmsList = new List<Film> {
                new Film {
                    FilmID = 1
                },
                new Film {
                    FilmID = 2
                }
            };

            // Now we create a list of showings to use in the method that will be tested
            List<Showing> showingslist = new List<Showing> {
                new Showing {
                    ShowingID = 1,
                    Film = filmsList[0],
                    FilmID = filmsList[0].FilmID

                },
                // This showing should stay in the middle
                new Showing {
                    ShowingID = 2,
                    Film = filmsList[1],
                    FilmID = filmsList[1].FilmID
                },
                // This showing should be placed first after the method is called
                new Showing {
                    ShowingID = 3,
                    Film = filmsList[0],
                    FilmID = filmsList[0].FilmID
                }
            };

            // Create an empty list of films that the method will fill with the films found in the showingslist
            List<Film> filmsfromshowingslist = new List<Film>();

            // Act
            FilterLogic.AddFilms(showingslist, filmsfromshowingslist);
            List<int> expectedresultlist = new List<int> {
                1,
                2,
                1
            };
            List<int> results = new List<int> {
                filmsfromshowingslist[0].FilmID,
                filmsfromshowingslist[1].FilmID,
                filmsfromshowingslist[2].FilmID
            };

            // Assert
            Assert.AreEqual(expectedresultlist[0], results[0]);
            Assert.AreEqual(expectedresultlist[1], results[1]);
            Assert.AreEqual(expectedresultlist[2], results[2]);
        }

    }
}
