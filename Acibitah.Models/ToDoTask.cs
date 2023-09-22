using Acibitah.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acibitah.Models
{
    public class ToDoTask 
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; } = string.Empty;
        public int? Difficulty { get; set; }
        public int MoneyImpact { get; set; } = 5;
        public ICollection<Subtask> Subtasks { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
