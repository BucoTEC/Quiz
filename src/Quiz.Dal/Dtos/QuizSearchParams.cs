namespace Quiz.Dal.Dtos;

/// <summary>
/// Represents the parameters for searching quizzes.
/// </summary>
public class QuizSearchParams
{
    /// <summary>
    /// Gets or sets the index of the page to retrieve.
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    /// Gets or sets the size of the page.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the sorting criteria.
    /// </summary>
    public string? Sort { get; set; }

    /// <summary>
    /// Gets or sets the search query.
    /// </summary>
    public string? Search { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to include questions in the search results.
    /// </summary>
    public bool IncludeQuestions { get; set; } = false;
}
