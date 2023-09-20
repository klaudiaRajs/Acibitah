namespace Acibitah.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Habit> Habits { get; set; }
        public IEnumerable<Daily> Dailies { get; set; }
        public IEnumerable<ToDoTask> ToDos { get; internal set; }
    }
}
