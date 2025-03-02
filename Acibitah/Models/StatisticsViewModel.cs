namespace Acibitah.Models
{
    public class StatisticsViewModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public IEnumerable<StatisticItem> Items { get; set; }
    }
}
