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

        public ViewResult PinView(PincodeViewModel model) {
            return View("PinView" , null, model);
        }

        [HttpGet]
        public ActionResult PinViewReturn(PincodeViewModel model) {
            return RedirectToAction("PinView", null, model);
        }

        [HttpGet]
        public ActionResult PinViewRemoveChar(PincodeViewModel model) {

            if (model.PinValue.Length > 0) {
                model.PinValue = model.PinValue.Remove(model.PinValue.Length -1 );
            }
            return RedirectToAction("PinView", null, model);
        }
    }
}