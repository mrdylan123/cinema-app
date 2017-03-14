using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema.IVH7B4.Domain.Abstract;
using Cinema.IVH7B4.Domain.Entities;
using Cinema.IVH7B4.WebUI.Models;
using System.Globalization;

namespace Cinema.IVH7B4.WebUI.Controllers
{
    public class FilterController : Controller
    {
        private IShowingRepository showingRepo;
        private IFilmRepository filmRepo;

        public FilterController(IFilmRepository filmRepo, IShowingRepository showingRepo)
        {
            this.filmRepo = filmRepo;
            this.showingRepo = showingRepo;
        }


        public ActionResult GetFilteredFilms()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }


            // Retrieve the user input
            string dayInput = Request["searchDay"]; // format: 03-04-2017      (string)
            string timeInput = Request["searchTime"]; // format: 13 14 15 .... (int)

            // Initialize time variable that will be used to work with the user input
            DateTime time;

            // If a day has been entered, put this in time. Else, take the current day.
            if (dayInput != "")
            {
                time = DateTime.ParseExact(dayInput, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                time = DateTime.Now;
            }

            // If a time has been entered, put this in time.
            if (timeInput != "")
            {
                time = time.AddHours(Int32.Parse(timeInput));
            }

            var dayofweek = time.DayOfWeek;

            //First get all showings
            IEnumerable<Showing> allShowings = showingRepo.GetShowings();

            //Filter the list to only contain showings coming after the given time
            IEnumerable<Showing> showingsAfterDate = FilterLogic.GetShowingsAfterDate(allShowings, time);

            //Order the results by date so that the most near one will be shown first
            IEnumerable<Showing> showingsOrdered = FilterLogic.OrderByDate(showingsAfterDate);
            
            //Filter out showings that are not on the chosen day
            IEnumerable<Showing> todaysShowings = FilterLogic.TodaysShowings(showingsOrdered, time);

            //If the list contains more than 10, take the first 10 showings to show on the page 
            List<Showing> allTenShowings = new List<Showing>();
            if (todaysShowings.Count() > 10)
            {
                allTenShowings = FilterLogic.TakeTen(todaysShowings);
            }
            else
            {
                allTenShowings = todaysShowings.ToList();
            }

            //Add the corresponding film to the list of showings,
            //so that the information of the film can be used
            List<Film> allFilms = new List<Film>();

            //Add every film that has been found in the result list of showings
            FilterLogic.AddFilms(allTenShowings, allFilms);

            if (time.Day == DateTime.Now.Day)
            {
                ViewBag.selectedDate = "Vandaag";
            }
            else
            {
                ViewBag.selectedDate = FilterLogic.GetDay(time.DayOfWeek) + " " + time.Day + "-" + time.Month;
            }

            ViewBag.selectedTime = time.ToString("HH:mm");
            ViewBag.numberOfResults = allTenShowings.Count;
            ViewBag.resultShowings = allTenShowings;
            ViewBag.resultFilms = allFilms;

            TempData["Model"] = model;
            return View("FilteredFilms", model);
        }
    }
}