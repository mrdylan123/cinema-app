using System.Collections.Generic;
using Cinema.IVH7B4.Domain.Entities;
using Cinema.IVH7B4.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject.Infrastructure.Language;

namespace CInema.IVH7B4.UnitTests
{
    [TestClass]
    public class UnitTestLogin
    {
        [TestMethod]
        public void TestCashRegisterLogin()
        {
            // Arrange
            // create username, password and list of logins
            string existingusername = "testusername";
            string existingpassword = "testpassword";
            List<CashRegisterLogin> loginslist = new List<CashRegisterLogin>
            {
                new CashRegisterLogin {Password = "testpassword", Username = "testusername"}
            };
            IEnumerable<CashRegisterLogin> logins = loginslist.ToEnumerable();

            // Act
            bool expectedResult = true;
            bool result = LoginLogic.CheckCashRegisterLogin(existingusername, existingpassword, logins);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestManagerLogin()
        {
            // Arrange
            // create username, password and list of logins
            string existingusername = "testusername";
            string existingpassword = "testpassword";
            List<ManagerLogin> loginslist = new List<ManagerLogin>
            {
                new ManagerLogin {Password = "testpassword", Username = "testusername"}
            };
            IEnumerable<ManagerLogin> logins = loginslist.ToEnumerable();

            // Act
            bool expectedResult = true;
            bool result = LoginLogic.CheckManagerLogin(existingusername, existingpassword, logins);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestBackOfficeLogin()
        {
            // Arrange
            // create username, password and list of logins
            string existingusername = "testusername";
            string existingpassword = "testpassword";
            List<BackOfficeLogin> loginslist = new List<BackOfficeLogin>
            {
                new BackOfficeLogin {Password = "testpassword", Username = "testusername"}
            };
            IEnumerable<BackOfficeLogin> logins = loginslist.ToEnumerable();

            // Act
            bool expectedResult = true;
            bool result = LoginLogic.CheckBackOfficeLogin(existingusername, existingpassword, logins);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
