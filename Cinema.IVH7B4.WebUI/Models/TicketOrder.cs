using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.IVH7B4.WebUI.Models
{
    public abstract class TicketOrder
    {
        protected CinemaViewModel model;
        public int Quantity;

        public abstract TicketType GetTicketType();

        public TicketOrder(CinemaViewModel model)
        {
            this.model = model;
        }

        public Decimal GetPrice() {
            return GetNormalPrice() - GetDiscount();
        }

        public Decimal GetNormalPrice()
        {
            if (this.model.SelectedFilm.Length > 120 )
            {
                return 9.00m;
            }

            return 8.50m;
        }

        public Decimal GetTotalPrice()
        {
            return Quantity * GetPrice();
        }

        public abstract Decimal GetDiscount();
    }

    public enum TicketType
    {
        NormalTicket,
        ChildTicket,
        StudentTicket,
        SeniorTicket,
        InvalidTicket,
        PopcornTicket,
        LadiesTicket
    }
}