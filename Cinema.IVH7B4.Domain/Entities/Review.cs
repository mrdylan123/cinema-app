using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.IVH7B4.Domain.Entities
{
    [Table("Review")]
    public class Review
    {

        public Review()
        {   
        }

        [Key]
        public int ReviewID { get; set; }

        public int Rating { get; set; }
        public string Description { get; set; }

        public int FilmID { get; set; }

        [ForeignKey("FilmID")]
        public virtual Film Film { get; set; }
    }
}
