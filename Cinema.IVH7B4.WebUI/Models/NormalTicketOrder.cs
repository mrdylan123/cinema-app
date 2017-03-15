using Cinema.IVH7B4.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.IVH7B4.WebUI.Models
{
    public class NormalTicketOrder : TicketOrder
    {
        public NormalTicketOrder(CinemaViewModel model) : base(model)
        {
        }

        public override TicketType GetTicketType()
        {
            return TicketType.NormalTicket;
        }

        public override decimal GetDiscount()
        {
            return 0.0m;
        }

    }
}