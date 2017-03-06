using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema.IVH7B4.Domain.Abstract;
using Cinema.IVH7B4.Domain.Concrete;
using Cinema.IVH7B4.Domain.Entities;
using Cinema.IVH7B4.WebUI.Models;
using Ninject.Infrastructure.Language;

namespace Cinema.IVH7B4.WebUI.Controllers
{
    public class UpcomingFilmController : Controller
    {
        private IFilmRepository iFilmRepository;
        private IShowingRepository iShowingRepository;

        public UpcomingFilmController(IFilmRepository iFilmRepository, IShowingRepository iShowingRepository)
        {
            this.iFilmRepository = iFilmRepository;
            this.iShowingRepository = iShowingRepository;
        }


        // GET: UpcomingFilm
        public ActionResult UpcomingFilms()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            DateTime now = DateTime.Now;

            List<Showing> allShowings = iShowingRepository.GetShowings().ToList();

            //Filter out showings from the past
            List<Showing> ShowingsFromNow = allShowings.ToEnumerable()
                .Where(s => s.BeginDateTime > now).ToList();

            //Order by showing date -> newest first
            List<Showing> ShowingsFromNowOrderedByDate = ShowingsFromNow.ToEnumerable()
                .OrderBy(s => s.BeginDateTime).ToList();
                
            //Filter out showings with duplicate FilmID's (Multiple showings of the same film)
            List<Showing> UniqueShowingsFromNowOrderedByDate = ShowingsFromNowOrderedByDate.ToEnumerable()
                .GroupBy(s => s.FilmID).Select(group => group.First()).ToList();

            //Only take the first 6 results to show to the user
            List<Showing> FirstSixUniqueShowingsFromNowOrderedByDate = UniqueShowingsFromNowOrderedByDate.ToEnumerable()
               .Take(6).ToList();


            List<Film> ShowingsFilms = new List<Film>();

            foreach (Showing s in FirstSixUniqueShowingsFromNowOrderedByDate)
            {
                ShowingsFilms.Add(s.Film);
            }

            DateTime today = DateTime.Now;
            string DayOfWeek = today.DayOfWeek.ToString();

            DateTime time = DateTime.Now;
            string HourOfDay = time.Hour.ToString();
            string MinuteOfDay = time.Minute.ToString();
            
            ViewBag.Films = ShowingsFilms;
            ViewBag.Showings = FirstSixUniqueShowingsFromNowOrderedByDate;
            ViewBag.DayOfWeek = DayOfWeek;
            ViewBag.HourOfDay = HourOfDay;
            ViewBag.MinuteOfDay = MinuteOfDay;

            TempData["model"] = model;
            return View(model);
        }



        public ActionResult Redirect(int f, int s)
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];
            Film film = iFilmRepository.GetFilms().First(fl => fl.FilmID == f);
            Showing showing = iShowingRepository.GetShowings().First(sh => sh.ShowingID == s);
            model.SelectedFilm = film;
            model.SelectedShowing = showing;

            TempData["model"] = model;
            return RedirectToAction("RateOverview", "Rate");
        }

    }
}