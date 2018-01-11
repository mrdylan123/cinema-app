using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.IVH7B4.Domain.Entities
{
    [Table("WeekDayCard")]
    public class WeekDayCard
    {

        public WeekDayCard()
        {
            
        }

        [Key]
        public int Code { get; set; }
    }
}
