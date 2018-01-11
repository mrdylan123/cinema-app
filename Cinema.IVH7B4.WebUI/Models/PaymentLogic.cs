using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cinema.IVH7B4.Domain.Entities;

namespace Cinema.IVH7B4.WebUI.Models
{
    public class PaymentLogic
    {
        // Check the remaining value of a gift card
        public decimal RemainingValue(decimal value, decimal price)
        {
            decimal remainingvalue = value - price;
            return remainingvalue;        
        }

        // Check the remaining price after a gift card has been fully used
        public decimal RemainingPrice(decimal value, decimal price)
        {
            decimal remainingprice = price - value;
            return remainingprice;
        }

        public TenRidesCard FindCard(int code, List<TenRidesCard> cards)
        {
            TenRidesCard card = cards.Where(c => c.CardCode == code).ToList()[0];
            return card;
        }

     }





    
}