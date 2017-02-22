using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema.IVH7B4.WebUI.Models;

namespace Cinema.IVH7B4.WebUI.Controllers {
    public class PinController : Controller {

        public PinController() {
            
        }

        public ViewResult PinView() {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null) {
                model = new CinemaViewModel();
            }


            TempData["model"] = model;
            return View("PinView",  model);
        }

        [HttpGet]
        public ActionResult PinViewAddchar(String s) {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            model.PinValue += s;

            TempData["model"] = model;
            return RedirectToAction("PinView");
        }

        [HttpGet]
        public ActionResult PinViewRemoveChar() {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            model.PinRemoveChar();

            TempData["model"] = model;
            return RedirectToAction("PinView");
        }
    }
}