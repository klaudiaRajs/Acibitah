namespace Acibitah.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TagsTasks> TagsTasks { get; set; }
        public ICollection<Habit> Habits { get; set; }
        public ICollection<Daily> Dailies { get; set; }
    }
}
