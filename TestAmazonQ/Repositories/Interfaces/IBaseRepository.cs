#nullable disable

using TestAmazonQ.Models.Responses;

namespace TestAmazonQ.Repositories.Interfaces;

/// <summary>
/// Base repository interface for common CRUD operations
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface IBaseRepository<T> where T : class
{
    /// <summary>
    /// Gets entity by ID
    /// </summary>
    /// <param name="id">Entity ID</param>
    /// <returns>Entity if found</returns>
    Task<T> GetByIdAsync(int id);
    /// <summary>
    /// Gets all entities
    /// </summary>
    /// <returns>List of all entities</returns>
    Task<List<T>> GetAllAsync();
    /// <summary>
    /// Gets paginated entities
    /// </summary>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>Paginated result</returns>
    Task<PagedResult<T>> GetPagedAsync(int pageNumber, int pageSize);
    /// <summary>
    /// Adds new entity
    /// </summary>
    /// <param name="entity">Entity to add</param>
    /// <returns>Added entity</returns>
    Task<T> AddAsync(T entity);
    /// <summary>
    /// Updates existing entity
    /// </summary>
    /// <param name="entity">Entity to update</param>
    /// <returns>Updated entity</returns>
    Task<T> UpdateAsync(T entity);
    /// <summary>
    /// Deletes entity by ID
    /// </summary>
    /// <param name="id">Entity ID to delete</param>
    Task DeleteAsync(int id);
}