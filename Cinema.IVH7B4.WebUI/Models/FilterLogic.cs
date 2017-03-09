using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cinema.IVH7B4.Domain.Entities;

namespace Cinema.IVH7B4.WebUI.Models
{
    public class FilterLogic
    {

        // Insert a list and return a list that has been filtered to only contain objects after a given DateTime
        public static IEnumerable<Showing> GetShowingsAfterDate(IEnumerable<Showing> showings, DateTime time)
        {
            IEnumerable<Showing> showingsAfterDate = showings.Where(s => s.BeginDateTime > time);

            return showingsAfterDate;
        }

        // Order the objects by date
        public static IEnumerable<Showing> OrderByDate(IEnumerable<Showing> showings)
        {
            IEnumerable<Showing> orderedShowings = showings.OrderBy(s => s.BeginDateTime);

            return orderedShowings;
        }

        // Filter the list to only contain results that are on the chosen day
        public static IEnumerable<Showing> TodaysShowings(IEnumerable<Showing> showings, DateTime time)
        {
            IEnumerable<Showing> showingsChosenDay = showings.Where(s => s.BeginDateTime.DayOfWeek == time.DayOfWeek);

            return showingsChosenDay;
        }

        // Only take first 10 results of current list
        public static List<Showing> TakeTen(IEnumerable<Showing> showings)
        {
            List<Showing> tenShowings = showings.Take(10).ToList();

            return tenShowings;           
        }

        // Add the films to the filmlist for every result in the showings list
        public static void AddFilms(List<Showing> showings, List<Film> films)
        {
            foreach (var s in showings)
            {
                films.Add(s.Film);
            }
        }


    }
}