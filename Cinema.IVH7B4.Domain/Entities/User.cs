using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.IVH7B4.Domain.Entities {
    [Table("User")]
    public class User {

        public User() {
            
        }
        [Key]
        public int UserID { get; set; }

        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
    }
}
