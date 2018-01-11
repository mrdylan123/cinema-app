using System;
using System.Collections.Generic;
using Cinema.IVH7B4.Domain.Entities;
using Cinema.IVH7B4.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CInema.IVH7B4.UnitTests
{
    [TestClass]
    public class UnitTestPayment
    {
        [TestMethod]
        public void TestRemainingValue()
        {
            //Arrange
            decimal value = 30;
            decimal price = 15.50m;

            //Act
            decimal expectedResult = 14.50m;
            decimal result = new PaymentLogic().RemainingValue(value, price);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestRemainingPrice()
        {
            //Arrange
            decimal price = 30;
            decimal value = 10.50m;

            //Act
            decimal expectedResult = 19.50m;
            decimal result = new PaymentLogic().RemainingPrice(value, price);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestFindCard()
        {
            //Arrange
            int code = 12345;
            List<TenRidesCard> cards = new List<TenRidesCard>
            {
                new TenRidesCard {CardCode = 12345, Rides = 10}
            };

            //Act
            TenRidesCard expResult = cards[0];
            TenRidesCard result = new PaymentLogic().FindCard(code, cards);

            //Assert
            Assert.AreEqual(expResult, result);
        }

    }
}
