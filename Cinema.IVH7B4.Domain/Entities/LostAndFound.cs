using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.IVH7B4.Domain.Entities
{
    [Table("LostAndFound")]
    public class LostAndFound
    {
  
        public LostAndFound()
        {
            
        }

        [Key]
        public DateTime Date { get; set; }

        public string Object { get; set; }
        public string Finder { get; set; }
        public string Owner { get; set; }
        public int RoomNumber { get; set; }
        public int Location { get; set; }
    }
}
