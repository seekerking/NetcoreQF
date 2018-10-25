namespace DBContextEF.Models
{
    public partial class Holidays
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DayKey { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
    }
}
