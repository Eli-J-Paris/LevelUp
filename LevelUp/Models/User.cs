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
        public int Level { get; set; } = 1;
        public int Xp { get; set; } = 0;
        public int Hygiene { get; set; } = 0;
        public int Wellness { get; set; } = 0;
        public int Mindfullness { get; set; } = 0;
        public int Productivity { get; set; } = 0;
        public int HabitBuilding { get; set; } = 0;

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
    }
}
