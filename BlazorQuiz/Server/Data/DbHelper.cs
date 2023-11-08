using BlazorQuiz.Server.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorQuiz.Server.Data
{
    public class DbHelper
    {
        private ApplicationDbContext _context;
        public DbHelper(ApplicationDbContext db)
        {
            _context = db;
        }
        public QuizModel FindQuiz(int refId)
        {
            QuizModel? game = _context.Quizzes
                .Where(x => x.Id == refId)
                .FirstOrDefault();

            return game;
        }




    }
}
