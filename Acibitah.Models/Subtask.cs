namespace Acibitah.Models
{
    public class Subtask
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
        public ToDoTask Task { get; set;}

    }
}
