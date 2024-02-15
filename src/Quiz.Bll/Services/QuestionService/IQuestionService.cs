using Quiz.Bll.Dtos;
using Quiz.Bll.Helpers;
using Quiz.Bll.SearchQueries;

namespace Quiz.Bll.Services.QuestionService;

/// <summary>
/// Service for managing questions.
/// </summary>
public interface IQuestionService
{
    /// <summary>
    /// Retrieves a question by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the question.</param>
    /// <param name="includeQuizzes">Optional. Indicates whether to include quizzes associated with the question. Default is false.</param>
    /// <returns>The requested question.</returns>
    Task<QuestionResponseDto> GetQuestionById(Guid id, bool includeQuizzes);

    /// <summary>
    /// Searches for questions based on the provided query parameters.
    /// </summary>
    /// <param name="searchQuestionsQuery">The query parameters for searching questions.</param>
    /// <returns>A paginated list of matching questions.</returns>
    Task<Pagination<QuestionResponseDto>> SearchQuestions(SearchQuestionsQuery searchQuestionsQuery);

    /// <summary>
    /// Creates a new question.
    /// </summary>
    /// <param name="createQuestionDto">The data for creating the question.</param>
    /// <returns>The created question.</returns>
    Task<QuestionResponseDto> CreateQuestion(CreateQuestionDto createQuestionDto);

    /// <summary>
    /// Updates an existing question.
    /// </summary>
    /// <param name="id">The unique identifier of the question to update.</param>
    /// <param name="updateQuestionDto">The data for updating the question.</param>
    /// <returns>The updated question.</returns>
    Task<QuestionResponseDto> UpdateQuestion(Guid id, UpdateQuestionDto updateQuestionDto);

    /// <summary>
    /// Deletes a question by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the question to delete.</param>
    Task DeleteQuestion(Guid id);
}
