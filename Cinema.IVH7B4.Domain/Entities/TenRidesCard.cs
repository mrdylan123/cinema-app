using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.IVH7B4.Domain.Entities
{
    [Table("TenRidesCard")]
    public class TenRidesCard
    {

        public TenRidesCard()
        {
            
        }

        [Key]
        public int CardCode { get; set; }

        public int Rides { get; set; } 
    }
}
