namespace LevelUp.Models
{
    public class Achievement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int XpReward { get; set; }
        public bool IsCompleted { get; set; }
        public int Score { get; set; } 
        public int MaxScore { get; set; }
        
        public Achievement(string title, int xpReward, int maxScore)
        {
            Title = title;
            XpReward = xpReward;
            IsCompleted = false;
            Score = 0;
            MaxScore = maxScore;
        }
       
        public void IncreaseScore()
        {
            if(IsCompleted == false)
            {
                Score++;
            }
        }
        public void DecreaseScore()
        {
            if (IsCompleted == false)
            {
                Score--;
            }
        }


        //Will want to make a method or something to check if max score is reached and change Iscompleted to true
    }
}
