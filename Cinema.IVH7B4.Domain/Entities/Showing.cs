using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.IVH7B4.Domain.Entities {

    [Table("Showing")]
    public class Showing {

        public Showing() {
            
        }

        [Key]
        public int ShowingID { get; set; }

        public DateTime BeginDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public int FilmID { get; set; }

        [ForeignKey("FilmID")]
        public Film Film { get; set; }

        public int RoomID { get; set; }

        [ForeignKey("RoomID")]
        public Room Room { get; set; }
    }
}
