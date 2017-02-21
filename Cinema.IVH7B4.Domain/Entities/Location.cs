﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.IVH7B4.Domain.Entities {

    [Table("Location")]
    public class Location {

        public Location() {
            
        }

        [Key]
        public int LocationID { get; set; }

        public string Name { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public int Rooms { get; set; }
        public decimal TicketPriceNormal { get; set; }
        public decimal TicketPriceLong { get; set; }
        public decimal kidDiscount { get; set; }
        public decimal StudentDiscount { get; set; }
        public decimal SeniorDiscount { get; set; }
    }
}
