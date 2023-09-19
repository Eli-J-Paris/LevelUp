namespace LevelUp.Models
{
    public class DailyTask : ITask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Difficulty { get; set; }
        public int XpReward { get; set; }
        public int AttributeReward { get; set; }
        public bool IsCompleted { get; set; }
        public User User { get; set; }


        //DailyTask only
        public TimeOnly TimeCreated { get; set; }
        public TimeOnly TimeCompleted { get; set; }
        public int Streak { get; set; }

    }
}
