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

            public static String getDutchString(this int ft) {
                switch ((FilmType)ft) {
                    case FilmType.Children: return Resources.Global.FilmOverView_Gerne_Children; // 0
                    case FilmType.Action: return Resources.Global.FilmOverView_Gerne_Action; // 1
                    case FilmType.Comedy: return Resources.Global.FilmOverView_Gerne_Comedy; // 2
                    case FilmType.Drama: return "Drama"; // 3
                    case FilmType.Horror: return "Horror"; // 4

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

            while (i < allShowings.Count)
            {
                Showing currentShowing = allShowings.ElementAt(i);

                if (currentShowing.FilmID == firstFilm.FilmID)
                {
                    currentShowings.Add(currentShowing);
                }
                i++;
            }

            currentShowings = (from e in currentShowings
                    orderby e.BeginDateTime, e.BeginDateTime
                    select e).ToList();

            foreach (var showing in currentShowings)
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

                //check if film is not in the past and if film is in current week
                if (currentBegin <= getNextWeekday(DateTime.Now, DayOfWeek.Thursday) && currentBegin.Day - DateTime.Now.Day <= 7 && currentBegin.Day - DateTime.Now.Day > 0)
                {
                    switch (dayWeek)
                    {
                        case "Monday":
                            dayWeek = Resources.Global.FilmOverView_WeekDayMonday;
                            break;
                        case "Tuesday":
                            dayWeek = Resources.Global.FilmOverView_WeekDayTuesday;
                            break;
                        case "Wednesday":
                            dayWeek = Resources.Global.FilmOverView_WeekDayWednesday;
                            break;
                        case "Thursday":
                            dayWeek = Resources.Global.FilmOverView_WeekDayThursday;
                            break;
                        case "Friday":
                            dayWeek = Resources.Global.FilmOverView_WeekDayFriday;
                            break;
                        case "Saturday":
                            dayWeek = Resources.Global.FilmOverView_WeekDaySaterday;
                            break;
                        case "Sunday":
                            dayWeek = Resources.Global.FilmOverView_WeekDaySunday;
                            break;
                    }

                    dateTimeStrings.Add(dayWeek + " " + dayMonth + "/" + month + "/" + year + " " +
                                " " + " " + Resources.Global.FilmOverView_FilmBeginTime+ " " + hourBegin + ":" + minutesBegin + " " + " " + " " + Resources.Global.FilmOverView_FilmEndTime + " " + hourEnd + ":" + minutesEnd + " " + Resources.Global.FilmOverView_FilmRoom+" " + showing.Room.RoomNumber);
                }
            }

            if (dateTimeStrings != null)
            {
                return dateTimeStrings;
            }
            else
            {
                Debug.WriteLine("dateTimeStrings is null");
                throw new NullReferenceException();
            }
        }

        public static string dateToString(DateTime dateTime)
        {
            return "" + dateTime.ToString("dd-MM-yyyy");
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