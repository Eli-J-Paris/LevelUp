namespace LevelUp.Models
{
    public class ToDoTask : ITask
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


        //DailyTask only
        public DateTime TimeCreated { get; set; }
        public DateTime TimeCompleted { get; set; }
       

    }
}
