using Cinema.IVH7B4.Domain.Concrete;
using Cinema.IVH7B4.Domain.Entities;
using Cinema.IVH7B4.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.IVH7B4.WebUI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var model = new CinemaViewModel();

            TempData["model"] = model;
            return View();
        }

        public ActionResult SetLocationAndGotoFilmOverview(String locationName)
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];
            model.SelectedLocation = model.findLocationByName(locationName);

            TempData["model"] = model;
            return RedirectToAction("filmOverview","FilmOverview");
        }
    }
}