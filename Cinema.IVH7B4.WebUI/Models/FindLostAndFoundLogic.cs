using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cinema.IVH7B4.Domain.Entities;

namespace Cinema.IVH7B4.WebUI.Models
{
    public class FindLostAndFoundLogic
    {
        // Filter a list of lost and found objects by date
        public static IEnumerable<LostAndFound> SearchByDate(IEnumerable<LostAndFound> objects, DateTime date)
        {
            IEnumerable<LostAndFound> searchResult = objects.Where(f => f.Date == date);
            return searchResult;
        }

        // Filter a list of lost and found objects by room number
        public static IEnumerable<LostAndFound> SearchByRoom(IEnumerable<LostAndFound> objects, int roomNumber)
        {
            IEnumerable<LostAndFound> searchResult = objects.Where(f => f.RoomNumber == roomNumber);
            return searchResult;
        }

        // Filter a list of lost and found objects by the object name
        public static IEnumerable<LostAndFound> SearchByObject(IEnumerable<LostAndFound> objects, string searchString)
        {
            // Set search string to lowercase and put object names in database to lowercase aswell so that uppercase letters won't cause a problem
            string lowerSS = searchString.ToLower();
            IEnumerable<LostAndFound> searchResult = objects.Where(f => f.Object.ToLower().StartsWith(lowerSS));
            return searchResult;
        }
    }
}