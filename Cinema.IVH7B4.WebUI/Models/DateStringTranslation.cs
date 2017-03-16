using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cinema.IVH7B4.Domain.Concrete;
using Cinema.IVH7B4.Domain.Entities;

namespace Cinema.IVH7B4.WebUI.Models {
    public class DateStringTranslation {
        FilmOverviewRepository repo = new FilmOverviewRepository();

        public virtual List<string> convertDateTime(int filmID) {
            List<string> currentShowings = new List<string>();
            List<Showing> showingList = repo.getShowingbyId(filmID);

            showingList = (from e in showingList
                orderby e.BeginDateTime, e.BeginDateTime
                select e).ToList();

            foreach (Showing showing in showingList) {
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
                if (currentBegin <= FilmOverviewRepository.getNextWeekday(DateTime.Now, DayOfWeek.Thursday) &&
                    currentBegin.Day - DateTime.Now.Day <= 7) {
                    switch (dayWeek) {
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

                    currentShowings.Add(dayWeek + " " + dayMonth + "/" + month + "/" + year + " " +
                                " " + " " + Resources.Global.FilmOverView_FilmBeginTime+ " " + hourBegin + ":" + minutesBegin + " " + " " + " " + Resources.Global.FilmOverView_FilmEndTime + " " + hourEnd + ":" + minutesEnd + " " + Resources.Global.FilmOverView_FilmRoom+" " + showing.Room.RoomNumber);
                }
            }
            return currentShowings;
        }
    }
}