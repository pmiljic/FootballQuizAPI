using Microsoft.EntityFrameworkCore;

namespace FootballQuizAPI.Models
{
    public class FootballQuizContext : DbContext
    {
        public FootballQuizContext(DbContextOptions<FootballQuizContext> options)
            : base(options)
        {
        }

        public DbSet<Candidate> candidate { get; set; }
        public DbSet<Answer> answer { get; set; }
        public DbSet<Question> question { get; set; }
        public DbSet<QuestionAnswer> question_answer { get; set; }
        public DbSet<Quiz> quiz { get; set; }
        public DbSet<QuizQuestion> quiz_question { get; set; }
    }
}