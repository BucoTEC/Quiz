namespace Quiz.Bll.SearchQueries;

/// <summary>
/// Represents the base search query parameters.
/// </summary>
public abstract class BaseSearchQuery
{
    private const int MaxPageSize = 50;

    /// <summary>
    /// Gets or sets the index of the page to retrieve.
    /// </summary>
    public int PageIndex { get; set; } = 1;

    private int _pageSize = 6;

    /// <summary>
    /// Gets or sets the size of each page.
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    /// <summary>
    /// Gets or sets the field to sort the results by.
    /// </summary>
    public string? Sort { get; set; }
}
