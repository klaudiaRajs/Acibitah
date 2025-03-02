using System.ComponentModel.DataAnnotations;

namespace Acibitah.Models
{
    public class ToDoTask : IModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; } = string.Empty;
        public int? Difficulty { get; set; }
        public int MoneyImpact { get; set; } = 5;
        public bool Done { get; set; } = false;
        public ICollection<Subtask> Subtasks { get; set; }
        public ICollection<TagsTasks> TagsTasks { get; set; }
    }
}
