namespace LevelUp.Models
{
    public interface ITask
    {
        int Id { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        string Category { get; set; }
        int Difficulty { get; set; }
        int XpReward { get; set; }
        int AttributeReward { get; set; }
        bool IsCompleted { get; set; }


    }
}
