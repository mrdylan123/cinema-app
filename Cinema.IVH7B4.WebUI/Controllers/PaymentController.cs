using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema.IVH7B4.WebUI.Models;

namespace Cinema.IVH7B4.WebUI.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment

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

        public ViewResult INGPayment()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }
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

        public ViewResult MasterCardPayment()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }
            TempData["model"] = model;


            return View("MasterCardPayment", model);
        }
    }
}