using Cinema.IVH7B4.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.IVH7B4.WebUI.Models
{
    public class PopcornTicketOrder : TicketOrder
    {
        public PopcornTicketOrder(CinemaViewModel model) : base(model)
        {

        }
        public override decimal GetDiscount()
        {
            return - 7.0m;
        }

        public override TicketType GetTicketType()
        {
            return TicketType.PopcornTicket;
        }
    }
}