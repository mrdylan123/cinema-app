using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema.IVH7B4.Domain.Abstract;
using Cinema.IVH7B4.Domain.Entities;
using Cinema.IVH7B4.WebUI.Models;

namespace Cinema.IVH7B4.WebUI.Controllers
{
    public class MailController : Controller
    {
        private INewsLetterRegistrationRepository repo;

        public MailController(INewsLetterRegistrationRepository repo)
        {
            this.repo = repo;
        }

        // Send Mail
        public ActionResult Mail(string error, string confirmation)
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

            if (model.LoggedInBackOffice != true)
            {
                return RedirectToAction("BackOfficeLogin", "Login", new {error = "Log in om deze pagina te bezoeken"});
            }

            TempData["model"] = model;
            return View("Mail", model);
        }

        // Send the mail
        [HttpPost]
        public ActionResult SendMails(/*HttpPostedFileBase file*/)
        {
            CinemaViewModel model = (CinemaViewModel) TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            if (model.LoggedInBackOffice != true)
            {
                return RedirectToAction("BackOfficeLogin", "Login", new { error = "Log in om deze pagina te bezoeken" });
            }

            string subject = Request["subject"];
            string mailtext = Request["text"];

            List<NewsletterRegistration> emails = repo.GetRegistrations().ToList();

            int resultscount = emails.Count();

            // Check if there is any emails to send the newsletter to, before starting to set it up
            if (resultscount == 0)
            {
                TempData["model"] = model;
                return RedirectToAction("Mail", "Mail", new {error = "Er zijn geen nieuwsbriefaanmeldingen."});
            }

            //var fileName = Path.GetFileName(file.FileName);
            //var path = Path.Combine(Server.MapPath("~/MailAttachments"), fileName);
            //file.SaveAs(path);


            new Mailer().SendMail(emails, mailtext, subject/*, path*/);

            TempData["model"] = model;
            return RedirectToAction("Mail", "Mail", new {confirmation = "Mails succesvol verzonden."});
        }
    
    }
}