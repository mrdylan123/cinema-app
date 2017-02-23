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

        public List<Film> getFilmList()
        {
            return context.Films.ToList();
        }

        public List<Showing> getShowingList()
        {
            return context.Showings.ToList();
        }

        public List<Showing> getShowingbyId(int id)
        {
            int i = 0;
            List<Showing> showingList = context.Showings.ToList();
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
            foreach (var dateTime in getShowingbyId(filmID))
            {
                DateTime currentBegin = dateTime.BeginDateTime;
                string dayWeek = currentBegin.DayOfWeek.ToString();
                string dayMonth = currentBegin.Day.ToString();
                string month = currentBegin.Month.ToString();
                string year = currentBegin.Year.ToString();
                string hourBegin = currentBegin.Hour.ToString("D2");
                string minutesBegin = currentBegin.Minute.ToString("D2");

                DateTime currentEnd = dateTime.EndDateTime;
                string hourEnd = currentEnd.Hour.ToString("D2");
                string minutesEnd = currentEnd.Minute.ToString("D2");

            switch (dayWeek)
            {
                case "Monday":
                    dayWeek = "maandag";
                    break;
                case "Tuesday":
                    dayWeek = "dinsdag";
                    break;
                case "Wednesday":
                    dayWeek = "woensdag";
                    break;
                case "Thursday":
                    dayWeek = "donerdag";
                    break;
                case "Friday":
                    dayWeek = "vrijdag";
                    break;
                case "Saturday":
                    dayWeek = "zaterdag";
                    break;
                case "Sunday":
                    dayWeek = "zondag";
                    break;
            }

            currentShowings.Add("Datum: " + dayWeek + " " + dayMonth + "/" + month + "/" + year + " " +
                            " " + " " + "Begintijd: " + hourBegin + ":" + minutesBegin + " " + " " + " " + "Eindtijd: " + hourEnd + ":" + minutesEnd);
            }
            return currentShowings;
        }
        
    }
}
