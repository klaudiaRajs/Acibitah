namespace Acibitah.Models
{
    public class TagsTasks
    {
        public int TagId { get; set; }
        public int TaskId { get; set; }
        public Tag Tag { get; set; }
        public ToDoTask Task { get; set; }
    }
}
