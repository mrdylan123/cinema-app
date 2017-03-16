using Cinema.IVH7B4.Domain.Abstract;
using Cinema.IVH7B4.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema.IVH7B4.WebUI.Models;
using Ninject.Infrastructure.Language;

namespace Cinema.IVH7B4.WebUI.Controllers
{
    public class FilmOverviewController : Controller
    {
        public IFilmOverviewRepository repo;

        public FilmOverviewController(IFilmOverviewRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public ActionResult filmOverview()
        {
            ViewBag.filmList = repo.getFilmList();

            var DateTimeAndIDList = new List<DateTimeAndID>();

            int i = 0;
            foreach (string s in Models.FilmOverviewLogic.convertDateTimeFirstFilm(repo.getFilmList(), repo.getShowingList()))
            {

                DateTimeAndIDList.Add(new DateTimeAndID()
                {
                    str = s,
                    index = i
                });
                i++;
            }

            ViewBag.firstDateTime = DateTimeAndIDList;

            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            SetModelStuff(model, repo.getFilmList()[0]);
            TempData["model"] = model;

            return View(model);
        }

        [HttpGet]
        public ViewResult renderFilm(int filmID)
        {
            CinemaViewModel model;
            model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            List<Film> filmList = repo.getFilmList();
            ViewBag.currentFilm = FilmOverviewLogic.renderFilm(filmID, filmList);
            ViewBag.firstDateTime = FilmOverviewLogic.convertDateTimeFirstFilm(filmList, repo.getShowingList());

            ViewBag.filmList = repo.getFilmList();
            var foundFilm = (filmList.ToEnumerable().Where(f => f.FilmID == filmID));
            foreach (Film f in foundFilm)
            {
                if (f.Image != null)
                {
                    ViewBag.image = @"data:image/jpg;base64," +
                                    Convert.ToBase64String(Models.FilmOverviewLogic.renderFilm(filmID, filmList).Image);
                }
            }
            var DateTimeAndIDList = new List<DateTimeAndID>();

            if (filmID == -1)
            {
                ViewBag.isNull = "GEEN RESULTATEN GEVONDEN";
            }
            else
            {
                ViewBag.isNull = "";
            }

            int i = 0;
            foreach (string s in repo.convertDateTime(filmID))
            {

                DateTimeAndIDList.Add(new DateTimeAndID()
                {
                    str = s,
                    index = i
                });
                i++;
            }

            ViewBag.dateTime = DateTimeAndIDList;

            SetModelStuff(model, ViewBag.currentFilm);
            TempData["model"] = model;
            return View("filmOverview");
        }

        [HttpPost]
        public ActionResult searchFilm()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];
            FilmVerifier searchVerifier = new FilmVerifier();
            string inputString = Request["searchFilm"];
            Film searchResult = searchVerifier.verify(inputString, repo.getFilmList());

            if (searchResult != null)
            {
                SetModelStuff(model, searchResult);
                TempData["model"] = model;

                //andere controller aanroepen
                return RedirectToAction("renderFilm", new { filmID = searchResult.FilmID });
            }
            else
            {
                return RedirectToAction("renderFilm", new { filmID = -1 });
            }

        }

        [HttpGet]
        public ActionResult SelectShowing(int index)
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];
            var Showing = repo.getShowingbyId(model.SelectedFilm.FilmID)[index];


            model.SelectedShowing = Showing;

            TempData["model"] = model;
            return RedirectToAction("RateOverView", "Rate");
        }

        private void SetModelStuff(CinemaViewModel model, Film f)
        {
            if (model != null)
            {
                model.SelectedFilm = f;
                // model.SelectedShowing = repo.getShowingList().Where(s => s.FilmID == model.SelectedFilm.FilmID && s.BeginDateTime > DateTime.Now).OrderBy(s => s.BeginDateTime).First();
            }
            else
            {
                model = new CinemaViewModel();
                model.SelectedFilm = f;
                //model.SelectedShowing = repo.getShowingList().Where(s => s.FilmID == model.SelectedFilm.FilmID && s.BeginDateTime > DateTime.Now).OrderBy(s => s.BeginDateTime).FirstOrDefault(null);
            }
        }
    }
}