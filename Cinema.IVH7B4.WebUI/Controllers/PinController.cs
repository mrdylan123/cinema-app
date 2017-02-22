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

        public ViewResult PinView(CinemaViewModel model) {
            return View("PinView" , null, model);
        }

        [HttpGet]
        public ActionResult PinViewReturn(CinemaViewModel model) {
            return RedirectToAction("PinView", null, model);
        }

        [HttpGet]
        public ActionResult PinViewRemoveChar(CinemaViewModel model) {
            model.PinRemoveChar();
            return RedirectToAction("PinView", null, model);
        }
    }
}