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
        public string Description { get; set; } = string.Empty;
        public int? NegativeValue { get; set; }
        public int? PositiveValue { get; set; } = 1;
        public int LifeImpact { get; set; } = 10;
        public int Streak { get; set; } = 0; 
        public ICollection<Tag> Tags { get; set; }
    }
}
