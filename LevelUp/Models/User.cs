using System.Runtime.InteropServices;
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
        public  AchievementHandler Achievements { get; set; } 

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
            foreach(DailyTask task in DailyTasks)
            {
                task.Reset();
            }
            foreach (WeeklyTask task in WeeklyTasks)
            {
                task.Reset();
            }
        }

        public void UpdateAchievement(string category)
        {
            category = category.ToLower();
            if(category == "hygiene")
            {
                Achievements.Hygenie5Achievement.IncreaseScore();
            }
            else if(category == "wellness")
            {
                Achievements.Wellness5Achievement.IncreaseScore();
            }
            else if(category == "mindfullness")
            {
                Achievements.Mindfulness5Achievement.IncreaseScore();
            }
            else if (category == "productivity")
            {
                Achievements.Productivity5Achievement.IncreaseScore();
            }
            else if (category == "habitbuilding")
            {
                Achievements.HabitBuilding5Achievement.IncreaseScore();
            }
        }
       
        public void UndoAchievement(string category)
        {
            category = category.ToLower();
            if (category == "hygiene")
            {
                Achievements.Hygenie5Achievement.DecreaseScore();
            }
            else if (category == "wellness")
            {
                Achievements.Wellness5Achievement.DecreaseScore();
            }
            else if (category == "mindfullness")
            {
                Achievements.Mindfulness5Achievement.DecreaseScore();
            }
            else if (category == "productivity")
            {
                Achievements.Productivity5Achievement.DecreaseScore();
            }
            else if (category == "habitbuilding")
            {
                Achievements.HabitBuilding5Achievement.DecreaseScore();
            }
        }

    }

}

