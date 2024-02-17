namespace Quiz.Bll.SearchQueries;

/// <summary>
/// Represents the search query parameters for quizzes.
/// </summary>
public class SearchQuizzesQuery : BaseSearchQuery
{
    private string? _search;

    /// <summary>
    /// Gets or sets the search term for filtering quizzes by quiz name.
    /// </summary>
    public string SearchByQuizName
    {
        get => _search ?? "";
        set => _search = value.ToLower().Trim();
    }

    /// <summary>
    /// Gets or sets a value indicating whether to include associated questions in the search results.
    /// </summary>
    public bool IncludeQuestions { get; set; } = false;
}
