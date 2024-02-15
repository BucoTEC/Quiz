using Quiz.Bll.Dtos;
using Quiz.Bll.Helpers;
using Quiz.Bll.SearchQueries;

namespace Quiz.Bll.Services.QuizService;

/// <summary>
/// Represents the service for managing quizzes.
/// </summary>
public interface IQuizService
{
    /// <summary>
    /// Retrieves a quiz by its unique identifier, optionally including its questions.
    /// </summary>
    /// <param name="id">The unique identifier of the quiz.</param>
    /// <param name="includeQuestions">A boolean value indicating whether to include questions associated with the quiz.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the DTO representing the retrieved quiz.</returns>
    Task<QuizResponseDto> GetQuizById(Guid id, bool includeQuestions);

    /// <summary>
    /// Creates a new quiz.
    /// </summary>
    /// <param name="createQuizDto">The DTO containing information for creating the quiz.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the DTO representing the created quiz.</returns>
    Task<QuizResponseDto> CreateQuiz(CreateQuizDto createQuizDto);

    /// <summary>
    /// Searches for quizzes based on the specified search criteria.
    /// </summary>
    /// <param name="searchQuizzesQuery">The query containing the search parameters.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the paginated list of quiz DTOs matching the search criteria.</returns>
    Task<Pagination<QuizResponseDto>> SearchQuizzes(SearchQuizzesQuery searchQuizzesQuery);

    /// <summary>
    /// Updates an existing quiz.
    /// </summary>
    /// <param name="id">The unique identifier of the quiz to update.</param>
    /// <param name="updateQuizDto">The DTO containing updated information for the quiz.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the DTO representing the updated quiz.</returns>
    Task<QuizResponseDto> UpdateQuiz(Guid id, UpdateQuizDto updateQuizDto);

    /// <summary>
    /// Deletes a quiz by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the quiz to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteQuiz(Guid id);

    /// <summary>
    /// Exports a quiz in the specified format.
    /// </summary>
    /// <param name="exporter">The name of the exporter to use for exporting the quiz.</param>
    /// <param name="id">The unique identifier of the quiz to export.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the DTO representing the exported quiz.</returns>
    Task<ExportQuizResponseDto> ExportQuiz(string exporter, Guid id);

    /// <summary>
    /// Retrieves the names of available exporters.
    /// </summary>
    /// <returns>An array containing the names of available exporters.</returns>
    string[] GetAvailableExporters();
}
