﻿using System;
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
            controller.TempData["model"] = model;
            controller.PinViewAddchar("2");
            //assert
            model = (CinemaViewModel)controller.TempData["model"];
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
            controller.TempData["model"] = model;
            model.PinValue += "3";
            //assert
            model = (CinemaViewModel)controller.TempData["model"];
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
            controller.TempData["model"] = model;
            controller.PinViewRemoveChar();
            //assert
            model = (CinemaViewModel)controller.TempData["model"];
            Assert.AreEqual(model.PinValue.Length, 2);
        }
    }
}