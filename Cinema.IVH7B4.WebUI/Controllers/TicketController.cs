using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema.IVH7B4.Domain.Concrete;
using Cinema.IVH7B4.Domain.Entities;
using Cinema.IVH7B4.WebUI.Models;

namespace Cinema.IVH7B4.WebUI.Controllers
{
    public class TicketController : Controller
    {
        // GET: Ticket
        public ActionResult ShowTicketView(CinemaViewModel model)
        {
            if (model.PinValue == "0000") {
                model.WrongPingValue = "Vul een geldige pincode in";
                return RedirectToAction("PinView","Pin");
            }
            else {
                return View("ShowTicketView");
            }
            
        }

        public ActionResult PrintTickets() {
            var context = new EFDbContext();
            var loc = new Location() {
                Name = "Test location name"
            };

            var pdf = new PDFGenerator(context.Tickets.ToList(), loc);
            return pdf.SendPdf();
        }
    }
}