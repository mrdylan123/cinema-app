using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema.IVH7B4.Domain.Abstract;
using Cinema.IVH7B4.Domain.Concrete;
using Cinema.IVH7B4.Domain.Entities;
using Cinema.IVH7B4.WebUI.Models;

namespace Cinema.IVH7B4.WebUI.Controllers
{
    public class PaymentController : Controller
    {
        private ITenRidesCardRepository TenRideRepo;
        private IWeekDayCardRepository WeekDayRepo;

        public PaymentController(ITenRidesCardRepository TenRideRepo, IWeekDayCardRepository WeekDayRepo)
        {
            this.TenRideRepo = TenRideRepo;
            this.WeekDayRepo = WeekDayRepo;
        }   

        public ViewResult Payment()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }
            TempData["model"] = model;


            return View("Payment", model);
        }

        public ViewResult IdealPayment()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }
            
            TempData["model"] = model;


            return View("BankSelection", model);
        }

        public ViewResult INGPayment(string error)
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            if (error != null)
            {
                ViewBag.Errorpayment = error;
            }

            model.isPaid = true;

            TempData["model"] = model;
            return View("INGPayment", model);
        }

        public ViewResult CreditCardPayment()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }
            TempData["model"] = model;


            return View("CreditCardPayment", model);
        }

        public ViewResult AfterPay()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }
            TempData["model"] = model;

            model.isPaid = false;

            return View("AfterPay", model);
        }

        public ViewResult MasterCardPayment(string error)
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            if (error != null)
            {
                ViewBag.Errorpayment = error;
            }

            model.isPaid = true;

            TempData["model"] = model;
            return View("MasterCardPayment", model);
        }

        // Sprint 4 - Cash register payment methods
        public ViewResult GiftCardPayment(string error)
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            if (error != null)
            {
                ViewBag.Errorpayment = error;
            }

            model.isPaid = true;

            TempData["model"] = model;
            return View("GiftCardPayment", model);
        }

        public ActionResult CheckValue()
        {
            string valuestring = Request["value"];

            decimal value = decimal.Parse(valuestring);

            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            if (model.LoggedInCashRegister == false)
            {
                return RedirectToAction("GiftCardPayment", "Payment",
                    new { error = "Log in om deze pagina te bezoeken." });
            }

            decimal price = model.GetTotalPriceForAllTickets();

            if (value > price)
            {
                decimal remainingvalue = new PaymentLogic().RemainingValue(value, price);

                if (remainingvalue > 5)
                {
                    ViewBag.todo = "Het resterende bedrag op de bon is €" + remainingvalue + ". Gelieve dit bedrag op de bon te noteren voor toekomstig gebruik.";
                }
                else
                {
                    ViewBag.todo = "Het resterende bedrag op de bon is €" + remainingvalue + ". Gelieve dit bedrag contant terug te geven en de bon weg te gooien.";
                }
            }

            else
            {
                decimal remainingprice = new PaymentLogic().RemainingPrice(value, price);

                ViewBag.todo = "Het resterende bedrag na inlevering van de bon is €" + remainingprice + ". Gelieve dit bedrag contant af te handelen.";
            }

            TempData["model"] = model;
            return View("GiftCardPayment", model);
        }

        public ViewResult TenRideCardPayment(string error, string confirmation)
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            if (model.LoggedInCashRegister == false)
            {
                return View("Payment", "Payment");
            }

            if (error != null)
            {
                ViewBag.Errorpayment = error;
            }

            if (confirmation != null)
            {
                ViewBag.confirmation = confirmation;
            }

            model.isPaid = true;

            TempData["model"] = model;
            return View("TenRideCardPayment", model);
        }

        public ActionResult TenRideCardCheck()
        {
            string valuestring = Request["code"];

            int code;

            int.TryParse(valuestring, out code);

            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            if (model.LoggedInCashRegister == false)
            {
                return RedirectToAction("TenRideCardPayment", "Payment",
                    new { error = "Log in om deze pagina te bezoeken." });
            }

            List<TenRidesCard> cards = TenRideRepo.GetCards().ToList();

            // Check the remaining rides on the card
            TenRidesCard foundCard = new PaymentLogic().FindCard(code, cards);

            if (foundCard == null)
            {
                return RedirectToAction("TenRideCardPayment", "Payment", new { error = "Er kon geen kaart gevonden worden met deze code"});
            }

            int rides = foundCard.Rides;
            int totalTickets = model.GetAllTicketsQuantity();

            if (rides > totalTickets)
            {
                int remaingRides = rides - totalTickets;
                
                var context = new EFDbContext();
                var CardToBeChanged = context.TenRidesCards.SingleOrDefault(c => c.CardCode == code);
                CardToBeChanged.Rides = remaingRides;
                context.SaveChanges();

                return RedirectToAction("TenRideCardPayment", "Payment", new { confirmation = "Betaling succesvol, de kaart heeft nog " + remaingRides + " ritten beschikbaar."});
            }

            if (rides == totalTickets)
            {
                var context = new EFDbContext();
                context.TenRidesCards.Remove(foundCard);
                context.SaveChanges();

                return RedirectToAction("TenRideCardPayment", "Payment", new { confirmation = "Betaling succesvol, het aantal ritten op deze kaart zijn nu verbruikt."});
            }

            else
            {
                var context = new EFDbContext();
                context.TenRidesCards.Remove(foundCard);
                context.SaveChanges();

                int remainingTickets = totalTickets - rides;
                return RedirectToAction("TenRideCardPayment", "Payment", new { confirmation = "De kaart is volledig verbruikt (" + rides + " ritten). Er dienen nog " + remainingTickets + " kaartjes afgerekend te worden."});
            }

        }

        public ViewResult WeekDayCard(string error, string confirmation)
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            if (model.LoggedInCashRegister == false)
            {
                return View("Payment", "Payment");
            }

            if (error != null)
            {
                ViewBag.Errorpayment = error;
            }

            if (confirmation != null)
            {
                ViewBag.confirmation = confirmation;
            }

            model.isPaid = true;

            TempData["model"] = model;
            return View("WeekDayCardPayment", model);
        }

        public ActionResult WeekDayCardCheck()
        {
            string codestring = Request["code"];

            int code = int.Parse(codestring);

            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            if (model.LoggedInCashRegister == false)
            {
                return RedirectToAction("WeekDayCard", "Payment",
                    new { error = "Log in om deze pagina te bezoeken." });
            }

            if (code != 0)
            {
                // Check if current day is monday/tuesday/wednesday/thursday
                string today = DateTime.Now.DayOfWeek.ToString();

                if (today == "Monday" || today == "Tuesday" || today == "Wednesday" || today == "Thursday")
                {
                    // Check if it's not currently an holiday
                    bool a = model.IsHoliday(DateTime.Now);

                    if (a != true)
                    {
                        List<WeekDayCard> cards = WeekDayRepo.GetCards().ToList();
                        WeekDayCard foundCard = cards.Where(c => c.Code == code).ToList()[0];

                        if (foundCard == null)
                        {
                            return RedirectToAction("WeekDayCard", "Payment",
                                new {error = "Er kon geen kaart gevonden worden met deze code"});
                        }

                        var context = new EFDbContext();
                        context.WeekDayCards.Remove(foundCard);
                        context.SaveChanges();

                        int totalcards = model.GetAllTicketsQuantity();
                        int remainingcards = totalcards - 1;

                        if (remainingcards != 0)
                        {
                            return RedirectToAction("WeekDayCard", "Payment",
                                new
                                {
                                    confirmation =
                                    "De kaart is succesvol gebruikt. Er dienen nog " + remainingcards +
                                    " kaartjes afgerekend te worden."
                                });
                        }
                        else
                        {
                            return RedirectToAction("WeekDayCard", "Payment",
                                new {confirmation = "Kaart succesvol gebruikt."});
                        }
                    }
                    else
                    {
                        return RedirectToAction("WeekDayCard", "Payment",
                            new {error = "Het is momenteel vakantie, dit soort tickets is nu niet geldig."});
                    }

                }
                else
                {
                    return RedirectToAction("WeekDayCard", "Payment",
                        new {error = "Het is momenteel geen van de geldige dagen voor dit soort kaart."});
                }
            }

            else
            {
                return RedirectToAction("WeekDayCard", "Payment", new {error = "Vul a.u.b. een correcte code in. (5 cijfers)"});
            }

        }

    }
}