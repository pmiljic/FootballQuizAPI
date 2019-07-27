using System.Collections.Generic;

namespace FootballQuizAPI.Models
{
    public class QuizQuestions
    {
        public Quiz quiz { get; set; }
        public List<QuestionAnswers> questionAnswers { get; set; } = new List<QuestionAnswers>();
    }
}
