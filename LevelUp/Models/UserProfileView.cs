using System.Globalization;

namespace LevelUp.Models
{
    public class UserProfileView
    {
        public User User { get; set; }
        public string DailyAffirmation { get; set; }
        public RadarChart RadarChart { get; set; }
    }
}
