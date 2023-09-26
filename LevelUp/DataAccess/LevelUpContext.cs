using LevelUp.Models;
using Microsoft.EntityFrameworkCore;

namespace LevelUp.DataAccess
{
    public class LevelUpContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<DailyTask> DailyTasks { get; set; }
        public DbSet<WeeklyTask> WeeklyTasks { get; set; }
        public DbSet<ToDoTask> ToDoTasks { get; set; }
        public DbSet<AchievementHandler> AllAchievements { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Camp> Camps { get; set; }
        public LevelUpContext(DbContextOptions<LevelUpContext> options) : base(options)
        {

        }
    }
}
