using BlazorQuiz.Server.Data;
using BlazorQuiz.Server.Models;
using BlazorQuiz.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BlazorQuiz.Server.Services
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediaService _mediaService;


        public GameService(ApplicationDbContext context, IMediaService mediaService)
        {
            _context = context;
            _mediaService = mediaService;

        }
        public async Task<UserQuizModel> CreateNewGameAsync(int quizId, string userId)
        {
            // Use the DbHelper or an equivalent repository to find the quiz based on ID
            var quiz = FindQuiz(quizId);


            // Create a new game
            var newGame = new UserQuizModel
            {
                QuizRefPublicId = quiz.PublicId,
                UserRefId = userId,
                Score = 0,
            };

            // Add the new game to the context
            _context.UserQuizModels.Add(newGame);
            await _context.SaveChangesAsync();

            // Return the newly created game
            return newGame;
        }

        public async Task<QuizModel> CreateQuizAsync(string title, List<NewQuestionViewModel> questions, int seconds, string userId)
        {
            // Create a new quiz
            var newQuiz = new QuizModel
            {
                Name = title,
                PublicId = Guid.NewGuid().ToString(),
                Timer = seconds,
                UserRefId = userId
            };

            // Add the new quiz to the context
            _context.Quizzes.Add(newQuiz);
            await _context.SaveChangesAsync();

            // Create new questions and bind them to the quiz.
            foreach (var questionViewModel in questions)
            {
                var media = await _mediaService.GetMediaByIdAsync(Guid.Parse(questionViewModel.QuizImageUrl));
                int mediaID = media.Id;

                var newQuestion = new QuestionModel
                {
                    Question = questionViewModel.Question,
                    Answer1 = questionViewModel.Answer1,
                    Answer2 = questionViewModel.Answer2,
                    Answer3 = questionViewModel.Answer3,
                    Answer4 = questionViewModel.Answer4,
                    QuizRefId = newQuiz.PublicId
                };

                if (mediaID > 0)
                {
                    newQuestion.MediaRefId = mediaID;

                }
                else
                {
                    newQuestion.MediaRefId = 0;
                }

                // Add the new question to the context
                _context.QuestionModels.Add(newQuestion);
            }

            // Save the questions to the database
            await _context.SaveChangesAsync();

            // Return the newly created quiz
            return newQuiz;
        }

        public async Task<GameWithQuizViewModel> GetUserQuizAsync(int id)
        {
            var userQuiz = await _context.UserQuizModels
                             .Include(uq => uq.Quiz)
                             .ThenInclude(q => q.Questions)
                             .FirstOrDefaultAsync(game => game.Id == id);

            if (userQuiz == null)
            {
                // Handle the case where the UserQuiz is not found.
                return null;
            }

            var quiz = userQuiz.Quiz;

            var viewModel = new GameWithQuizViewModel
            {
                UserQuizId = userQuiz.Id,
                Score = userQuiz.Score,
                QuizId = quiz.Id,
                QuizPublicId = quiz.PublicId,
                QuizName = quiz.Name,
                QuizTimer = quiz.Timer,
                Questions = quiz.Questions
            };

            return viewModel;
        }

        public Task<bool> UpdateGameAsync(int gameId, string gameState)
        {
            throw new NotImplementedException();
        }

        private QuizModel FindQuiz(int refId)
        {
            QuizModel? game = _context.Quizzes
                .Where(x => x.Id == refId)
                .FirstOrDefault();

            return game;
        }
    }
}
