namespace LevelUp.Models
{
    public class DailyTask : ITask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TaskType { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Difficulty { get; set; }
        public int XpReward { get; set; }
        public int AttributeReward { get; set; }
        public bool IsCompleted { get; set; } = false;
        public User User { get; set; }
        public DateTime TimeCreated { get; set; } = DateTime.UtcNow;
        public DateTime? TimeCompleted { get; set; } = null;


        //DailyTask only
        public int Streak { get; set; } = 0;

    }
}
