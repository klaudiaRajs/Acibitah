using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acibitah.Models
{
    public class Habit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? NegativeValue { get; set; }
        public int? PositiveValue { get; set; }
        public int LifeImpact { get; set; }
        public int Streak { get; set; }
    }
}
