using Cinema.IVH7B4.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.IVH7B4.Domain.Entities;
using Cinema.IVH7B4.Domain.Concrete;
using System.Diagnostics;

namespace Cinema.IVH7B4.Domain.Concrete
{
    public class FilmOverviewRepository : IFilmOverviewRepository
    {
        private EFDbContext context = new EFDbContext();

        public FilmOverviewRepository() { }

        public virtual List<Film> getFilmList()
        {
            List<Film> list = context.Films.ToList();
            List<Showing> showingsList = context.Showings.ToList();
            List<Film> retList = new List<Film>();

            foreach(Film f in list)
            {
                Showing s = showingsList.Where(sh => sh.FilmID == f.FilmID && sh.BeginDateTime  >= DateTime.Now).
                    FirstOrDefault();             

                if (s != null)
                {
                    retList.Add(f);
                }
            }

            return retList;
        }

        public virtual List<Showing> getShowingList()
        {
            return context.Showings.ToList();
        }

        public List<Showing> getShowingbyId(int id)
        {
            int i = 0;
            List<Showing> showingList = getShowingList();
            List<Showing> showingListById = new List<Showing>();
            while (i < showingList.Count)
            {
                Showing currentShowing = showingList.ElementAt(i);
                if(currentShowing.FilmID == id)
                {
                    DateTime currentFilm = currentShowing.BeginDateTime;
                    string hour = currentFilm.Hour.ToString();

                    showingListById.Add(currentShowing);
                }
                i++;
            }

            if(showingListById != null)
            {
                return showingListById;
            } else
            {
                throw new Exception("Er is iets misgegaan");
            }
        }

        public List<string> convertDateTime(int filmID)
        {
            List<string> currentShowings = new List<string>();
            List<Showing> showingList = getShowingbyId(filmID);

            showingList = (from e in showingList
                               orderby e.BeginDateTime, e.BeginDateTime
                               select e).ToList();

            foreach (Showing showing in showingList)
            {
                int dateCheck = DateTime.Compare(showing.BeginDateTime, DateTime.Now);
                int dayCheck = showing.BeginDateTime.Day;
                int monthCheck = showing.BeginDateTime.Month;

                DateTime currentBegin = showing.BeginDateTime;
                string dayWeek = currentBegin.DayOfWeek.ToString();
                string dayMonth = currentBegin.Day.ToString();
                string month = currentBegin.Month.ToString();
                string year = currentBegin.Year.ToString();
                string hourBegin = currentBegin.Hour.ToString("D2");
                string minutesBegin = currentBegin.Minute.ToString("D2");

                DateTime currentEnd = showing.EndDateTime;
                string hourEnd = currentEnd.Hour.ToString("D2");
                string minutesEnd = currentEnd.Minute.ToString("D2");

                if (currentBegin <= getNextWeekday(DateTime.Now, DayOfWeek.Thursday) && currentBegin.Day - DateTime.Now.Day <= 7)
                {
                    switch (dayWeek)
                    {
                        case "Monday":
                            dayWeek = "Maandag";
                            break;
                        case "Tuesday":
                            dayWeek = "Dinsdag";
                            break;
                        case "Wednesday":
                            dayWeek = "Woensdag";
                            break;
                        case "Thursday":
                            dayWeek = "Donderdag";
                            break;
                        case "Friday":
                            dayWeek = "Vrijdag";
                            break;
                        case "Saturday":
                            dayWeek = "Zaterdag";
                            break;
                        case "Sunday":
                            dayWeek = "Zondag";
                            break;
                    }

                    currentShowings.Add(dayWeek + " " + dayMonth + "/" + month + "/" + year + " " +
                                    " " + " " + "Begintijd: " + hourBegin + ":" + minutesBegin + " " + " " + " " + "Eindtijd: " + hourEnd + ":" + minutesEnd + " " + "Zaalnummer: " + showing.Room.RoomNumber);
                }
            }
            return currentShowings;
        }

        public static DateTime getNextWeekday(DateTime start, DayOfWeek day)
        {
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            DateTime newDate = start.AddDays(daysToAdd);

            return newDate;
        }

    }
}
