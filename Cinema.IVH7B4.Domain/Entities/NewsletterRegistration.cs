using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.IVH7B4.Domain.Entities
{
    [Table("NewsletterRegistration")]
    public class NewsletterRegistration
    {
        [Key]
        public int RegistrationID { get; set; }

        public int Age { get; set; }
        public String Name { get; set; }
        public String Location { get; set; }
        public String Interests { get; set; }
        public String Email { get; set; }

    }
}
