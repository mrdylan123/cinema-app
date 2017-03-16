using Cinema.IVH7B4.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.IVH7B4.Domain.Entities;
using Cinema.IVH7B4.Domain.Concrete;
using System.Diagnostics;
using TaskExtensions = System.Data.Entity.SqlServer.Utilities.TaskExtensions;

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

            foreach (Film f in list)
            {
                Showing s = showingsList.Where(sh => sh.FilmID == f.FilmID && sh.BeginDateTime >= DateTime.Now).
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
                if (currentShowing.FilmID == id)
                {
                    DateTime currentFilm = currentShowing.BeginDateTime;
                    string hour = currentFilm.Hour.ToString();

                    showingListById.Add(currentShowing);
                }
                i++;
            }

            if (showingListById != null)
            {
                return showingListById;
            }
            else
            {
                throw new Exception("Er is iets misgegaan");
            }
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
