using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LevelUp.Models
{
    public interface ITask
    {
        int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        string Title { get; set; }
        [Required(ErrorMessage = "Task Type is required")]
        string TaskType { get; set; }
        [Required(ErrorMessage = "Description is required")]
        string Description { get; set; }
        string Category { get; set; }
        int Difficulty { get; set; }
        int XpReward { get; set; }
        int AttributeReward { get; set; }
        bool IsCompleted { get; set; }
        [ValidateNever]
        User? User { get; set; }
        DateTime TimeCreated { get; set; }
        DateTime? TimeCompleted { get; set; }

        void Complete();
        void UndoComplete();
    }
}
