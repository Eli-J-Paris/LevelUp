namespace LevelUp.Models
{
    public interface ITask
    {
        int Id { get; set; }
        string Title { get; set; }
        string TaskType { get; set; }
        string Description { get; set; }
        string Category { get; set; }
        int Difficulty { get; set; }
        int XpReward { get; set; }
        int AttributeReward { get; set; }
        bool IsCompleted { get; set; }
        User User { get; set; }
        DateTime TimeCreated { get; set; }
        DateTime? TimeCompleted { get; set; }

        void Complete();
        void UndoComplete();
    }
}
