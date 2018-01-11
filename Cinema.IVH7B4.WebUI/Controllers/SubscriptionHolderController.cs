using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cinema.IVH7B4.Domain.Abstract;
using Cinema.IVH7B4.Domain.Concrete;
using Cinema.IVH7B4.Domain.Entities;
using Cinema.IVH7B4.WebUI.Models;

namespace Cinema.IVH7B4.WebUI.Controllers
{
    public class SubscriptionHolderController : Controller
    {
        private ISubscriptionHolderRepository repo;

        public SubscriptionHolderController(ISubscriptionHolderRepository repo)
        {
            this.repo = repo;
        }

        // Show SubscriptionHolder login page
        public ViewResult Login(string error)
        {
            CinemaViewModel model = (CinemaViewModel) TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            if (error != null)
            {
                ViewBag.error = error;
            }

            TempData["model"] = model;
            return View("Login", model);
        }

        // Show manage screen so subscription holders can edit their address
        public ActionResult Manage()
        {
            CinemaViewModel model = (CinemaViewModel) TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            TempData["model"] = model;
            return View("Manage", model);
        }

        // User has logged in with facebook and wants to edit his/her info
        public ActionResult GetInfo()
        {
            CinemaViewModel model = (CinemaViewModel) TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            List<SubscriptionHolder> holders = repo.GetHolders().ToList();
            string email = Request["email"];
            int totalholders = holders.Count();

            if (totalholders > 0)
            {
                bool exists = new SubscriptionHolderLogic().HolderExists(email, holders);

                if (exists == true)
                {
                    SubscriptionHolder holder = new SubscriptionHolderLogic().GetHolder(email, holders);
                    model.FoundHolder = holder;
                    return RedirectToAction("EditInfo", "SubscriptionHolder");
                }
                else
                {
                    return RedirectToAction("NewHolder", "SubscriptionHolder");
                }
            }
            else
            {
                return RedirectToAction("NewHolder", "SubscriptionHolder");
            }
        }

        public ActionResult NewHolder(string error, string confirmation)
        {
            CinemaViewModel model = (CinemaViewModel) TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            if (error != null)
            {
                ViewBag.AddError = error;
            }

            if (confirmation != null)
            {
                ViewBag.AddConfirmation = confirmation;
            }

            TempData["model"] = model;

            return View("NewHolder", model);
        }

        public ActionResult AddHolder()
        {
            CinemaViewModel model = (CinemaViewModel) TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            // Retrieve the input
            string email = Request["email"];
            string address = Request["address"];
            string city = Request["city"];

            // Check if the required fields have been filled in
            if (email == "" || address == "" || city == "")
            {
                return RedirectToAction("NewHolder", "SubscriptionHolder",
                    new {error = "Vul a.u.b. alle verplichte velden in"});
            }

            var context = new EFDbContext();

            var newholder = new SubscriptionHolder()
            {
                Email = email,
                Address = address,
                City = city
            };

            context.SubscriptionHolders.Add(newholder);
            context.SaveChanges();


            return RedirectToAction("NewHolder", "SubscriptionHolder",
                new {confirmation = "Uw gegevens zijn succesvol toegevoegd"});
        }

        public ActionResult EditInfo(string error, string confirmation)
        {
            CinemaViewModel model = (CinemaViewModel) TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            if (error != null)
            {
                ViewBag.error = error;
            }

            if (confirmation != null)
            {
                ViewBag.confirmation = confirmation;
            }

            TempData["model"] = model;
            return View("EditInfo", model);
        }

        public ActionResult EditHolder()
        {
            string email = Request["email"];
            string address = Request["address"];
            string city = Request["city"];

            CinemaViewModel model = (CinemaViewModel) TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            // Check if email still exists
            if (email == "")
            {
                return RedirectToAction("Login", "SubscriptionHolder",
                    new {error = "Er is iets foutgegaan, probeer het a.u.b. opnieuw"});
            }

            // Check if user wanted to edit something
            if (address == "" && city == "")
            {
                return RedirectToAction("EditInfo", "SubscriptionHolder",
                    new {error = "U heeft niks ingevoerd en er kon niks gewijzigd worden."});
            }

            // Execute all possibilities of wanting to edit something
            // Only the address needs to be changed
            if (address != "" && city == "")
            {
                List<SubscriptionHolder> holders = repo.GetHolders().ToList();

                var context = new EFDbContext();
                var holderToBeChanged = context.SubscriptionHolders.FirstOrDefault(h => h.Email == email);
                holderToBeChanged.Address = address;
                context.SaveChanges();

                model.FoundHolder = holderToBeChanged;
                return RedirectToAction("EditInfo", "SubscriptionHolder",
                    new {confirmation = "Adres succesvol aangepast"});
            }
            // Only the city needs to be changed
            if (address == "" && city != "")
            {
                List<SubscriptionHolder> holders = repo.GetHolders().ToList();

                var context = new EFDbContext();
                var holderToBeChanged = context.SubscriptionHolders.FirstOrDefault(h => h.Email == email);
                holderToBeChanged.City = city;
                context.SaveChanges();

                model.FoundHolder = holderToBeChanged;
                return RedirectToAction("EditInfo", "SubscriptionHolder",
                    new {confirmation = "Woonplaats succesvol aangepast"});
            }
            // Both address and city need to be changed
            if (address != "" && city != "")
            {
                List<SubscriptionHolder> holders = repo.GetHolders().ToList();

                var context = new EFDbContext();
                var holderToBeChanged = context.SubscriptionHolders.FirstOrDefault(h => h.Email == email);
                holderToBeChanged.Address = address;
                holderToBeChanged.City = city;
                context.SaveChanges();

                model.FoundHolder = holderToBeChanged;
                return RedirectToAction("EditInfo", "SubscriptionHolder",
                    new {confirmation = "Adres en woonplaats succesvol aangepast"});
            }

            return RedirectToAction("Login", "SubscriptionHolder",
                new {error = "Er is iets foutgegaan, probeer het a.u.b. opnieuw"});
        }

    }

}
