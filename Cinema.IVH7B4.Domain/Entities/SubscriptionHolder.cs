using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.IVH7B4.Domain.Entities
{
    [Table("SubscriptionHolder")]
    public class SubscriptionHolder
    {

        public SubscriptionHolder()
        {
            
        }

        [Key]
        public string Email { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
    }
}
