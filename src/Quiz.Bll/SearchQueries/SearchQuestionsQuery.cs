namespace Quiz.Bll.SearchQueries;

/// <summary>
/// Represents the search query parameters for questions.
/// </summary>
public class SearchQuestionsQuery : BaseSearchQuery
{
    private string? _search;

    /// <summary>
    /// Gets or sets the search term for filtering questions by question text.
    /// </summary>
    public string SearchByQuestionText
    {
        get => _search ?? "";
        set => _search = value.ToLower();
    }

    /// <summary>
    /// Gets or sets a value indicating whether to include associated quizzes in the search results.
    /// </summary>
    public bool IncludeQuizzes { get; set; } = false;
}
