using System.Collections.Generic;

namespace FootballQuizAPI.Models
{
    public class QuestionAnswers
    {
        public Question question { get; set; }
        public List<Answer> answers { get; set; } = new List<Answer>();
    }
}
