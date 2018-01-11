using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cinema.IVH7B4.Domain.Entities;

namespace Cinema.IVH7B4.WebUI.Models
{
    public class SubscriptionHolderLogic
    {
        public bool HolderExists(string email, List<SubscriptionHolder> holders)
        {
            List<SubscriptionHolder> foundHolder = holders.Where(h => h.Email == email).ToList();
            int result = foundHolder.Count;

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public SubscriptionHolder GetHolder(string email, List<SubscriptionHolder> holders)
        {
            SubscriptionHolder foundHolder = holders.Where(h => h.Email == email).ToList()[0];

            return foundHolder;
        }
    }
}