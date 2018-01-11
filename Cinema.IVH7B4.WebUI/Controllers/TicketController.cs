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
        public ActionResult ShowTicketView()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            // Request input from Credit Card Payment
            string cardNumber = Request["cardnumber"];
            string name = Request["name"];
            string date = Request["date"];
            string securityNumber = Request["securitynumber"];

            //Request input from ING Bank Payment
            string username = Request["username"];
            string password = Request["password"];

            if (cardNumber != null && name != null && date != null && securityNumber != null)
            {
                if (cardNumber == "" || name == "" || date == "" || securityNumber == "")
                {
                    ViewBag.Errorpayment = "Vul a.u.b. alle velden in.";

                    return RedirectToAction("MasterCardPayment", "Payment", new {error = "Vul a.u.b. alle velden in." });
                }
                else
                {
                    InsertNewTicketsIntoDatabase(model);

                    // empty seat coord list
                    model.SeatCoordList.Clear();
                    TempData["model"] = model;
                    return View("ShowTicketView");
                }
            }

            if (username != null && password != null)
            {
                if (username == "" || password == "")
                {
                    ViewBag.Errorpayment = "Vul a.u.b. alle velden in.";

                    return RedirectToAction("INGPayment", "Payment", new { error = "Vul a.u.b. beide velden in." });
                }
                else
                {
                    InsertNewTicketsIntoDatabase(model);

                    // empty seat coord list
                    model.SeatCoordList.Clear();
                    TempData["model"] = model;
                    return View("ShowTicketView");
                }
            }

            /* if (model.PinValue != null)
            {
                if (model.PinValue == "")
                {
                    return View("ShowTicketView");
                }

                if (model.PinValue == "0000")
                {
                    model.WrongPingValue = "Vul een geldige pincode in";


                    TempData["model"] = model;
                    return RedirectToAction("PinView", "Pin");
                }
                else
                {
                    InsertNewTicketsIntoDatabase(model);

                    // empty seat coord list
                    model.SeatCoordList.Clear();
                    TempData["model"] = model;
                    return View("ShowTicketView");
                }
            } */
            else
            {
                InsertNewTicketsIntoDatabase(model);

                // empty seat coord list
                model.SeatCoordList.Clear();
                TempData["model"] = model;
                return View("ShowTicketView");
            }
            
        }

        public ActionResult ShowCode()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];
            List<Ticket> InsertedTickets = ((List<Ticket>)TempData["InsertedTickets"]);

            Ticket t = InsertedTickets[0];

            if (t != null)
            {
                ViewBag.keycode = t.SecretKey;
            }


            TempData["model"] = model;
            TempData["InsertedTickets"] = InsertedTickets;
            return View("ShowTicketView");
        }

        public ActionResult PrintTickets(String Culture) {
                CinemaViewModel model = (CinemaViewModel) TempData["model"];
                List<Ticket> InsertedTickets = ((List<Ticket>) TempData["InsertedTickets"]);

                var context = new EFDbContext();
                var customer = context.Customers.ToList()[0];
                var loc = context.Locations.ToList()[0];

                Ticket t = ((List<Ticket>) TempData["InsertedTickets"])[0];
                List<Ticket> tickets = context.Tickets.ToList().FindAll(tt => tt.SecretKey == t.SecretKey).ToList();
                var pdf = new PDFGenerator(tickets, loc, Culture);
                TempData["model"] = model;
                TempData["InsertedTickets"] = InsertedTickets;
                return pdf.SendPdf();

        }

        private void InsertNewTicketsIntoDatabase(CinemaViewModel model)
        {
            var context = new EFDbContext();
            var customer = context.Customers.ToList()[0];

            model.GetSeatsList().ForEach(s => context.Seats.Add(s));
            context.SaveChanges();

            var tickets = model.GetTicketsList(context.Seats.ToList(), customer);

            foreach (Ticket t in tickets)
            {
                context.Tickets.Add(t);
                context.SaveChanges();
            }
            TempData["InsertedTickets"] = tickets;
            context.SaveChanges();
        }

        public ViewResult AfterPayCode()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            InsertNewTicketsIntoDatabase(model);

            // empty seat coord list
            model.SeatCoordList.Clear();

            List<Ticket> InsertedTickets = ((List<Ticket>)TempData["InsertedTickets"]);

            Ticket t = InsertedTickets[0];

            if (t != null)
            {
                ViewBag.keycode = t.SecretKey;
            }


            TempData["model"] = model;
            TempData["InsertedTickets"] = InsertedTickets;
            return View("ShowCodeView");
        }
    }
}