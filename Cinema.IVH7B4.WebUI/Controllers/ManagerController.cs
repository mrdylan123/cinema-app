using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema.IVH7B4.Domain.Abstract;
using Cinema.IVH7B4.Domain.Entities;
using Cinema.IVH7B4.WebUI.Models;

namespace Cinema.IVH7B4.WebUI.Controllers
{
    public class ManagerController : Controller
    {
        private IManagerLoginRepository repo;

        public ManagerController(IManagerLoginRepository repo)
        {
            this.repo = repo;
        }

        // Check login credentials and show dashboard
        public ActionResult Dashboard()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];
            // Request input from cash register login
            string username = Request["username"];
            string password = Request["password"];

            // No user input
            if (username == "" || password == "")
            {
                return RedirectToAction("ManagerLogin", "Login", new { error = "Vul a.u.b. beide velden in" });
            }
            else
            {
                // Get all logins
                IEnumerable<ManagerLogin> allLogins = repo.GetLogins();

                // Check if username and password exist in database
                bool exists = LoginLogic.CheckManagerLogin(username, password, allLogins);

                if (exists == true)
                {
                    model.LoggedInUser = username;
                    model.loggedInManager = true;
                    TempData["model"] = model;
                    return View("Dashboard", model);
                }
                // No right username - password combination
                else
                {
                    return RedirectToAction("ManagerLogin", "Login",
                        new { error = "Deze combinatie van gebruikersnaam en wachtwoord kon niet gevonden worden" });
                }
            }
        }
    }
}