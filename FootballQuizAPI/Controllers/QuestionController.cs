using FootballQuizAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NaovisQuizAPI.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly FootballQuizContext _context;
        public QuestionController(FootballQuizContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<QuestionAnswers>> GetQuizQuestions()
        {
            List<QuestionAnswers> quizQuestionsAnswers = new List<QuestionAnswers>();
            QuestionAnswers questionsAnswers;
            List<Question> randomQuestions = GetFiveRandomQuestions();

            for (int i = 0; i < randomQuestions.Count; i++)
            {
                questionsAnswers = new QuestionAnswers
                {
                    question = randomQuestions[i],
                    answers = GetAnswersByQuestion(randomQuestions[i].id)
                };
                quizQuestionsAnswers.Add(questionsAnswers);
            }

            return quizQuestionsAnswers;
        }

        public List<Question> GetFiveRandomQuestions()
        {
            Random rnd = new Random();
            return _context.question.OrderBy(x => rnd.Next()).Take(5).ToList();
        }

        public List<Answer> GetAnswersByQuestion(int questionId)
        {
            var answers =
               (from a in _context.answer
                join qa in _context.question_answer on a.id equals qa.answer
                where qa.question == questionId
                select a).ToList();

            return answers;
        }
    }
}