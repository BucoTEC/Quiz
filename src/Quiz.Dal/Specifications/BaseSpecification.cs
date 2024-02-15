using System.Linq.Expressions;

namespace Quiz.Dal.Specifications;

/// <inheritdoc/>
public abstract class BaseSpecification<T> : ISpecification<T>
{

    // suppress nullability warnings for sake of base specification reausiblibty

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public BaseSpecification()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public BaseSpecification(Expression<Func<T, bool>> criteria)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        Criteria = criteria;
    }

    /// <inheritdoc/>
    public Expression<Func<T, bool>> Criteria { get; }

    /// <inheritdoc/>
    public List<Expression<Func<T, object>>> Includes { get; } =
        new List<Expression<Func<T, object>>>();

    /// <inheritdoc/>
    public Expression<Func<T, object>> OrderBy { get; private set; }

    /// <inheritdoc/>
    public Expression<Func<T, object>> OrderByDescending { get; private set; }

    /// <inheritdoc/>
    public int Take { get; private set; }

    /// <inheritdoc/>
    public int Skip { get; private set; }

    /// <inheritdoc/>
    public bool IsPagingEnabled { get; private set; }

    /// <inheritdoc/>
    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    /// <inheritdoc/>
    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    /// <inheritdoc/>
    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }

    /// <inheritdoc/>
    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}
