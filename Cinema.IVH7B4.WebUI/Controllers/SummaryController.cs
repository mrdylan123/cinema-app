using Cinema.IVH7B4.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema.IVH7B4.Domain.Concrete;
using Cinema.IVH7B4.Domain.Entities;

namespace Cinema.IVH7B4.WebUI.Controllers
{
    public class SummaryController : Controller
    {
        // GET: Summary
        public ActionResult SummaryView()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model.GetAllTicketsQuantity() <= 0) {

                TempData["model"] = model;
                return RedirectToAction("RateOverview", "Rate");
            }
            ViewBag.Base64 = @"data:image/gif;base64," + Convert.ToBase64String(model.SelectedFilm.Image);

            var context = new EFDbContext();
            var ass = new AutomaticSeatSelection();

            var occupiedSeats = new List<Seat>();

            foreach (Seat s in context.Seats.ToList()) {

                Ticket t = context.Tickets.ToList().Find(ti => ti.SeatID == s.SeatID && ti.ShowingID == model.SelectedShowing.ShowingID);

                if (t == null) {
                    continue;
                }

                if (s.SeatID != t.SeatID) {
                    continue;
                }
                occupiedSeats.Add(s);
            }

            model.SeatCoordList = ass.CalculateSeats(model.SelectedShowing.Room, model.GetAllTicketsQuantity(), occupiedSeats);
            ViewBag.SeatSelectionGUI = ass.VisualizeSeats(model.SelectedShowing.Room, occupiedSeats, model.SeatCoordList);

            TempData["model"] = model;
            return View("SummaryView", model);
        }
    }
}