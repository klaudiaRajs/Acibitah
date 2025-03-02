namespace Acibitah.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Habit> Habits { get; set; }
        public IEnumerable<Daily> Dailies { get; set; }
        public IEnumerable<ToDoTask> ToDos { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public ToDoTask ToDo { get; set; } = new ToDoTask();
        public Habit Habit { get; set; } = new Habit();
        public Daily Daily { get; set; } = new Daily();
        public Tag Tag { get; set; }
    }
}
