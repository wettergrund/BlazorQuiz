﻿using BlazorQuiz.Server.Data;
using BlazorQuiz.Server.Models;
using BlazorQuiz.Server.Models.ViewModels;
using BlazorQuiz.Server.ViewModels;
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

        public async Task<QuizViewModel> CreateNewGameAsync(string quizId, string userId)
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

            // Questions belonging to specific quiz
            List<QuestionModel> questionsByQuiz = _context.QuestionModels.Where(q => q.QuizRefId == quiz.PublicId).ToList();

            //List to be filled with questions
            List<QuestionViewModel> quizQuestions = new();

            //Turn questions into viewmodel
            foreach (var question in questionsByQuiz)
            {
                QuestionViewModel questionViewModel = new(_context, question);

                quizQuestions.Add(questionViewModel);
            }


            QuizViewModel displayQuiz = new()
            {
                title = quiz.Name,
                timer = quiz.Timer,
                publicId = quiz.PublicId,
                questions = quizQuestions,
                gameId = newGame.Id
            };


            // Return the newly created game
            return displayQuiz;
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
            foreach (var question in questions)
            {


                var newQuestion = new QuestionModel
                {
                    Question = question.Question,
                    Answer1 = question.Answer1,
                    Answer2 = question.Answer2,
                    Answer3 = question.Answer3,
                    Answer4 = question.Answer4,
                    QuizRefId = newQuiz.PublicId
                };

                if (!string.IsNullOrEmpty(question.QuizImageUrl))
                {

                    var media = await _mediaService.GetMediaByIdAsync(Guid.Parse(question.QuizImageUrl));
                    int mediaID = media.Id;

                    newQuestion.MediaRefId = mediaID;


                }

                // Add the new question to the context
                _context.QuestionModels.Add(newQuestion);
            }

            // Save the questions to the database
            await _context.SaveChangesAsync();

            // Return the newly created quiz
            return newQuiz;
        }

        public async Task<GameWithQuizViewModel> GetQuizByPublicIdAsync(string id)
        {
            var userQuiz = await _context.UserQuizModels
                             .Include(uq => uq.Quiz)
                             .ThenInclude(q => q.Questions)
                             .FirstOrDefaultAsync(game => game.QuizRefPublicId == id);

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

        public async Task<bool> CheckGuess(GuessCheckViewModel guess)
        {
            var correctAnswer = _context.QuestionModels.Where(questions => questions.Id == guess.GuessId).Select(question => question.Answer1).Single();

            if (correctAnswer == guess.Guess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<UserQuizModel> FinishedGame(UserQuizModel quiz, List<GuessCheckViewModel> guesses)
        {
            //throw new NotImplementedException();

            int score = 0;

            foreach (var guess in guesses)
            {
                bool guessStatus = await CheckGuess(guess);

                if (guessStatus == true)
                {
                    score += 1000;
                    if (guess.Seconds != null)
                    {
                        score -= guess.Seconds;
                    }
                }
            }

            var quizToUpdate = _context.UserQuizModels.FirstOrDefault(q => q.Id == quiz.Id);

            quizToUpdate.Score = score;
            await _context.SaveChangesAsync();

            //Update game with

            return quizToUpdate;


        }


        private QuizModel FindQuiz(string refId)
        {
            QuizModel? game = _context.Quizzes
                .Where(x => x.PublicId == refId)
                .FirstOrDefault();

            return game;
        }

        public UserQuizModel FindUserQuiz(int id)
        {
            UserQuizModel? game = _context.UserQuizModels.Where(g => g.Id == id).FirstOrDefault();

            return game;
        }

        public List<QuestionModel> FindQuestionsByQuizRef(string quizRef)
        {

            var questions = _context.QuestionModels.Where(q => q.QuizRefId == quizRef).ToList();
            return questions;

        }
    }
}
