namespace Quiz.Dal.Dtos;
public abstract class BaseSearchParams
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
}
