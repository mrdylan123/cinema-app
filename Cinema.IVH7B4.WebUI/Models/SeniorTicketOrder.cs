using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.IVH7B4.WebUI.Models
{
    public class SeniorTicketOrder : TicketOrder
    {
        public SeniorTicketOrder(CinemaViewModel model) : base(model)
        {
        }

        public override TicketType GetTicketType() { return TicketType.SeniorTicket; }

        public override Decimal GetDiscount()
        {
            DayOfWeek sd = model.SelectedShowing.BeginDateTime.DayOfWeek;
            int DayOfYear = model.SelectedShowing.BeginDateTime.DayOfYear;

            if (model.IsHoliday(DayOfYear))
            {
                return 0.0m;
            }

            if (model.IsMondayTuesdayWednesdayThursday(sd))
            {
                return 1.5m;
            }

            return 0.0m;
        }
    }
}