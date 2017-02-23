using Cinema.IVH7B4.Domain.Abstract;
using Cinema.IVH7B4.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Cinema.IVH7B4.WebUI.Models
{
    public static class FilmOverviewLogic
    {
        public static Film renderFilm(this int filmID, List<Film> filmList)
        {
            int i = 0;
            Film currentFilm = null;


            while (i < filmList.Count)
            {
                currentFilm = filmList.ElementAt(i);

                if (currentFilm.FilmID == filmID)
                {
                    return currentFilm;

                }
                else if (currentFilm.FilmID == 0)
                {
                    return currentFilm;
                }
                i++;
            }
            return currentFilm;
        }

        public static string getDutchString(this int filmType)
        {
            switch (filmType)
            {
                case 0: return "Kinder"; // 0
                case 1: return "Actie"; // 1
                case 2: return "Komedie"; // 2
                case 3: return "Drama"; // 3
                case 4: return "Horror"; // 4

                default: return "ERROR";
            }
        }

        public static string convertImageByteCode(this byte[] imageCode)
        {
            var image = @"data:image/jpg;base64," + Convert.ToBase64String(imageCode);

            return image;
        }

        public static List<string> convertDateTimeFirstFilm(this List<Film> allFilms, List<Showing> allShowings)
        {
            List<Showing> currentShowings = new List<Showing>();
            List<string> dateTimeStrings = new List<string>();
            Showing firstShowing = allShowings.ElementAt(0);
            Film firstFilm = allFilms.ElementAt(0);
            int i = 0;

            while(i < allShowings.Count)
            {
                Showing currentShowing = allShowings.ElementAt(i);

                if (currentShowing.FilmID == firstFilm.FilmID)
                {
                    currentShowings.Add(currentShowing);
                }
                i++;
            }     

            foreach (var dateTime in currentShowings)
            {
                DateTime currentBegin = dateTime.BeginDateTime;
                string dayWeek = currentBegin.DayOfWeek.ToString();
                string dayMonth = currentBegin.Day.ToString();
                string month = currentBegin.Month.ToString();
                string year = currentBegin.Year.ToString();
                string hourBegin = currentBegin.Hour.ToString();
                string minutesBegin = currentBegin.Minute.ToString();

                DateTime currentEnd = dateTime.EndDateTime;
                string hourEnd = currentEnd.Hour.ToString();
                string minutesEnd = currentEnd.Minute.ToString();

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

                dateTimeStrings.Add("Datum: " + dayWeek + " " + dayMonth + "/" + month + "/" + year + " " +
                            " " + " " + "Begintijd: " + hourBegin + minutesBegin + " " + " " + " " + "Eindtijd: " + hourEnd + minutesEnd);
            }

            if (dateTimeStrings != null)
            {
                return dateTimeStrings;
            } else
            {
                Debug.WriteLine("dateTimeStrings is null");
                throw new NullReferenceException();                
            }
        }
    }
}