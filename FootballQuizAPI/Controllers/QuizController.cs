using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FootballQuizAPI.Models;

namespace NaovisQuizAPI.Controllers
{
    [Route("api/quizzes")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly FootballQuizContext _context;
        public QuizController(FootballQuizContext context)
        {
            _context = context;
        }

        [HttpGet("{id}", Name = "GetQuiz")]
        public ActionResult<Quiz> GetById(int id)
        {
            var quiz = _context.quiz.Find(id);
            if (quiz == null)
            {
                return NotFound();
            }
            return quiz;
        }

        [HttpPost]
        public QuizQuestions Create(QuizQuestions quizQuestions)
        {
            quizQuestions.quiz.score = getScore(quizQuestions.questionAnswers);
            _context.quiz.Add(quizQuestions.quiz);
            _context.SaveChanges();

            return quizQuestions;
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Quiz quiz)
        {
            var q = _context.quiz.Find(id);
            if (q == null)
            {
                return NotFound();
            }

            q.candidate = quiz.candidate;
            q.score = quiz.score;

            _context.quiz.Update(q);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var q = _context.quiz.Find(id);
            if (q == null)
            {
                return NotFound();
            }

            _context.quiz.Remove(q);
            _context.SaveChanges();

            return NoContent();
        }

        public int getScore(List<QuestionAnswers> questionAnswers)
        {
            int score = 0, correct = 0, wrong = 0;

            foreach (QuestionAnswers qa in questionAnswers)
            {
                int rightAnswers =
                           (from q in _context.question_answer
                            where q.question == qa.question.id
                            where q.is_correct
                            select q).Count();

                foreach (Answer a in qa.answers)
                {
                    if (a.is_selected)
                    {
                        var correctAnswer =
                        (from q in _context.question_answer
                         where q.answer == a.id
                         where q.question == qa.question.id
                         select q.is_correct).ToList();
                        if (correctAnswer.First())
                        {
                            if (qa.question.is_multiple_choice)
                            {
                                correct += 1;
                            }
                            else
                            {
                                score += 1;
                            }
                        }
                        else
                        {
                            wrong += 1;
                        }
                    }
                }
                correct = correct - wrong;
                if (correct == rightAnswers)
                {
                    score += 1;
                }
                correct = 0;
                wrong = 0;
                rightAnswers = 0;
            }

            return score;
        }
    }
}