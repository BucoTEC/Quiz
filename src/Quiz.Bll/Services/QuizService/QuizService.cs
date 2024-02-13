using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quiz.Bll.Dtos;
using Quiz.Dal.Entities;
using Quiz.Dal.Repositories.Uow;
using Quiz.Dal.Specifications;

namespace Quiz.Bll.Services.QuizService
{
    public class QuizService(IUnitOfWork unitOfWork) : IQuizService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<QuizResponseDto> CreateQuiz(CreateQuizDto createQuizDto)
        {
            // TODO check if quiz with same name exists if yes throw exception

            //TODO check that questions coming in has unique question text name

            var newQuiz = BuildQuizEntity(createQuizDto);

            _unitOfWork.QuizRepository.Add(newQuiz);
            await _unitOfWork.CompleteAsync();

            return BuildQuizResponse(newQuiz);

        }

        public async Task<QuizResponseDto> GetQuizById(Guid id, bool includeQuestions)
        {
            QuizEntity quiz;

            if (includeQuestions)
            {
                var spec = new QuizWithQuestionsSpecification(id);
                quiz = await _unitOfWork.QuizRepository.GetEntityWithSpec(spec) ?? throw new Exception("No quiz with this id");
            }
            else
            {
                quiz = await _unitOfWork.QuizRepository.GetByIdAsync(id) ?? throw new Exception("No quiz with this id");
            }

            return BuildQuizResponse(quiz);

        }

        private static QuizEntity BuildQuizEntity(CreateQuizDto createQuizDto)
        {
            var questions = createQuizDto.Questions?.Select(q => new QuestionEntity { QuestionText = q.QuestionText, QuestionAnswer = q.QuestionAnswer });
            var quiz = new QuizEntity
            {
                Name = createQuizDto.Name,
                Questions = questions?.ToList() ?? []
            };

            return quiz;
        }

        private static QuizResponseDto BuildQuizResponse(QuizEntity quizEntity)
        {
            return new QuizResponseDto(quizEntity.Id, quizEntity.Name, quizEntity.Questions);
        }
    }
}
