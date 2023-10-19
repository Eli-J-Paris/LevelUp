using LevelUp.Models;
using System.Security.Cryptography;
using System.Text;

namespace LevelUpTesting
{
    [Collection("Controller Tests")]
    public class ModelTests
    {
        [Fact]
        public void User_Constructor_CreatesUserObject()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog"};
            user.Password = user.Encrypt(user.Password);

            Assert.Equal("John Doe", user.Name);
            Assert.Equal("jdoe", user.Username);
            Assert.Equal(user.Encrypt("123"), user.Password);
            Assert.Equal("dog", user.FavoriteAnimal);
            Assert.Equal(1, user.Level);
            Assert.Equal(0, user.Xp);
            Assert.Equal(0, user.Hygiene);
            Assert.Equal(0, user.Wellness);
            Assert.Equal(0, user.Mindfullness);
            Assert.Equal(0, user.Productivity);
            Assert.Equal(0, user.HabitBuilding);
            Assert.Equal("", user.PfpUrl);

        }
        [Fact]
        public void DailyTask_Constructor_CreatesDailyTask()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new DailyTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };

            Assert.Equal("make bed", task.Title);
            Assert.Equal("daily", task.TaskType);
            Assert.Equal("Make your bed in the morning", task.Description);
            Assert.Equal("HabitBuilding", task.Category);
            Assert.Equal(2, task.Difficulty);
            Assert.Equal(2, task.XpReward);
            Assert.Equal(1, task.AttributeReward);
            Assert.False(task.IsCompleted);
            Assert.Equal(user, task.User);
            Assert.Equal(DateTime.UtcNow.Date, task.TimeCreated.Date);
            Assert.Null(task.TimeCompleted);
            Assert.Equal(0, task.Streak);
        }
        [Fact]
        public void WeeklyTask_Constructor_CreatesWeeklyTask()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new WeeklyTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };

            Assert.Equal("make bed", task.Title);
            Assert.Equal("daily", task.TaskType);
            Assert.Equal("Make your bed in the morning", task.Description);
            Assert.Equal("HabitBuilding", task.Category);
            Assert.Equal(2, task.Difficulty);
            Assert.Equal(2, task.XpReward);
            Assert.Equal(1, task.AttributeReward);
            Assert.False(task.IsCompleted);
            Assert.Equal(user, task.User);
            Assert.Equal(DateTime.UtcNow.Date, task.TimeCreated.Date);
            Assert.Null(task.TimeCompleted);
            Assert.Equal(0, task.Streak);
        }
        [Fact]
        public void ToDoTask_Constructor_CreatesToDoTask()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new ToDoTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };

            Assert.Equal("make bed", task.Title);
            Assert.Equal("daily", task.TaskType);
            Assert.Equal("Make your bed in the morning", task.Description);
            Assert.Equal("HabitBuilding", task.Category);
            Assert.Equal(2, task.Difficulty);
            Assert.Equal(2, task.XpReward);
            Assert.Equal(1, task.AttributeReward);
            Assert.False(task.IsCompleted);
            Assert.Equal(user, task.User);
            Assert.Equal(DateTime.UtcNow.Date, task.TimeCreated.Date);
            Assert.Null(task.TimeCompleted);
        }
        [Fact]
        public void Achievement_Constructor_CreatesAchevement()
        {
            var achievement = new Achievement("complete task", 1, 1);

            Assert.Equal("complete task", achievement.Title);
            Assert.Equal(1, achievement.XpReward);
            Assert.False(achievement.IsCompleted);
            Assert.Equal(0, achievement.Score);
            Assert.Equal(1, achievement.MaxScore);
        }
        [Fact]
        public void AchievementHandler_Constructor_CreatesAcheivementHandler()
        {
            var achievementHandler = new AchievementHandler();

            var hygenie5Achievement = new Achievement("Complete 5 Hygenie Tasks", 10, 5);
            var wellness5Achievement = new Achievement("Complete 5 Wellness Tasks", 10, 5);
            var mindfulness5Achievement = new Achievement("Complete 5 Mindfulness Tasks", 10, 5);
            var productivity5Achievement = new Achievement("Complete 5 Productivity Tasks", 10, 5);
            var habitBuilding5Achievement = new Achievement("Complete 5 HabbitBuilding Tasks", 10, 5);

            Assert.Equivalent(hygenie5Achievement, achievementHandler.Hygenie5Achievement);
            Assert.Equivalent(wellness5Achievement, achievementHandler.Wellness5Achievement);
            Assert.Equivalent(mindfulness5Achievement, achievementHandler.Mindfulness5Achievement);
            Assert.Equivalent(productivity5Achievement, achievementHandler.Productivity5Achievement);
            Assert.Equivalent(habitBuilding5Achievement, achievementHandler.HabitBuilding5Achievement);
        }
        [Fact]
        public void User_Encrypt_ReturnsHashedValue()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var password = "password";
            HashAlgorithm sha = SHA256.Create();
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            byte[] passwordDigested = sha.ComputeHash(passwordBytes);
            StringBuilder passwordBuilder = new StringBuilder();
            foreach (byte b in passwordDigested)
            {
                passwordBuilder.Append(b.ToString("x2"));
            }
            
            Assert.Equal(passwordBuilder.ToString(), user.Encrypt(password));
            Assert.NotEqual(passwordBuilder.ToString(), user.Encrypt(password + "1"));
        }
        [Fact]
        public void User_GetStreaks_ReturnsDictionaryOfStreaks()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task1 = new DailyTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            var task2 = new WeeklyTask
            {
                Title = "brush teeth",
                TaskType = "daily",
                Description = "Brush your teeth in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            task1.Streak = 1;
            task2.Streak = 3;
            var expected = new Dictionary<string, int>
            {
                {"make bed", 1 },
                {"brush teeth", 3 }
            };
            user.DailyTasks.Add(task1);
            user.WeeklyTasks.Add(task2);
            Assert.Equal(expected, user.GetStreaks());
        }
        [Fact]
        public void User_XpGain_AddsToXp()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            user.XpGain(3);
            Assert.Equal(3, user.Xp);
        }
        [Fact]
        public void User_Reset_DoesNothingIfXpToLow()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            user.Xp = 15;
            user.Level = 2;
            user.Reset(null, "");
            Assert.Equal(2, user.Level);
        }
        [Fact]
        public void User_Reset_IncrementsLevelIfXpHighEnough()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            user.Xp = 25;
            user.Level = 2;
            user.Reset(null, "");
            Assert.Equal(3, user.Level);
        }
        [Fact]
        public void User_Reset_DecrementLevelIfXpNegative()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            user.Xp = -5;
            user.Level = 2;
            user.Reset(null, "");
            Assert.Equal(1, user.Level);
        }
        [Fact]
        public void Achievement_IncreaseScore_IncrementsScore()
        {
            var achievement = new Achievement("complete task", 1, 1);
            achievement.Score = 3;
            achievement.IncreaseScore();
            Assert.Equal(4, achievement.Score);
        }
        [Fact]
        public void Achievement_decreaseScore_decrementsScore()
        {
            var achievement = new Achievement("complete task", 1, 1);
            achievement.Score = 3;
            achievement.DecreaseScore();
            Assert.Equal(2, achievement.Score);
        }
        [Fact]
        public void DailyTask_Complete_SetsIsCompletedToTrue()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new DailyTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            task.Complete();
            Assert.True(task.IsCompleted);
        }
        [Fact]
        public void DailyTask_Complete_SetsCurrentTimeCompleted()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new DailyTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            task.Complete();
            Assert.Equal(DateTime.UtcNow.Date, ((DateTime)task.TimeCompleted).Date);
        }
        [Fact]
        public void DailyTask_UndoComplete_SetsIsCompletedToFalse()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new DailyTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            task.IsCompleted = true;
            task.UndoComplete();
            Assert.False(task.IsCompleted);
        }
        [Fact]
        public void DailyTask_UndoComplete_SetsTimeCompletedToNull()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new DailyTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            task.IsCompleted = true;
            task.UndoComplete();
            Assert.Null(task.TimeCompleted);
        }
        [Fact]
        public void WeeklyTask_Complete_SetsIsCompletedToTrue()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new WeeklyTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            task.Complete();
            Assert.True(task.IsCompleted);
        }
        [Fact]
        public void WeeklyTask_Complete_SetsCurrentTimeCompleted()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new WeeklyTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            task.Complete();
            Assert.Equal(DateTime.UtcNow.Date, ((DateTime)task.TimeCompleted).Date);
        }
        [Fact]
        public void WeeklyTask_UndoComplete_SetsIsCompletedToFalse()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new WeeklyTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            task.IsCompleted = true;
            task.UndoComplete();
            Assert.False(task.IsCompleted);
        }
        [Fact]
        public void WeeklyTask_UndoComplete_SetsTimeCompletedToNull()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new WeeklyTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            task.IsCompleted = true;
            task.UndoComplete();
            Assert.Null(task.TimeCompleted);
        }
        [Fact]
        public void ToDoTask_Complete_SetsIsCompletedToTrue()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new ToDoTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            task.Complete();
            Assert.True(task.IsCompleted);
        }
        [Fact]
        public void ToDoTask_Complete_SetsCurrentTimeCompleted()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new ToDoTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            task.Complete();
            Assert.Equal(DateTime.UtcNow.Date, ((DateTime)task.TimeCompleted).Date);
        }
        [Fact]
        public void ToDoTask_UndoComplete_SetsIsCompletedToFalse()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new ToDoTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            task.IsCompleted = true;
            task.UndoComplete();
            Assert.False(task.IsCompleted);
        }
        [Fact]
        public void ToDoTask_UndoComplete_SetsTimeCompletedToNull()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new ToDoTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            task.IsCompleted = true;
            task.UndoComplete();
            Assert.Null(task.TimeCompleted);
        }
        [Fact]
        public void DailyTask_Reset_SameDayDoesNothing()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new DailyTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            task.Complete();
            task.Streak = 3;
            task.Reset(null);
            Assert.Equal(3, task.Streak);
        }
        [Fact]
        public void DailyTask_Reset_NextDayCompletedIncrementsStreak()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new DailyTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            task.Streak = 3;
            task.Complete();
            task.TimeCompleted = ((DateTime)task.TimeCompleted).AddDays(-1);
            task.Reset(null);
            Assert.Equal(4, task.Streak);
            Assert.False(task.IsCompleted);
        }
        [Fact]
        public void DailyTask_Reset_NextDayNotCompletedResetsStreak()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new DailyTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            task.Complete();
            task.IsCompleted = false;
            task.Streak = 3;
            task.TimeCompleted = ((DateTime)task.TimeCompleted).AddDays(-1);
            task.Reset(null);
            Assert.Equal(0, task.Streak);
            Assert.False(task.IsCompleted);
        }
        [Fact]
        public void DailyTask_Reset_FollowingDayCompletedResetsStreak()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new DailyTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            task.Streak = 3;
            task.Complete();
            task.TimeCompleted = ((DateTime)task.TimeCompleted).AddDays(-2);
            task.Reset(null);
            Assert.Equal(0, task.Streak);
            Assert.False(task.IsCompleted);
        }
        [Fact]
        public void WeeklyTask_Reset_SameWeekDoesNothing()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new WeeklyTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            task.Complete();
            task.Streak = 3;
            task.Reset(null);
            Assert.Equal(3, task.Streak);
        }
        [Fact]
        public void WeeklyTask_Reset_NextWeekCompletedIncrementsStreak()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new WeeklyTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            task.Streak = 3;
            task.Complete();
            task.TimeCompleted = ((DateTime)task.TimeCompleted).AddDays(-7);
            task.Reset(null);
            Assert.Equal(4, task.Streak);
            Assert.False(task.IsCompleted);
        }
        [Fact]
        public void WeeklyTask_Reset_NextWeekNotCompletedResetsStreak()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new WeeklyTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            task.Complete();
            task.IsCompleted = false;
            task.Streak = 3;
            task.TimeCompleted = ((DateTime)task.TimeCompleted).AddDays(-7);
            task.Reset(null);
            Assert.Equal(0, task.Streak);
            Assert.False(task.IsCompleted);
        }
        [Fact]
        public void WeeklyTask_Reset_FollowingWeekCompletedResetsStreak()
        {
            var user = new User { Name = "John Doe", Username = "jdoe", Password = "123", FavoriteAnimal = "dog" };
            var task = new WeeklyTask
            {
                Title = "make bed",
                TaskType = "daily",
                Description = "Make your bed in the morning",
                Category = "HabitBuilding",
                Difficulty = 2,
                XpReward = 2,
                AttributeReward = 1,
                User = user
            };
            task.Streak = 3;
            task.Complete();
            task.TimeCompleted = ((DateTime)task.TimeCompleted).AddDays(-14);
            task.Reset(null);
            Assert.Equal(0, task.Streak);
            Assert.False(task.IsCompleted);
        }
    }
}