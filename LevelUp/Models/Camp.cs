namespace LevelUp.Models
{
    public class Camp
    {
        public int Id { get; set;}
        public string Title { get; set; }
        public string Description { get; set; }

        //public User Owner { get; set; }
        public List<User> Members { get; set; } = new List<User>();
        public List<Message> MessageBoard { get; set; } = new List<Message>();
    }
}
