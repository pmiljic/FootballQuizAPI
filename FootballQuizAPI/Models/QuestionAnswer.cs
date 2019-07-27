namespace FootballQuizAPI.Models
{
    public class QuestionAnswer
    {
        public int id { get; set; }
        public int question { get; set; }
        public int answer { get; set; }
        public bool is_correct { get; set; }
    }
}
