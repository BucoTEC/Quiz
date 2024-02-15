
using System.Linq.Expressions;

namespace Quiz.Dal.Specifications;

/// <summary>
/// Represents a specification interface for querying entities.
/// </summary>
/// <typeparam name="T">The type of entity.</typeparam>
public interface ISpecification<T>
{
    /// <summary>
    /// Gets the criteria expression for filtering entities.
    /// </summary>
    Expression<Func<T, bool>> Criteria { get; }

    /// <summary>
    /// Gets the list of include expressions for eager loading related entities.
    /// </summary>
    List<Expression<Func<T, object>>> Includes { get; }

    /// <summary>
    /// Gets the expression for ordering entities in ascending order.
    /// </summary>
    Expression<Func<T, object>> OrderBy { get; }

    /// <summary>
    /// Gets the expression for ordering entities in descending order.
    /// </summary>
    Expression<Func<T, object>> OrderByDescending { get; }

    /// <summary>
    /// Gets the number of entities to take.
    /// </summary>
    int Take { get; }

    /// <summary>
    /// Gets the number of entities to skip.
    /// </summary>
    int Skip { get; }

    /// <summary>
    /// Gets a value indicating whether paging is enabled.
    /// </summary>
    bool IsPagingEnabled { get; }
}
