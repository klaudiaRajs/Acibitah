using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acibitah.Models
{
    public class HabbitStats
    {
        public int Id { get; set; }
        public int HabitId { get; set; }
        public Habit Habit { get; set; }
        public DateTime DateOfUpdate { get; set; }
        public int NegativeValue { get; set; }
        public int PositiveValue { get; set; }
    }
}
