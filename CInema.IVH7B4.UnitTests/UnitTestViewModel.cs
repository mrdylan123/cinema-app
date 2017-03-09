using System;
using System.Runtime.InteropServices.ComTypes;
using Cinema.IVH7B4.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CInema.IVH7B4.UnitTests {
    [TestClass]
    public class UnitTestViewModel {

        [TestMethod]
        public void TestSetTicketQuanityChild() {
            //arrange
            var model = new CinemaViewModel();
            int quantity = 2;
            TicketType tt = new ChildTicketOrder(model).GetTicketType();
            //act
            model.SetTicketQuantity(quantity, tt);
            //assert
            Assert.AreEqual(2 , model.ChildTicketOrder.Quantity);

        }
        [TestMethod]
        public void TestSetTicketQuanityNormal() {
            //arrange
            var model = new CinemaViewModel();
            int quantity = 2;
            TicketType tt = new NormalTicketOrder(model).GetTicketType();
            //act
            model.SetTicketQuantity(quantity, tt);
            //assert
            Assert.AreEqual(2, model.NormalTicketOrder.Quantity);
        }

        [TestMethod]
        public void TestSetTicketQuanitySenior() {
            //arrange
            var model = new CinemaViewModel();
            int quantity = 2;
            TicketType tt = model.SeniorTicketOrder.GetTicketType();
            //act
            model.SetTicketQuantity(quantity, tt);
            //assert
            Assert.AreEqual(2, model.SeniorTicketOrder.Quantity);
        }

        [TestMethod]
        public void TestSetTicketQuantityStudent() {
            //arrange
            var model = new CinemaViewModel();
            int quantity = 2;
            TicketType tt = model.StudentTicketOrder.GetTicketType();
            //act
            model.SetTicketQuantity(quantity, tt);
            //assert
            Assert.AreEqual(2, model.StudentTicketOrder.Quantity);
        }

        [TestMethod]
        public void GetAllTickektsQuantityTest() {
            //arrange
            var model = new CinemaViewModel();
            int quantity = 2;
            TicketType tt = model.StudentTicketOrder.GetTicketType();
            TicketType ttSenior = model.SeniorTicketOrder.GetTicketType();
            //act
            model.SetTicketQuantity(quantity, tt);
            model.SetTicketQuantity(quantity, ttSenior);

            int allTickets = model.GetAllTicketsQuantity();
            //assert
            Assert.AreEqual(4, allTickets);
        }
    }
}