using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quiz.Bll.Dtos;
using Quiz.Bll.Helpers;
using Quiz.Bll.SearchQueries;

namespace Quiz.Bll.Services.QuestionService
{
    public interface IQuestionService
    {
        Task<QuestionResponseDto> CreateQuestion(CreateQuestionDto createQuestionDto);
        Task<QuestionResponseDto> UpdateQuestion(Guid id, UpdateQuestionDto updateQuestionDto);
        Task<QuestionResponseDto> GetQuestionById(Guid id, bool includeQuizzes);
        Task<Pagination<QuestionResponseDto>> SearchQuestions(SearchQuestionsQuery searchQuestionsQuery);
        Task DeleteQuestion(Guid id);
    }
}
