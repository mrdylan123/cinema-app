using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.IVH7B4.Domain.Entities {

    [Table("Manager")]
    public class Manager : User {

        public Manager() {
            
        }
        [Key]
        public int UserID { get; set; }
    }
}
