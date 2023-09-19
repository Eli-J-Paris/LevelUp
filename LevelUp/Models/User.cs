using System.Threading.Tasks.Sources;

namespace LevelUp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string  Password { private get; set; }
        public List<ITask> Tasks { get; set; } = new List<ITask>();
        public int Level { get; set; }
        public int Xp { get; set; }
        public int Hygiene { get; set; }
        public int Wellness { get; set; }
        public int Mindfullness { get; set; }
        public int Productivity { get; set; }
        public int HabitBuilding { get; set; }





    }
}
