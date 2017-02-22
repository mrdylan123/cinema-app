using System;
using System.Security.Cryptography.X509Certificates;
using Cinema.IVH7B4.WebUI.Controllers;
using Cinema.IVH7B4.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CInema.IVH7B4.UnitTests {
    [TestClass]
    public class UnitTestPin {
        [TestMethod]
        public void TestMethodAddNumber() {
            //arrange
            PinController controller = new PinController();
            CinemaViewModel model = new CinemaViewModel() {
                PinValue = "2"
            };
            //act
            controller.PinViewReturn(model);
            model.PinValue += "2";
            controller.PinViewReturn(model);
            //assert
            Assert.AreEqual(2, model.PinValue.Length);
        }

        [TestMethod]
        public void TestMethodAddMoreThanFour() {
            //arrange
            PinController controller = new PinController();
            CinemaViewModel model = new CinemaViewModel() {
                PinValue = "2464"
            };
            //act
            model.PinValue += "3";
            //assert
            Assert.AreEqual(model.PinValue.Length, 4);
        }

        [TestMethod]
        public void TestMethodRemoveLastChar() {
            //arrange
            PinController controller = new PinController();
            CinemaViewModel model = new CinemaViewModel() {
                PinValue = "246"
            };
            //act
            controller.PinViewRemoveChar(model);
            //assert
            Assert.AreEqual(model.PinValue.Length, 2);
        }-
    }
}
