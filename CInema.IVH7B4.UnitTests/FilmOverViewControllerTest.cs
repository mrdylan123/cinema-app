using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cinema.IVH7B4.Domain.Abstract;
using Cinema.IVH7B4.Domain.Entities;
using Cinema.IVH7B4.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CInema.IVH7B4.UnitTests {
    [TestClass]
    public class FilmOverViewControllerTest {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestRenderFilm() {
            var mockRepo = new Mock<IFilmOverviewRepository>();
            FilmOverviewController controller = new FilmOverviewController(mockRepo.Object);
            int showingID = 1;
            Mock<IFilmOverviewRepository> mock = new Mock<IFilmOverviewRepository>();
            mock.Setup(f => f.getShowingbyId(showingID));

            var mockHttpContext = new Mock<HttpContextBase>();
            mockHttpContext.Setup(s => s.Session["Film"]).Returns(new Film {
                Age = 16,
                Description = "Test",
                FilmID = 2,
                FilmType = 1,
                Is3D = true,
                Language = "NL",
                LanguageSubs = "NL",
                Length = 180,
                LocationID = 1,
                Name = "Test"
            });

            RequestContext rc = new RequestContext(mockHttpContext.Object,new RouteData());
            controller.ControllerContext = new ControllerContext(rc, controller);
            var result = controller.renderFilm(showingID);

            Assert.AreEqual(result.ViewName, "FilmOverview");
        }
    }
}
