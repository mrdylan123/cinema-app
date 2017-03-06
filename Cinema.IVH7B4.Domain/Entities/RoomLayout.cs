using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.IVH7B4.Domain.Entities {
    [Table("RoomLayout")]
    public class RoomLayout {

        public RoomLayout() {
            
        }

        [Key]
        public int LayoutID { get; set; }


        public int FrontX { get; set; }
        public int FrontY { get; set; }
        public int BackX { get; set; }
        public int BackY { get; set; }

    }
}
