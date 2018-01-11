using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema.IVH7B4.Domain.Abstract;
using Cinema.IVH7B4.Domain.Concrete;
using Cinema.IVH7B4.WebUI.Models;
using Cinema.IVH7B4.Domain.Entities;

namespace Cinema.IVH7B4.WebUI.Controllers
{
    public class CriticizeController : Controller
    {
        private IReviewRepository reviewRepo;
        private IFilmRepository filmRepo;

        public CriticizeController(IReviewRepository reviewRepo, IFilmRepository filmRepo)
        {
            this.reviewRepo = reviewRepo;
            this.filmRepo = filmRepo;
        }

        // If a user wants to partake in the survey
        public ViewResult Survey(string error)
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }
            TempData["model"] = model;

            if (error != null)
            {
                ViewBag.SurveyError = error;
            }

            return View("Survey", model);
        }

        // Add the users survey to the database
        public ActionResult AddSurvey()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            // Retrieve the input
            string a1string = Request["rating1"];
            string a2string = Request["rating2"];
            string a3string = Request["rating3"];
            string a4string = Request["rating4"];
            string a5string = Request["rating5"];

            int a1;
            int a2;
            int a3;
            int a4;
            int a5;

            // Check if all fields have been filled in
            if (a1string == "" || a2string == "" || a3string == "" || a4string == "" || a5string == "")
            {
                return RedirectToAction("Survey", "Criticize",
                    new { error = "Vul a.u.b. alle velden in" });
            }

            // Try convert strings to int and start insertion into database
            if (int.TryParse(a1string, out a1) && int.TryParse(a2string, out a2) && int.TryParse(a3string, out a3) && int.TryParse(a4string, out a4) && int.TryParse(a5string, out a5))
            {
                var context = new EFDbContext();

                var survey = new Survey()
                {
                    A1 = a1,
                    A2 = a2,
                    A3 = a3,
                    A4 = a4,
                    A5 = a5
                };

                context.Surveys.Add(survey);
                context.SaveChanges();


                return RedirectToAction("SurveyConfirmation", "Criticize");

            }

            else
            {
                return RedirectToAction("Survey", "Criticize",
                    new { error = "Er is iets fout gegaan, probeer het later opnieuw" });
            }
        }

        public ViewResult SurveyConfirmation()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }
            TempData["model"] = model;

            return View("SurveyConfirmation", model);
        }

        // Enter a review and show all current reviews
        public ViewResult Review(string error, string confirmation)
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }
            TempData["model"] = model;

            if (error != null)
            {
                ViewBag.ReviewError = error;
            }

            if (confirmation != null)
            {
                ViewBag.ReviewConfirmation = confirmation;
            }

            IEnumerable<Review> allReviews = reviewRepo.GetReviews();
            List<Review> reviewsForFilm = allReviews.Where(r => r.FilmID == model.SelectedFilm.FilmID).ToList();

            int numberofresults = reviewsForFilm.Count();
            ViewBag.reviews = reviewsForFilm;
            ViewBag.numberOfResults = numberofresults;
            ViewBag.filmName = model.SelectedFilm.Name;
            return View("Review", model);
        }

        // Add a new review to the database and return user to same page with their review added to it
        public ActionResult AddReview()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            // Retrieve the input
            string ratingstring = Request["rating"];
            string description = Request["description"];

            int rating;

            // Check if the required fields have been filled in
            if (ratingstring == "" || description == "")
            {
                return RedirectToAction("Review", "Criticize",
                    new { error = "Vul a.u.b. alle velden in" });
            }

            // Try convert strings to int and start insertion into database
            if (int.TryParse(ratingstring, out rating))
            {
                var context = new EFDbContext();

                var review = new Review()
                {
                    Rating = rating,
                    Description = description,
                    FilmID = model.SelectedFilm.FilmID

                };

                context.Reviews.Add(review);
                context.SaveChanges();

    
                return RedirectToAction("Review", "Criticize",
                    new { confirmation = "Review succesvol toegevoegd" });
            }

            else
            {
                return RedirectToAction("Review", "Criticize",
                    new { error = "Er is iets fout gegaan, probeer het later opnieuw" });
            }
        }
    }
}