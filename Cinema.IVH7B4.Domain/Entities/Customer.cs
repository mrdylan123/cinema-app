using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.IVH7B4.Domain.Entities {

    [Table("Customer")]
    public class Customer : User {

        public Customer() {

        }

        public decimal NationaleBioscoopBonCredit { get; set; }
        public decimal MaDiWoDoCredit { get; set; }
        [Key]
        public int UserID { get; set; }
    }
}
