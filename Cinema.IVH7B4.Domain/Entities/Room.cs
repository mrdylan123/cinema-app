﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.IVH7B4.Domain.Entities {

    [Table("Room")]
    public class Room {

        public Room() {        
        }

        [Key]
        public int RoomID { get; set; }

        public int RoomNumber { get; set; }

        public int LayoutID { get; set; }

        [ForeignKey("LayoutID")]
        public RoomLayout Layout { get; set; }
    }
}
