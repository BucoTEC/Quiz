using Quiz.Dal.Data;
using Quiz.Dal.Entities;
using Quiz.Dal.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Quiz.Dal.Repositories.GenericRepo;

/// <inheritdoc/>
public abstract class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context = context;

    /// <inheritdoc/>
    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    /// <inheritdoc/>
    public virtual async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    /// <inheritdoc/>
    public virtual async Task<T?> GetEntityWithSpec(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    /// <inheritdoc/>
    public virtual async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    /// <inheritdoc/>
    public virtual async Task<int> CountAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).CountAsync();
    }

    /// <inheritdoc/>
    public virtual void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    /// <inheritdoc/>
    public virtual void Update(T entity)
    {
        entity.UpdatedAt = DateTime.Now.ToUniversalTime();
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    /// <inheritdoc/>
    public virtual void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
    }
}
