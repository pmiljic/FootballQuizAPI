namespace FootballQuizAPI.Models
{
    public class Question
    {
        public int id { get; set; }
        public string text { get; set; }
        public bool is_multiple_choice { get; set; }
    }
}
