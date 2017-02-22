using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.IVH7B4.WebUI.Models
{
    public class ChildTicketOrder : TicketOrder
    {
        public ChildTicketOrder(CinemaViewModel model) : base(model)
        {
        }

        public override TicketType GetTicketType() { return TicketType.ChildTicket; }

        public override Decimal GetDiscount() {

            if (model.SelectedFilm.Language == "NL" && model.SelectedShowing.EndDateTime.Hour < 18
                && model.SelectedFilm.FilmType == (int)FilmType.Children)
            {
                return 1.5m;
            }
            return 0.0m;
        }
    }
}