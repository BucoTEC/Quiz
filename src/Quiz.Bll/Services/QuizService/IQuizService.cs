using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quiz.Bll.Dtos;
using Quiz.Bll.Helpers;
using Quiz.Bll.SearchQueries;

namespace Quiz.Bll.Services.QuizService
{
    public interface IQuizService
    {
        Task<QuizResponseDto> CreateQuiz(CreateQuizDto createQuizDto);

        Task<QuizResponseDto> GetQuizById(Guid id, bool includeQuestions);

        Task<Pagination<QuizResponseDto>> SearchQuizzes(SearchQuizzesQuery searchQuizzesQuery);

        Task<QuizResponseDto> UpdateQuiz(Guid id, UpdateQuizDto updateQuizDto);

        Task DeleteQuiz(Guid id);

    }
}
