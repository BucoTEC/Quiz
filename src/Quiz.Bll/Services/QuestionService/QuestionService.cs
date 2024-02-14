using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quiz.Bll.Dtos;
using Quiz.Dal.Entities;
using Quiz.Dal.Repositories.Uow;
using Quiz.Dal.Specifications.QuestionSearch;

namespace Quiz.Bll.Services.QuestionService
{
    public class QuestionService(IUnitOfWork unitOfWork) : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<QuestionResponseDto> CreateQuestion(CreateQuestionDto createQuestionDto)
        {
            var spec = new QuestionsSearchSpecification(createQuestionDto.QuestionText);
            var existingQuestion = await _unitOfWork.QuestionRepository.GetEntityWithSpec(spec);
            if (existingQuestion is not null) throw new Exception("Same question already exists");

            var newQuestion = BuildQuestionEntity(createQuestionDto);
            _unitOfWork.QuestionRepository.Add(newQuestion);
            await _unitOfWork.CompleteAsync();

            return BuildQuestionResponseDto(newQuestion);
        }


        public async Task<QuestionResponseDto> GetQuestionById(Guid id, bool includeQuizzes)
        {
            QuestionEntity question;

            if (includeQuizzes)
            {
                var spec = new QuestionWithQuizzesSpecification(id);
                question = await _unitOfWork.QuestionRepository.GetEntityWithSpec(spec) ?? throw new Exception("No question with this id");
            }
            else
            {
                question = await _unitOfWork.QuestionRepository.GetByIdAsync(id) ?? throw new Exception("No question with this id");
            }

            return BuildQuestionResponseDto(question);
        }

        private static QuestionEntity BuildQuestionEntity(CreateQuestionDto createQuestionDto)
        {
            return new QuestionEntity
            {
                QuestionText = createQuestionDto.QuestionText,
                QuestionAnswer = createQuestionDto.QuestionAnswer
            };
        }

        private static QuestionResponseDto BuildQuestionResponseDto(QuestionEntity questionEntity)
        {
            var QuizResponsesDto = questionEntity.Quizzes.Select(q => new QuizResponseDto(q.Id, q.Name, null)).ToList();
            return new QuestionResponseDto(questionEntity.Id, questionEntity.QuestionText, questionEntity.QuestionAnswer, QuizResponsesDto);

        }
    }
}
