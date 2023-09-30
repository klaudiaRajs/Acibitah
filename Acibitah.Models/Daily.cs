using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acibitah.Models
{
    public class Daily : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }  
        public DateTime Created { get; set; }
        public int Streak { get; set; }
        public bool Done { get; set; } = false; 
        public ICollection<Tag> Tags { get; set; }
    }
}
