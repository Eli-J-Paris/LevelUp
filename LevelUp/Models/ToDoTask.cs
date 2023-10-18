using System.ComponentModel.DataAnnotations;

namespace LevelUp.Models
{
    public class ToDoTask : ITask
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Task Type is required")]
        public string TaskType { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public string Category { get; set; }
        public int Difficulty { get; set; }
        public int XpReward { get; set; }
        public int AttributeReward { get; set; }
        public bool IsCompleted { get; set; }
        public User User { get; set; }
        public DateTime TimeCreated { get; set; } = DateTime.UtcNow;
        public DateTime? TimeCompleted { get; set; } = null;


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
    }
}
