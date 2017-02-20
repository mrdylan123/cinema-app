using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.IVH7B4.Domain.Entities {
    [Table("Seat")]
    public class Seat {

        public Seat() {
            
        }

        [Key]
        public int SeatID { get; set; }
        public int SeatType { get; set; }
        public int RowNo { get; set; }
        public int seatNo { get; set; }

        public int RoomID { get; set; }

        [ForeignKey("RoomID")]
        public Room Room { get; set; }
    }
}
