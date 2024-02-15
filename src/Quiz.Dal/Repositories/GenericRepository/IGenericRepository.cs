using Quiz.Dal.Entities;
using Quiz.Dal.Specifications;

namespace Quiz.Dal.Repositories.GenericRepo;

/// <summary>
/// Represents a generic repository for entities of type T.
/// </summary>
/// <typeparam name="T">The type of entity.</typeparam>
public interface IGenericRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Asynchronously retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the entity with the specified identifier, if found; otherwise, null.</returns>
    Task<T?> GetByIdAsync(Guid id);

    /// <summary>
    /// Asynchronously retrieves all entities of type T.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. The task result contains a read-only list of entities.</returns>
    Task<IReadOnlyList<T>> ListAllAsync();

    /// <summary>
    /// Asynchronously retrieves an entity based on the specified specification.
    /// </summary>
    /// <param name="spec">The specification to apply.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the entity matching the specification, if found; otherwise, null.</returns>
    Task<T?> GetEntityWithSpec(ISpecification<T> spec);

    /// <summary>
    /// Asynchronously retrieves entities based on the specified specification.
    /// </summary>
    /// <param name="spec">The specification to apply.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a read-only list of entities matching the specification.</returns>
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

    /// <summary>
    /// Asynchronously counts the number of entities matching the specified specification.
    /// </summary>
    /// <param name="spec">The specification to apply.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the count of entities matching the specification.</returns>
    Task<int> CountAsync(ISpecification<T> spec);

    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    void Add(T entity);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    void Update(T entity);

    /// <summary>
    /// Deletes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    void Delete(T entity);
}
