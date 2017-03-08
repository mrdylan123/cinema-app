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
                time = DateTime.ParseExact(dayInput, "dd-mm-yyyy", CultureInfo.InvariantCulture);
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

            /*
            // split the string by the dash
            string[] ints = dayInput.Split('-');

            // create int variables
            int y = 0;
            int d = 0;
            int m = 0;

            // parse the strings from the searchDay result to ints
            foreach (string s in ints)
            {
                int day = Int32.Parse(ints[0]);
                int month = Int32.Parse(ints[1]);
                d = day;
                m = month;
            }

            // parse the string from the searchTime result to int
            int chosentime = Int32.Parse(timeInput);

            // create a datetime object of the chosen filter parameters
            DateTime time = new DateTime(DateTime.Now.Year, m, d, chosentime, 0, 0);
            */

            var dayofweek = time.DayOfWeek;

            //First get all showings
            IEnumerable<Showing> allShowings = showingRepo.GetShowings();

            //Filter the list to only contain showings coming after the given time
            IEnumerable<Showing> filteredShowings = allShowings.Where(s => s.BeginDateTime > time);

            //Order the results by date so that the most near one will be shown first
            IEnumerable<Showing> orderedShowings = filteredShowings.OrderBy(s => s.BeginDateTime);
            
            //Filter out showings that are not on the chosen day
            IEnumerable<Showing> todaysShowings = orderedShowings.Where(s => s.BeginDateTime.DayOfWeek == dayofweek);

            //Take the first 10 showings to show on the page 
            List<Showing> resultShowings = todaysShowings.Take(10).ToList();

            //Add the corresponding film to the list of showings,
            //so that the information of the film can be used
            List<Film> allFilms = filmRepo.GetFilms().ToList();

            //Add every film that has been found in the result list of showings
            foreach (Showing s in resultShowings)
            {
                allFilms.Add(s.Film);
            }

            if (time.Day == DateTime.Now.Day)
            {
                ViewBag.selectedDate = "Vandaag";
            }
            else
            {
                ViewBag.selectedDate = time.DayOfWeek + " " + time.Day + " " + time.Month;
            }

            ViewBag.selectedTime = time.TimeOfDay.ToString("HH:mm");
            ViewBag.numberOfResults = resultShowings.Count;
            ViewBag.resultShowings = resultShowings;
            ViewBag.resultFilms = allFilms;

            TempData["Model"] = model;
            return View("FilteredFilms", model);
        }
    }
}