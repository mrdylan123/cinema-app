using System;
using Cinema.IVH7B4.Domain.Entities;
using Cinema.IVH7B4.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CInema.IVH7B4.UnitTests {
    [TestClass]
    public class UnitTestTicketDiscount {

        [TestMethod]
        public void DiscountNormalPriceDurationLessThan120MinTest() {
            var model = new CinemaViewModel() {
                SelectedFilm = new Film() {
                    Length = 119
                }
            };
            Assert.AreEqual(8.5m, new NormalTicketOrder(model).GetNormalPrice());
        }

        [TestMethod]
        public void DiscountNormalPriceDurationEqualTo120MinTest() {
            var model = new CinemaViewModel() {
                SelectedFilm = new Film() {
                    Length = 120
                }
            };
            Assert.AreEqual(8.5m, new NormalTicketOrder(model).GetNormalPrice());
        }

        [TestMethod]
        public void DiscountNormalPriceDurationLongerThan120MinTest() {
            var model = new CinemaViewModel() {
                SelectedFilm = new Film() {
                    Length = 121
                }
            };
            Assert.AreEqual(9.0m, new NormalTicketOrder(model).GetNormalPrice());
        }

        [TestMethod]
        public void DiscountChildDutchSpokenBefore18HourTest() {
            var model = new CinemaViewModel() {
                SelectedFilm = new Film() {
                    Language = "NL"
                },
                SelectedShowing = new Showing() {
                    EndDateTime = new DateTime(2017,4,4,17,0,0)
                }
            };
            Assert.AreEqual(1.5m, new ChildTicketOrder(model).GetDiscount());
        }
        [TestMethod]
        public void DiscountChildDutchSpoken18HourTest() {
            var model = new CinemaViewModel() {
                SelectedFilm = new Film() {
                    Language = "NL"
                },
                SelectedShowing = new Showing() {
                    EndDateTime = new DateTime(2017, 4, 4, 18, 0, 0)
                }
            };
            Assert.AreEqual(0.0m, new ChildTicketOrder(model).GetDiscount());
        }

        [TestMethod]
        public void DiscountChildDutchSpokenAfter18HourTest() {
            var model = new CinemaViewModel() {
                SelectedFilm = new Film() {
                    Language = "NL"
                },
                SelectedShowing = new Showing() {
                    EndDateTime = new DateTime(2017, 4, 4, 19, 0, 0)
                }
            };
            Assert.AreEqual(0.0m, new ChildTicketOrder(model).GetDiscount());
        }

        [TestMethod]
        public void DiscountChildEnglishpokenBefore18HourTest() {
            var model = new CinemaViewModel() {
                SelectedFilm = new Film() {
                    Language = "EN"
                },
                SelectedShowing = new Showing() {
                    EndDateTime = new DateTime(2017, 4, 4, 17, 0, 0)
                }
            };
            Assert.AreEqual(0.0m, new ChildTicketOrder(model).GetDiscount());
        }

        [TestMethod]
        public void DiscountStudentMondayTest() {
            var model = new CinemaViewModel() {
                SelectedShowing = new Showing() {
                    EndDateTime = new DateTime(2017, 2, 6)
                }
            };
            Assert.AreEqual(1.5m, new StudentTicketOrder(model).GetDiscount());
        }
    }
}
