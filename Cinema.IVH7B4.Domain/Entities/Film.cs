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
        public string LanguageSubs { get; set; }
        public int Age { get; set; }
        public int FilmType { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int Length { get; set; }
        public bool Is3D { get; set; }
        public string Trailer { get; set; }
        public string Protagonist { get; set; }
        public string IMDB { get; set; }
        public string Website { get; set; }
        public string Director { get; set; }
        public string DescriptionEN { get; set; }

        public int LocationID { get; set; }

        [ForeignKey("LocationID")]
        public virtual Location Location { get; set; }
    }
}
