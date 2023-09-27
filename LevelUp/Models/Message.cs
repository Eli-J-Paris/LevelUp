namespace LevelUp.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }

        public Message(string content)
        {
            Content = content;
            CreatedAt = DateTime.Now.ToUniversalTime();
        }
    }
}
