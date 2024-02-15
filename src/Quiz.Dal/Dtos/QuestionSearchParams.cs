namespace Quiz.Dal.Dtos;

/// <summary>
/// Represents the parameters for searching questions.
/// </summary>
public class QuestionSearchParams : BaseSearchParams
{
    /// <summary>
    /// Gets or sets a value indicating whether to include quizzes in the search results.
    /// </summary>
    public bool IncludeQuizzes { get; set; } = false;
}
