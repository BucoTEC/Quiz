using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quiz.Bll.Dtos;
using Quiz.Bll.Helpers;
using Quiz.Bll.SearchQueries;
using Quiz.Dal.Dtos;
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

        public async Task<Pagination<QuestionResponseDto>> SearchQuestions(SearchQuestionsQuery searchQuestionsQuery)
        {
            var questionSearchParams = BuildQuestionSearchParams(searchQuestionsQuery);

            var countSpec = new QuestionsCountSpecification(questionSearchParams);
            var spec = new QuestionsSearchSpecification(questionSearchParams);

            var totalCountOfQuestions = await _unitOfWork.QuestionRepository.CountAsync(countSpec);
            var question = await _unitOfWork.QuestionRepository.ListAsync(spec);

            var data = question.Select(question => BuildQuestionResponseDto(question));

            return new Pagination<QuestionResponseDto>(searchQuestionsQuery.PageIndex, searchQuestionsQuery.PageSize, totalCountOfQuestions, data);
        }

        public async Task<QuestionResponseDto> UpdateQuestion(Guid id, UpdateQuestionDto updateQuestionDto)
        {
            var existingQuestion = await _unitOfWork.QuestionRepository.GetByIdAsync(id) ?? throw new Exception("No question with this id");

            existingQuestion.QuestionText = updateQuestionDto.QuestionText;
            existingQuestion.QuestionAnswer = updateQuestionDto.QuestionAnswer;
            _unitOfWork.QuestionRepository.Update(existingQuestion);
            await _unitOfWork.CompleteAsync();

            return BuildQuestionResponseDto(existingQuestion);
        }

        private static QuestionSearchParams BuildQuestionSearchParams(SearchQuestionsQuery searchQuestionsQuery)
        {
            return new QuestionSearchParams
            {
                PageIndex = searchQuestionsQuery.PageIndex,
                PageSize = searchQuestionsQuery.PageSize,
                Sort = searchQuestionsQuery.Sort,
                Search = searchQuestionsQuery.SearchByQuestionText,
                IncludeQuizzes = searchQuestionsQuery.IncludeQuizzes
            };
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
