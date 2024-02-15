using Quiz.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Quiz.Dal.Specifications;

/// <summary>
/// Represents a class for evaluating specifications and building queries.
/// </summary>
/// <typeparam name="TEntity">The type of entity.</typeparam>
public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// Gets the query based on the provided input query and specification.
    /// </summary>
    /// <param name="inputQuery">The input query to apply the specification to.</param>
    /// <param name="spec">The specification to apply.</param>
    /// <returns>The resulting query after applying the specification.</returns>
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
    {
        var query = inputQuery;

        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }

        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        if (spec.IsPagingEnabled)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }
}
