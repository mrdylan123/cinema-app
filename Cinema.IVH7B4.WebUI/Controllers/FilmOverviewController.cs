using Cinema.IVH7B4.Domain.Abstract;
using Cinema.IVH7B4.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinema.IVH7B4.WebUI.Controllers
{
    

    public class FilmOverviewController : Controller
    {
        private IFilmOverviewRepository repo;

        public FilmOverviewController(IFilmOverviewRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public ActionResult filmOverview()
        {
            ViewBag.filmList = repo.getFilmList();
            ViewBag.firstDateTime = Models.FilmOverviewLogic.convertDateTimeFirstFilm(repo.getFilmList(), repo.getShowingList());

            return View();
        }

        [HttpGet]
        public ViewResult renderFilm(int filmID)
        {
            List<Film> filmList = repo.getFilmList();
            ViewBag.currentFilm = Models.FilmOverviewLogic.renderFilm(filmID, filmList);
            ViewBag.firstDateTime = Models.FilmOverviewLogic.convertDateTimeFirstFilm(filmList, repo.getShowingList()); 
            ViewBag.filmList = repo.getFilmList();
            ViewBag.image = @"data:image/jpg;base64," + Convert.ToBase64String(Models.FilmOverviewLogic.renderFilm(filmID, filmList).Image);
            ViewBag.dateTime = repo.convertDateTime(filmID);

            return View("filmOverview");
        }
    }
}