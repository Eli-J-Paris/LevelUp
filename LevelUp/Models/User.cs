
﻿using System.Runtime.InteropServices;
﻿using LevelUp.DataAccess;

using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks.Sources;

namespace LevelUp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string  Password { get; set; }
        public List<DailyTask> DailyTasks { get; set; } = new List<DailyTask>();
        public List<WeeklyTask> WeeklyTasks { get; set; } = new List<WeeklyTask>();
        public List<ToDoTask> ToDoTasks { get; set; } = new List<ToDoTask>();
        public int Level { get; set; } = 1;
        public int Xp { get; set; } = 0;
        public int Hygiene { get; set; } = 0;
        public int Wellness { get; set; } = 0;
        public int Mindfullness { get; set; } = 0;
        public int Productivity { get; set; } = 0;
        public int HabitBuilding { get; set; } = 0;
        public AchievementHandler? Achievements { get; set; } = new AchievementHandler();
        public List<Camp> Camps { get; set; } = new List<Camp>();
        public string Encrypt(string password)
        {
            HashAlgorithm sha = SHA256.Create();
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            byte[] passwordDigested = sha.ComputeHash(passwordBytes);
            StringBuilder passwordBuilder = new StringBuilder();
            foreach (byte b in passwordDigested)
            {
                passwordBuilder.Append(b.ToString("x2"));
            }
            return passwordBuilder.ToString();
        }

        public Dictionary<string, int> GetStreaks()
        {
            var returnDict = new Dictionary<string, int>();
            foreach(var task in DailyTasks)
            {
                returnDict.Add(task.Title, task.Streak);
            }
            foreach (var task in WeeklyTasks)
            {
                returnDict.Add(task.Title, task.Streak);
            }
            return returnDict;
        }

        public void XpGain(int xp)
        {
            Xp += xp;
        }

        public void Reset()
        {
            if(Xp >= NeededXp())
            {
                Xp -= NeededXp();
                Level++;  
            }

            if(Xp < 0)
            {
                Level--;
                Xp += NeededXp();
            }
            foreach(DailyTask task in DailyTasks)
            {
                task.Reset();
            }
            foreach (WeeklyTask task in WeeklyTasks)
            {
                task.Reset();
            }
        }
      
        public int NeededXp()
        {
            return Level * 10;
        }

        public void UpdateAchievement(string category,LevelUpContext context)
        {
            category = category.ToLower();
            if(category == "hygiene")
            {
              Achievements.Hygenie5Achievement.IncreaseScore();
              context.AllAchievements.Update(Achievements);
            }  
            else if (category == "wellness")
            {
                Achievements.Wellness5Achievement.IncreaseScore();
                context.AllAchievements.Update(Achievements);
            }
            else if (category == "mindfullness")
            {
                Achievements.Mindfulness5Achievement.IncreaseScore();
                context.AllAchievements.Update(Achievements);
            }
            else if (category == "productivity")
            {
                Achievements.Productivity5Achievement.IncreaseScore();
                context.AllAchievements.Update(Achievements);
            }
            else if (category == "habitbuilding")
            {
                Achievements.HabitBuilding5Achievement.IncreaseScore();
                context.AllAchievements.Update(Achievements);
            }
            context.SaveChanges();

        }

        public void UndoAchievement(string category,LevelUpContext context)
        {
            category = category.ToLower();
            if (category == "hygiene")
            {
                Achievements.Hygenie5Achievement.DecreaseScore();
                context.AllAchievements.Update(Achievements);

            }
            else if (category == "wellness")
            {
                Achievements.Wellness5Achievement.DecreaseScore();
                context.AllAchievements.Update(Achievements);

            }
            else if (category == "mindfullness")
            {
                Achievements.Mindfulness5Achievement.DecreaseScore();
                context.AllAchievements.Update(Achievements);

            }
            else if (category == "productivity")
            {
                Achievements.Productivity5Achievement.DecreaseScore();
                context.AllAchievements.Update(Achievements);

            }
            else if (category == "habitbuilding")
            {
                Achievements.HabitBuilding5Achievement.DecreaseScore();
                context.AllAchievements.Update(Achievements);
            }
        }

        public void AddDaily(ITask task, LevelUpContext context)
        {
            var newTask = new DailyTask {User = this, Title = task.Title, Description = task.Description, TaskType = task.TaskType, Category = task.Category, Difficulty = task.Difficulty, XpReward = task.XpReward, AttributeReward = task.AttributeReward };
            context.DailyTasks.Add(newTask);
            context.SaveChanges();
        }

        public void AddWeekly(ITask task, LevelUpContext context)
        {
            var newTask = new WeeklyTask { User = this, Title = task.Title, Description = task.Description, TaskType = task.TaskType, Category = task.Category, Difficulty = task.Difficulty, XpReward = task.XpReward, AttributeReward = task.AttributeReward };
            context.WeeklyTasks.Add(newTask);
            context.SaveChanges();
        }

    }

}

