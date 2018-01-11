using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using Cinema.IVH7B4.Domain.Abstract;
using Cinema.IVH7B4.Domain.Concrete;
using Cinema.IVH7B4.Domain.Entities;
using Cinema.IVH7B4.WebUI.Models;


namespace Cinema.IVH7B4.WebUI.Controllers
{
    public class CashRegisterController : Controller
    {
        private ICashRegisterLoginRepository LoginRepo;
        private ILostAndFoundRepository LostAndFoundRepo;

        public CashRegisterController(ICashRegisterLoginRepository LoginRepo, ILostAndFoundRepository LostAndFoundRepo)
        {
            this.LoginRepo = LoginRepo;
            this.LostAndFoundRepo = LostAndFoundRepo;
        }

        // Check login credentials and show dashboard
        public ActionResult Dashboard()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];
            // Request input from cash register login
            string username = Request["username"];
            string password = Request["password"];

            // No user input
            if (username == "" || password == "")
            {
                    return RedirectToAction("CashRegisterEmployeeLogin", "Login", new {error = "Vul a.u.b. beide velden in"});
            }
            else
            {
                // Get all logins
                IEnumerable<CashRegisterLogin> allLogins = LoginRepo.GetLogins();

                // Check if username and password exist in database
                bool exists = LoginLogic.CheckCashRegisterLogin(username, password, allLogins);

                if (exists == true)
                {
                    model.LoggedInUser = username;
                    model.LoggedInCashRegister = true;
                    TempData["model"] = model;
                    return View("Dashboard", model);
                }
                // No right username - password combination
                else
                {
                    return RedirectToAction("CashRegisterEmployeeLogin", "Login",
                        new {error = "Deze combinatie van gebruikersnaam en wachtwoord kon niet gevonden worden"});
                }
            }
        }

        /**
         *  Lost and found functionalities
         */  
        public ActionResult ManageLostAndFound()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            if (model.LoggedInCashRegister == false)
            {
                return RedirectToAction("CashRegisterEmployeeLogin", "Login",
                        new { error = "Log in om deze pagina te bezoeken." });
            }

            TempData["model"] = model;

            return View("ManageLostAndFound", model);
        }

        public ActionResult AddLostAndFound(string error, string confirmation)
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            if (error != null)
            {
                ViewBag.AddError = error;
            }

            if (confirmation != null)
            {
                ViewBag.AddConfirmation = confirmation;
            }


            if (model.LoggedInCashRegister == false)
            {
                return RedirectToAction("CashRegisterEmployeeLogin", "Login",
                        new { error = "Log in om deze pagina te bezoeken." });
            }

            TempData["model"] = model;

            return View("AddLostAndFound", model);
        }

        public ActionResult AddObject()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            if (model.LoggedInCashRegister == false)
            {
                return RedirectToAction("CashRegisterEmployeeLogin", "Login",
                        new { error = "Log in om deze pagina te bezoeken." });
            }

            // Retrieve the input
            string datestring = Request["date"];
            string objectstring = Request["object"];
            string finder = Request["finder"];
            string owner = Request["owner"];
            string roomnumberstring = Request["roomnumber"];
            string locationstring = Request["location"];

            int roomnumber;
            int location;
            DateTime date;

            // Check if the required fields have been filled in
            if (datestring == "" || objectstring == "" || roomnumberstring == "" || locationstring == "")
            {
                return RedirectToAction("AddLostAndFound", "CashRegister",
                    new {error = "Vul a.u.b. alle verplichte velden in"});
            }

            // Try convert strings to int and start insertion into database
            if (int.TryParse(roomnumberstring, out roomnumber) && int.TryParse(locationstring, out location))
            {
                date = DateTime.ParseExact(datestring, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                var context = new EFDbContext();

                var lostorfoundobject = new LostAndFound()
                {
                    Date = date,
                    Finder = finder,
                    Location = location,
                    Object = objectstring,
                    Owner = owner,
                    RoomNumber = roomnumber
                };

                context.LostAndFounds.Add(lostorfoundobject);
                context.SaveChanges();


                return RedirectToAction("AddLostAndFound", "CashRegister",
                    new {confirmation = "Gevonden of verloren voorwerp succesvol toegevoegd"});
            }

            else
            {
                return RedirectToAction("AddLostAndFound", "CashRegister",
                    new { error = "Er is iets fout gegaan, probeer het later opnieuw" });
            }
        }

        public ActionResult FindLostAndFound(string error, string confirmation)
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            if (error != null)
            {
                ViewBag.FindError = error;
            }

            if (model.LoggedInCashRegister == false)
            {
                return RedirectToAction("CashRegisterEmployeeLogin", "Login",
                        new { error = "Log in om deze pagina te bezoeken." });
            }

            TempData["model"] = model;

            return View("FindLostAndFound", model);
        }

        public ActionResult FindObject()
        {
            CinemaViewModel model = (CinemaViewModel)TempData["model"];

            if (model == null)
            {
                model = new CinemaViewModel();
            }

            if (model.LoggedInCashRegister == false)
            {
                return RedirectToAction("CashRegisterEmployeeLogin", "Login",
                        new { error = "Log in om deze pagina te bezoeken." });
            } 

            // Retrieve the input
            string datestring = Request["date"];
            string objectstring = Request["object"];
            string roomnumberstring = Request["roomnumber"];

            int roomnumber;
            DateTime date;

            // Check if at least one of the search fields have been filled in
            if (datestring == "" && objectstring == "" && roomnumberstring == "")
            {
                return RedirectToAction("FindLostAndFound", "CashRegister",
                    new { error = "Vul a.u.b. ten minste één filter in" });
            }


            // If all filters have been filled in..
            if (datestring != "" && objectstring != "" && roomnumberstring != "")
            {
                // Convert date to DateTime object
                date = DateTime.ParseExact(datestring, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                // Convert string to int
                int.TryParse(roomnumberstring, out roomnumber);

                // First get all lost and found objects
                IEnumerable<LostAndFound> objects = LostAndFoundRepo.GetLostAndFounds();

                // Filter by chosen date
                IEnumerable<LostAndFound> filteredByDate = FindLostAndFoundLogic.SearchByDate(objects, date);

                // Filter by chosen roomnumber
                IEnumerable<LostAndFound> filteredByDateAndRoomNumber = FindLostAndFoundLogic.SearchByRoom(filteredByDate, roomnumber);

                // Filter by put in object name
                List<LostAndFound> result = FindLostAndFoundLogic.SearchByObject(filteredByDateAndRoomNumber, objectstring).ToList();

                int results = result.Count();

                // Check if the search query has returned any results
                if (results > 0)
                {
                    ViewBag.numberOfResults = results;
                    ViewBag.resultList = result;
                    return View("SearchResults");
                }
                else
                {
                    return RedirectToAction("FindLostAndFound", "CashRegister",
                    new { error = "Er zijn bij deze zoekopdracht geen resultaten gevonden" });
                }
            }


            // If only the date filter has been filled in..
            if (datestring != "" && objectstring == "" && roomnumberstring == "")
            {
                // Convert date to DateTime object
                date = DateTime.ParseExact(datestring, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                // First get all lost and found objects
                IEnumerable<LostAndFound> objects = LostAndFoundRepo.GetLostAndFounds();

                // Filter by chosen date
                List<LostAndFound> result = FindLostAndFoundLogic.SearchByDate(objects, date).ToList();

                int results = result.Count();

                // Check if the search query has returned any results
                if (results > 0)
                {
                    ViewBag.numberOfResults = results;
                    ViewBag.resultList = result;
                    return View("SearchResults");
                }
                else
                {
                    return RedirectToAction("FindLostAndFound", "CashRegister",
                    new { error = "Er zijn bij deze zoekopdracht geen resultaten gevonden" });
                }
            }


            // If only the object name filter has been filled in..
            if (datestring == "" && objectstring != "" && roomnumberstring == "")
            {
                // First get all lost and found objects
                IEnumerable<LostAndFound> objects = LostAndFoundRepo.GetLostAndFounds();

                // Filter by put in object name
                List<LostAndFound> result = FindLostAndFoundLogic.SearchByObject(objects, objectstring).ToList();

                int results = result.Count();

                // Check if the search query has returned any results
                if (results > 0)
                {
                    ViewBag.numberOfResults = results;
                    ViewBag.resultList = result;
                    return View("SearchResults");
                }
                else
                {
                    return RedirectToAction("FindLostAndFound", "CashRegister",
                    new { error = "Er zijn bij deze zoekopdracht geen resultaten gevonden" });
                }
            }


            // If only the room number filter has been filled in..
            if (datestring == "" && objectstring == "" && roomnumberstring != "")
            {
                // First get all lost and found objects
                IEnumerable<LostAndFound> objects = LostAndFoundRepo.GetLostAndFounds();

                // Convert string to int
                int.TryParse(roomnumberstring, out roomnumber);

                // Filter by chosen roomnumber
                List<LostAndFound> result = FindLostAndFoundLogic.SearchByRoom(objects, roomnumber).ToList();

                int results = result.Count();

                // Check if the search query has returned any results
                if (results > 0)
                {
                    ViewBag.numberOfResults = results;
                    ViewBag.resultList = result;
                    return View("SearchResults");
                }
                else
                {
                    return RedirectToAction("FindLostAndFound", "CashRegister",
                    new { error = "Er zijn bij deze zoekopdracht geen resultaten gevonden" });
                }
            }


            // If date and object name filters have been filled in..
            if (datestring != "" && objectstring != "" && roomnumberstring == "")
            {
                // Convert date to DateTime object
                date = DateTime.ParseExact(datestring, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                // First get all lost and found objects
                IEnumerable<LostAndFound> objects = LostAndFoundRepo.GetLostAndFounds();

                // Filter by chosen date
                IEnumerable<LostAndFound> filterByDate = FindLostAndFoundLogic.SearchByDate(objects, date);

                // Filter by put in object name
                List<LostAndFound> result = FindLostAndFoundLogic.SearchByObject(filterByDate, objectstring).ToList();

                int results = result.Count();

                // Check if the search query has returned any results
                if (results > 0)
                {
                    ViewBag.numberOfResults = results;
                    ViewBag.resultList = result;
                    return View("SearchResults");
                }
                else
                {
                    return RedirectToAction("FindLostAndFound", "CashRegister",
                    new { error = "Er zijn bij deze zoekopdracht geen resultaten gevonden" });
                }
            }


            // If date and room number filters have been filled in..
            if (datestring != "" && objectstring == "" && roomnumberstring != "")
            {
                // Convert date to DateTime object
                date = DateTime.ParseExact(datestring, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                // First get all lost and found objects
                IEnumerable<LostAndFound> objects = LostAndFoundRepo.GetLostAndFounds();

                // Filter by chosen date
                IEnumerable<LostAndFound> filterByDate = FindLostAndFoundLogic.SearchByDate(objects, date);

                // Convert string to int
                int.TryParse(roomnumberstring, out roomnumber);

                // Filter by chosen roomnumber
                List<LostAndFound> result = FindLostAndFoundLogic.SearchByRoom(filterByDate, roomnumber).ToList();

                int results = result.Count();

                // Check if the search query has returned any results
                if (results > 0)
                {
                    ViewBag.numberOfResults = results;
                    ViewBag.resultList = result;
                    return View("SearchResults");
                }
                else
                {
                    return RedirectToAction("FindLostAndFound", "CashRegister",
                    new { error = "Er zijn bij deze zoekopdracht geen resultaten gevonden" });
                }
            }


            // If object name and room number have been filled in..
            if (datestring == "" && objectstring != "" && roomnumberstring != "")
            {
                // First get all lost and found objects
                IEnumerable<LostAndFound> objects = LostAndFoundRepo.GetLostAndFounds();

                // Filter by put in object name
                IEnumerable<LostAndFound> filterByName = FindLostAndFoundLogic.SearchByObject(objects, objectstring);

                // Convert string to int
                int.TryParse(roomnumberstring, out roomnumber);

                // Filter by chosen roomnumber
                List<LostAndFound> result = FindLostAndFoundLogic.SearchByRoom(filterByName, roomnumber).ToList();

                int results = result.Count();

                // Check if the search query has returned any results
                if (results > 0)
                {
                    ViewBag.numberOfResults = results;
                    ViewBag.resultList = result;
                    return View("SearchResults");
                }
                else
                {
                    return RedirectToAction("FindLostAndFound", "CashRegister",
                    new { error = "Er zijn bij deze zoekopdracht geen resultaten gevonden" });
                }
            }

            else
            {
                return RedirectToAction("FindLostAndFound", "CashRegister",
                    new { error = "Er is iets fout gegaan, probeer het later opnieuw" });
            }
        }

        /**
         *  End Lost and found functionalities
         */


        /**
         *  Subscriptions etc functionalities
         */
        //TODO: 
    }
}