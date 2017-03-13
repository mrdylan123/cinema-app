using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema.IVH7B4.Domain.Concrete;
using Cinema.IVH7B4.Domain.Entities;

namespace Cinema.IVH7B4.WebUI.Controllers
{
    public class NewsletterController : Controller
    {
        // GET: Newsletter
        public ActionResult ShowNewsletter()
        {
            return View("ShowNewsletter");
        }

        [HttpPost]
        public ActionResult HandleSubmitForm()
        {
            String name = Request["Name"];
            int age = Int32.Parse(Request["Age"]);
            String location = Request["Location"];
            String interests = Request["Interests"];
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
                return View("ShowRegistrationThanks");
            }
            else
            {
                ModelState.AddModelError("", "Email bestaat al");
                return View("ShowNewsletter");
            }
            
        }
    }
}