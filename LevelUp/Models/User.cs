
﻿using System.Runtime.InteropServices;
﻿using LevelUp.DataAccess;

using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks.Sources;
using OpenAI_API.Images;
using OpenAI_API;
using Serilog;
using static System.Net.WebRequestMethods;
using System.ComponentModel.DataAnnotations;

namespace LevelUp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(747, ErrorMessage = "Name cannot exceed 747 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Favorite Animal is required")]
        public string FavoriteAnimal { get; set; }
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
        public string PfpUrl { get; set; } = "";
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

        public void Reset(LevelUpContext? context, string apiKey)
        {
            LevelUp(context, apiKey);
            LevelDown(context, apiKey);
            ResetTasks(context);
        }
        public void LevelUp(LevelUpContext? context, string apiKey)
        {
            if (Xp >= NeededXp())
            {
                Xp -= NeededXp();
                Level++;
                if (context != null)
                {
                    PfpUrl = GetAIGeneratedAvatar(apiKey).Result;
                    context.Users.Update(this);
                    context.SaveChanges();
                }
            }
        }
        public void LevelDown(LevelUpContext? context, string apiKey)
        {
            if (Xp < 0)
            {
                Level--;
                Xp += NeededXp();
                if (context != null)
                {
                    PfpUrl = GetAIGeneratedAvatar(apiKey).Result;
                    context.Users.Update(this);
                    context.SaveChanges();
                }
            }
        }
        public void ResetTasks(LevelUpContext? context)
        {
            var tasks = new List<IRecuring>();
            tasks.AddRange(DailyTasks);
            tasks.AddRange(WeeklyTasks);
            foreach (IRecuring task in tasks)
            {
                task.Reset(context);
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

        public async Task<string> GetAIGeneratedAvatar(string apiKey)
        {
            try
            {
                OpenAIAPI api = new OpenAIAPI(apiKey);
                var result = await api.ImageGenerations.CreateImageAsync(new ImageGenerationRequest(GetAvatarPrompt(), 1, ImageSize._256));
                Log.Information($"Api call to OpenAIAPI successful: {result.Data[0].Url}");
                return result.Data[0].Url;
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "OpenAI API not responding");
                return "https://placehold.co/256x256?text=OpenAI%20Api%20Not%20Responding";
            }
        }

        private string GetAvatarPrompt()
        {
            var hats = new List<string>
            {
                "blue baseball cap",
                "black beanie",
                "football helmet",
                "Top hat",
                "pirate hat",
                "fantasy wizard hat",
                "pink cowboy hat",
                "joker hat",
                "space helmet",
                "golden crown"
            };
            var i = Level - 1;
            if (i > hats.Count - 1) i = hats.Count - 1;
            return $"a {FavoriteAnimal} wearing a {hats[i]}";
        }
    }
}

