using System;
using Cinema.IVH7B4.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CInema.IVH7B4.UnitTests {
    [TestClass]
    public class UnitTestViewModel {

        [TestMethod]
        public void TestSetTicketChildQuanity() {
            //arrange
            var CinemaViewModel = new CinemaViewModel();
            int quantity = 2;
            ChildTicketOrder childOrder = new ChildTicketOrder(CinemaViewModel);
            ChildTicketOrder normalOrder = new ChildTicketOrder(CinemaViewModel);
            ChildTicketOrder studentOrder = new ChildTicketOrder(CinemaViewModel);
            ChildTicketOrder seniorOrder = new ChildTicketOrder(CinemaViewModel);

            TicketType tt = TicketType.ChildTicket;

            switch (tt) {
                case TicketType.ChildTicket: childOrder.Quantity = quantity; break;
                case TicketType.SeniorTicket: seniorOrder.Quantity = quantity; break;
                case TicketType.StudentTicket: studentOrder.Quantity = quantity; break;
                case TicketType.NormalTicket: normalOrder.Quantity = quantity; break;
                default: break;
            }

            //act
            childOrder.Quantity = quantity;

            //assert
            Assert.AreEqual(2, childOrder.Quantity);
        }
        [TestMethod]
        public void TestSetTicketQuanityNormal() {
            //arrange
            var CinemaViewModel = new CinemaViewModel();
            int quantity = 2;
            ChildTicketOrder childOrder = new ChildTicketOrder(CinemaViewModel);
            ChildTicketOrder normalOrder = new ChildTicketOrder(CinemaViewModel);
            ChildTicketOrder studentOrder = new ChildTicketOrder(CinemaViewModel);
            ChildTicketOrder seniorOrder = new ChildTicketOrder(CinemaViewModel);

            TicketType tt = TicketType.NormalTicket;

            switch (tt) {
                case TicketType.ChildTicket: childOrder.Quantity = quantity; break;
                case TicketType.SeniorTicket: seniorOrder.Quantity = quantity; break;
                case TicketType.StudentTicket: studentOrder.Quantity = quantity; break;
                case TicketType.NormalTicket: normalOrder.Quantity = quantity; break;
                default: break;
            }

            //act
            normalOrder.Quantity = quantity;

            //assert
            Assert.AreEqual(2, normalOrder.Quantity);
        }
        [TestMethod]
        public void TestSetTicketQuanitySenior() {
            //arrange
            var CinemaViewModel = new CinemaViewModel();
            int quantity = 2;
            ChildTicketOrder childOrder = new ChildTicketOrder(CinemaViewModel);
            ChildTicketOrder normalOrder = new ChildTicketOrder(CinemaViewModel);
            ChildTicketOrder studentOrder = new ChildTicketOrder(CinemaViewModel);
            ChildTicketOrder seniorOrder = new ChildTicketOrder(CinemaViewModel);

            TicketType tt = TicketType.SeniorTicket;

            switch (tt) {
                case TicketType.ChildTicket: childOrder.Quantity = quantity; break;
                case TicketType.SeniorTicket: seniorOrder.Quantity = quantity; break;
                case TicketType.StudentTicket: studentOrder.Quantity = quantity; break;
                case TicketType.NormalTicket: normalOrder.Quantity = quantity; break;
                default: break;
            }

            //act
            seniorOrder.Quantity = quantity;

            //assert
            Assert.AreEqual(2, seniorOrder.Quantity);
        }
        [TestMethod]
        public void TestSetTicketQuantityStudent() {
            //arrange
            var CinemaViewModel = new CinemaViewModel();
            int quantity = 2;
            ChildTicketOrder childOrder = new ChildTicketOrder(CinemaViewModel);
            ChildTicketOrder normalOrder = new ChildTicketOrder(CinemaViewModel);
            ChildTicketOrder studentOrder = new ChildTicketOrder(CinemaViewModel);
            ChildTicketOrder seniorOrder = new ChildTicketOrder(CinemaViewModel);

            TicketType tt = TicketType.StudentTicket;

            switch (tt) {
                case TicketType.ChildTicket: childOrder.Quantity = quantity; break;
                case TicketType.SeniorTicket: seniorOrder.Quantity = quantity; break;
                case TicketType.StudentTicket: studentOrder.Quantity = quantity; break;
                case TicketType.NormalTicket: normalOrder.Quantity = quantity; break;
                default: break;
            }

            //act
            studentOrder.Quantity = quantity;

            //assert
            Assert.AreEqual(2, studentOrder.Quantity);
        }
        [TestMethod]
        public void GetAllTickektsQuantityTest() {
            //assert
            var CinemaViewModel = new CinemaViewModel();
            int childQuantity = 1;
            int normalQuantity = 2;
            int studentQuantity = 1;
            int seniorQuantity = 1;

            ChildTicketOrder childOrder = new ChildTicketOrder(CinemaViewModel);
            ChildTicketOrder normalOrder = new ChildTicketOrder(CinemaViewModel);
            ChildTicketOrder studentOrder = new ChildTicketOrder(CinemaViewModel);
            ChildTicketOrder seniorOrder = new ChildTicketOrder(CinemaViewModel);

            //act
            childOrder.Quantity = childQuantity;
            normalOrder.Quantity = normalQuantity;
            studentOrder.Quantity = studentQuantity;
            seniorOrder.Quantity = seniorQuantity;

            int quantityAllTickets = childOrder.Quantity +
                                     seniorOrder.Quantity +
                                     normalOrder.Quantity +
                                     studentOrder.Quantity;
            //assert
            Assert.AreEqual(5, quantityAllTickets);
        }

        [TestMethod]
        public void getTicketListTest() {
            
        }
    }
}