using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.IVH7B4.Domain.Entities {

    [Table("Film")]
    public class Film {

        public Film() {
            
        }
        [Key]
        public int FilmID { get; set; }

        public string Name { get; set; }
        public string Language { get; set; }
        public string LanguagueSubs { get; set; }
        public int Age { get; set; }
        public int FilmType { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Lenght { get; set; }
        public bool Is3D { get; set; }
    }
}
