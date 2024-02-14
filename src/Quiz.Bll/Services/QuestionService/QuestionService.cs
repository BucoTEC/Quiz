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
            return new QuestionResponseDto(questionEntity.Id, questionEntity.QuestionText, questionEntity.QuestionAnswer);

        }
    }
}
