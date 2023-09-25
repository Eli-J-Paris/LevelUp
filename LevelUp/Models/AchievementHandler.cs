namespace LevelUp.Models
{
    public class AchievementHandler
    {
        public Achievement Hygenie5Achievement { get; set; } 
        public Achievement Wellness5Achievement { get; set; } 
        public Achievement Mindfulness5Achievement { get; set; }
        public Achievement Productivity5Achievement { get; set; }
        public Achievement HabitBuilding5Achievement { get; set; }


        public AchievementHandler()
        {       
                                                     //Title, XPReward, maxscore
            Hygenie5Achievement = new Achievement("Complete 5 Hygenie Tasks", 10, 5);
            Wellness5Achievement = new Achievement("Complete 5 Wellness Tasks", 10, 5);
            Mindfulness5Achievement = new Achievement("Complete 5 Mindfulness Tasks", 10, 5);
            Productivity5Achievement = new Achievement("Complete 5 Productivity Tasks", 10, 5);
            HabitBuilding5Achievement = new Achievement("Complete 5 HabbitBuilding Tasks", 10, 5);
        }

    }
}
