using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acibitah.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ToDoTask> Tasks { get; set; }
        public ICollection<Habit> Habits { get; set; }
        public ICollection<Daily> Dailies { get; set; }
    }
}
