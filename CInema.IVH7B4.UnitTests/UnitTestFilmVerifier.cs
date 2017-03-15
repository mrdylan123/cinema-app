using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Cinema.IVH7B4.Domain.Entities;
using System.Collections.Generic;
using Cinema.IVH7B4.WebUI.Models;

namespace CInema.IVH7B4.UnitTests
{
    [TestClass]
    public class UnitTestFilmVerifier
    {
        public static List<Film> TestFilmList() {
            {
                return new List<Film>() {
                new Film()
            {
                FilmID = 1,
                Name = "testName",
                Language = "testLanguage",
                LanguageSubs = "testLanguageSubs",
                Age = 12,
                FilmType = 1,
                Description = "testDescription",
                Image = null,
                Length = 120,
                Is3D = true,
                Trailer = "testURL",
                LocationID = 1
            },
                new Film()
            {
                FilmID = 2,
                Name = "testName2",
                Language = "testLanguage2",
                LanguageSubs = "testLanguageSubs2",
                Age = 18,
                FilmType = 2,
                Description = "testDescription2",
                Image = null,
                Length = 120,
                Is3D = false,
                Trailer = "testURL2",
                LocationID = 2
            }

        };
            }
        }
    
        [TestMethod]
        public void testVerify()
        {
            //arrange
            Mock<FilmVerifier> mock = new Mock<FilmVerifier>();
            
            //act
            int expected = 2;
            int result = mock.Object.verify("testname2", TestFilmList()).FilmID;

            //assert
            Assert.AreEqual(expected, result);
        }
    }
}
