namespace Acibitah.Models
{
    public class TaskViewModel
    {
        public IEnumerable<ToDoTask> Tasks { get; set; }
        public ToDoTask ToDo { get; set; }
        public IEnumerable<Subtask> Subtasks { get; set; }
        
    }
}
