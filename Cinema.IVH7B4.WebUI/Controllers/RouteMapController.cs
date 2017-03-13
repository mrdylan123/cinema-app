using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cinema.IVH7B4.Domain.Concrete;

namespace Cinema.IVH7B4.WebUI.Controllers
{
    public class RouteMapController : Controller
    {
        // GET: RouteMap
        public ActionResult RouteMap()
        {
            ViewBag.Locations = new EFDbContext().Locations.ToList();
            return View();
        }
    }
}