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
        /// <summary>
        /// Retrieves a quiz by its unique identifier, optionally including its questions.
        /// </summary>
        /// <param name="id">The unique identifier of the quiz.</param>
        /// <param name="includeQuestions">A boolean value indicating whether to include questions associated with the quiz.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the DTO representing the retrieved quiz.</returns>
        /// <exception cref="NotFoundException">Thrown when no quiz is found with the specified identifier.</exception>
        Task<QuizResponseDto> GetQuizById(Guid id, bool includeQuestions);
        Task<QuizResponseDto> CreateQuiz(CreateQuizDto createQuizDto);


        Task<Pagination<QuizResponseDto>> SearchQuizzes(SearchQuizzesQuery searchQuizzesQuery);

        Task<QuizResponseDto> UpdateQuiz(Guid id, UpdateQuizDto updateQuizDto);

        Task DeleteQuiz(Guid id);

    }
}
