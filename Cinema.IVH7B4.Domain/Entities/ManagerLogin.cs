using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.IVH7B4.Domain.Entities
{
    [Table("ManagerLogin")]
    public class ManagerLogin
    {
        public ManagerLogin()
        {

        }

        [Key]
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
