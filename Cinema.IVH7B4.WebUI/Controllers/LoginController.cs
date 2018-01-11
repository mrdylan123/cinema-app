using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema.IVH7B4.WebUI.Models;

namespace Cinema.IVH7B4.WebUI.Controllers
{
    public class LoginController : Controller
    {
        // Show login-type selection screen
        public ViewResult Login()
        {
            CinemaViewModel model = (CinemaViewModel) TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }
            TempData["model"] = model;


            return View("Login", model);
        }

        // Show cash register employee login screen
        public ViewResult CashRegisterEmployeeLogin(string error)
        {
            CinemaViewModel model = (CinemaViewModel) TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            if (error != null)
            {
                ViewBag.Errorlogincash = error;
            }

            TempData["model"] = model;
            return View("CashRegisterEmployeeLogin", model);
        }

        // Show manager login screen
        public ViewResult ManagerLogin(string error)
        {
            CinemaViewModel model = (CinemaViewModel) TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            if (error != null)
            {
                ViewBag.Errorloginmanager = error;
            }

            TempData["model"] = model;
            return View("ManagerLogin", model);
        }

        // Show back-office employee login screen
        public ViewResult BackOfficeLogin(string error)
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            if (error != null)
            {
                ViewBag.Errorloginbackoffice = error;
            }

            TempData["model"] = model;
            return View("BackOfficeLogin", model);
        }

    }
}