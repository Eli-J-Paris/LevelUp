namespace LevelUp.Models
{
    public class WeeklyTask : ITask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TaskType { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Difficulty { get; set; }
        public int XpReward { get; set; }
        public int AttributeReward { get; set; }
        public bool IsCompleted { get; set; }
        public User User { get; set; }
        public DateTime TimeCreated { get; set; } = DateTime.UtcNow;
        public DateTime? TimeCompleted { get; set; } = null;

        //DailyTask only
        public int Streak { get; set; }

        public void Complete()
        {
            IsCompleted = true;
            TimeCompleted = DateTime.UtcNow;
            User.XpGain(XpReward);
        }

        public void UndoComplete()
        {
            IsCompleted = false;
            TimeCompleted = null;
            User.XpGain(-XpReward);
        }

        public void Reset()
        {
            DateOnly dayCompleted = new DateOnly(2000, 1, 1);
            var thisWeek = GetWeek(true);
            var lastWeek = GetWeek(false);
            if (TimeCompleted != null)
            {
                dayCompleted = DateOnly.FromDateTime((DateTime)TimeCompleted);
            }
            if (!thisWeek.Contains(dayCompleted))
            {
                if (IsCompleted)
                {
                    IsCompleted = false;
                    Streak++;
                }
                else
                {
                    Streak = 0;
                }
                if (!lastWeek.Contains(dayCompleted))
                {
                    Streak = 0;
                }
            }
        }

        public List<DateOnly> GetWeek(bool thisWeek)
        {
            var returnList = new List<DateOnly>();
            int dayOfWeek = (int)DateTime.UtcNow.DayOfWeek;
            var today = DateOnly.FromDateTime((DateTime)DateTime.UtcNow);
            int j = 7;
            if (thisWeek) j = 0;
            for (int i = 0; i < 7; i++)
            {
                var day = today.AddDays(i - dayOfWeek - j);
                returnList.Add(day);
            }
            return returnList;
        }
    }
}
