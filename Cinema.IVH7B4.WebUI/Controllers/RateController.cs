using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema.IVH7B4.Domain.Entities;
using Cinema.IVH7B4.Domain;
using System.IO;
using System.Reflection;
using System.Drawing;
using Cinema.IVH7B4.Domain.Concrete;
using Cinema.IVH7B4.WebUI.Models;

namespace Cinema.IVH7B4.WebUI.Controllers
{
    public class RateController : Controller
    {
        // GET: RateOverview
        public ActionResult RateOverview()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];
            ViewBag.Base64 = @"data:image/gif;base64," + Convert.ToBase64String(model.SelectedFilm.Image);

            model.SeatCoordList.Clear();
            TempData["model"] = model;
            return View("RateOverview", model);
        }

        public ActionResult AdjustQuantity(int newQuantity, TicketType tt)
        {

            CinemaViewModel model = (CinemaViewModel)TempData["model"];
            model.SetTicketQuantity(newQuantity, tt);


            TempData["model"] = model;
            return RedirectToAction("RateOverview");
        }

        public ActionResult ResetQuantity()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];
            model.ResetTickets();

            TempData["model"] = model;
            return RedirectToAction("RateOverview");
        }
    }
}