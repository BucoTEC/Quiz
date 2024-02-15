namespace Quiz.Bll.Helpers;

/// <summary>
/// Represents a pagination result containing a page of data.
/// </summary>
/// <typeparam name="T">The type of data.</typeparam>
public class Pagination<T>(int pageIndex, int pageSize, int count, IEnumerable<T> data) where T : class
{
    /// <summary>
    /// Gets or sets the index of the current page.
    /// </summary>
    public int PageIndex { get; set; } = pageIndex;

    /// <summary>
    /// Gets or sets the size of each page.
    /// </summary>
    public int PageSize { get; set; } = pageSize;

    /// <summary>
    /// Gets or sets the total number of items across all pages.
    /// </summary>
    public int Count { get; set; } = count;

    /// <summary>
    /// Gets or sets the collection of data for the current page.
    /// </summary>
    public IEnumerable<T>? Data { get; set; } = data;
}
