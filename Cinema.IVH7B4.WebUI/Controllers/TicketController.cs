﻿using System;
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
        public ActionResult ShowTicketView() {
            CinemaViewModel model = (CinemaViewModel) TempData["model"];
            if (model.PinValue == "0000") {
                model.WrongPingValue = "Vul een geldige pincode in";


                TempData["model"] = model;
                return RedirectToAction("PinView","Pin");
            }
            else {
                InsertNewTicketsIntoDatabase(model);

                TempData["model"] = model;
                return View("ShowTicketView");
            }
            
        }

        public ActionResult PrintTickets() {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];
            var context = new EFDbContext();
            var customer = context.Customers.ToList()[0];
            var loc = context.Locations.ToList()[0];

            var tickets = new List<Ticket>();
            foreach (Ticket t in (List<Ticket>)TempData["InsertedTickets"]) {

                Ticket ti = context.Tickets.ToList().Find(tt => tt.SecretKey == t.SecretKey);

                if (ti != null) {
                    tickets.Add(ti);
                }
            }

            var pdf = new PDFGenerator(tickets, loc);
            TempData["model"] = model;
            return pdf.SendPdf();
        }

        private void InsertNewTicketsIntoDatabase(CinemaViewModel model) {
            var context = new EFDbContext();
            var customer = context.Customers.ToList()[0];

            model.GetSeatsList().ForEach(s =>context.Seats.Add(s));
            context.SaveChanges();

            var tickets = model.GetTicketsList(context.Seats.ToList(), customer);

            foreach (Ticket t in tickets) {
                context.Tickets.Add(t);
                context.SaveChanges();
            }
            TempData["InsertedTickets"] = tickets;
            context.SaveChanges();
        }
    }
}