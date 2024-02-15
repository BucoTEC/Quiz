namespace Quiz.Dal.Dtos;

/// <summary>
/// Represents the parameters for searching quizzes.
/// </summary>
public class QuizSearchParams : BaseSearchParams
{
    /// <summary>
    /// Gets or sets a value indicating whether to include questions in the search results.
    /// </summary>
    public bool IncludeQuestions { get; set; } = false;
}
