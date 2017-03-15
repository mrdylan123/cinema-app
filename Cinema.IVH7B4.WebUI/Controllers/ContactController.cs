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
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Contact()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            ViewBag.Location = "Breda";

            TempData["model"] = model;
            return View("Contact", model);
        }

        public ActionResult SetLocationAndReturn(String locationName)
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];
            if (locationName != null && model != null)
            {
                model.SelectedLocation = model.findLocationByName(locationName);
            }
            else if (model == null)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                model.SelectedLocation = model.findLocationByName("Breda");
            }

            TempData["model"] = model;
            return RedirectToAction("Contact", "Contact");
        }

        [HttpPost]
        public ActionResult HandleSubmitForm()
        {
            String interest = "";
            Dictionary<string,string> checkboxlist = new Dictionary<string, string>();
            checkboxlist.Add("Actie",Request["Actie"]);
            checkboxlist.Add("Drama", Request["Drama"]);
            checkboxlist.Add("Avontuur", Request["Avontuur"]);
            checkboxlist.Add("Thriller", Request["Thriller"]);
            checkboxlist.Add("Animatie", Request["Animatie"]);
            checkboxlist.Add("Horror", Request["Horror"]);
            checkboxlist.Add("Komedie", Request["Komedie"]);
            checkboxlist.Add("Romantiek", Request["Romantiek"]);

            for(int i = 0; i< checkboxlist.Count; i++)
            {
                if (checkboxlist.ElementAt(i).Value != null)
                {
                   interest += checkboxlist.ElementAt(i).Key + ",";
                }
                else
                {
                    interest += "";
                }
            }

            String name = Request["Name"];
            int age = Int32.Parse(Request["Age"]);
            String location = Request["Location"];
            String interests = interest;
            String email = Request["Email"];

            var context = new EFDbContext();

            if (context.NewsletterRegistrations.ToList().Find(nr => nr.Email == email) == null)
            {
                context.NewsletterRegistrations.Add(new NewsletterRegistration()
                {
                    Age = age,
                    Email = email,
                    Interests = interests,
                    Name = name,
                    Location = location
                });

                context.SaveChanges();
                ModelState.AddModelError("", "U bent ingeschreven voor de nieuwsbrief!");
                return View("Contact");
            }
            else
            {
                ModelState.AddModelError("", "Email bestaat al");
                return View("Contact");
            }

        }
    }
}