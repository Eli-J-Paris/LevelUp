using LevelUp.Models;
using Microsoft.EntityFrameworkCore;

namespace LevelUp.DataAccess
{
    public class LevelUpContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<DailyTask> DailyTasks { get; set; }
        public LevelUpContext(DbContextOptions<LevelUpContext> options) : base(options)
        {

        }

    }
}
