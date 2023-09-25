namespace LevelUp.Models
{
    public class RadarChart
    {
        public List<string> Labels { get; set; } = new List<string> {};
        public List<int> Values { get; set; } = new List<int> {};

        public RadarChart Data { get; set; }
    }
}
