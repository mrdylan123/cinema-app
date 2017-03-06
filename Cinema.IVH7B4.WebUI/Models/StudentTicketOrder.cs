using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.IVH7B4.WebUI.Models
{
    public class StudentTicketOrder : TicketOrder
    {
        public StudentTicketOrder(CinemaViewModel model) : base(model)
        {
        }

        public override TicketType GetTicketType() { return TicketType.StudentTicket; }

        public override Decimal GetDiscount()
        {
            DayOfWeek sd = model.SelectedShowing.BeginDateTime.DayOfWeek;

            if (model.IsMondayTuesdayWednesdayThursday(sd) == true)
            {
                return 1.5m;
            }
            return 0.0m;
        }
    }
}