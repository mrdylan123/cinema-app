﻿using Cinema.IVH7B4.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.IVH7B4.WebUI.Models
{
    public class LadiesTicketOrder : TicketOrder
    {
        public LadiesTicketOrder(CinemaViewModel model) : base(model)
        {

        }
        public override decimal GetDiscount()
        {
            return -6.95m;
        }

        public override TicketType GetTicketType()
        {
            return TicketType.LadiesTicket;
        }
    }
}