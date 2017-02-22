using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.IVH7B4.Domain.Entities {

    [Table("Ticket")]
    public class Ticket {

        public Ticket() {

        }
        [Key]
        public int TicketID { get; set; }
        public decimal Price { get; set; }
        public int TicketType { get; set; }
        public decimal Discount { get; set; }

        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }

        public String SecretKey { get; set; }

        public int SeatID { get; set; }

        [ForeignKey("SeatID")]
        public virtual Seat Seat { get; set;  }

        public int ShowingID { get; set; }

        [ForeignKey("ShowingID")]
        public virtual Showing Showing { get; set; }
    }
}
