using System.Linq.Expressions;

namespace RepositoryMVC.Repositories.Interfaces
{
    /// <summary>
    /// Generic repository interface defining common CRUD operations
    /// This interface can be used for any entity type to ensure consistency
    /// </summary>
    /// <typeparam name="T">The entity type that implements this repository</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Retrieves all entities from the database asynchronously
        /// </summary>
        /// <returns>Collection of all entities</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Retrieves a single entity by its primary key asynchronously
        /// </summary>
        /// <param name="id">The primary key value</param>
        /// <returns>The entity if found, null otherwise</returns>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Finds entities that match the specified predicate
        /// </summary>
        /// <param name="predicate">The condition to match</param>
        /// <returns>Collection of matching entities</returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Adds a new entity to the database asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>Task representing the asynchronous operation</returns>
        Task AddAsync(T entity);

        /// <summary>
        /// Adds multiple entities to the database asynchronously
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>Task representing the asynchronous operation</returns>
        Task AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Updates an existing entity in the database
        /// </summary>
        /// <param name="entity">The entity to update</param>
        void Update(T entity);

        /// <summary>
        /// Removes an entity from the database
        /// </summary>
        /// <param name="entity">The entity to remove</param>
        void Remove(T entity);

        /// <summary>
        /// Removes multiple entities from the database
        /// </summary>
        /// <param name="entities">The entities to remove</param>
        void RemoveRange(IEnumerable<T> entities);

        /// <summary>
        /// Checks if any entity matches the specified predicate
        /// </summary>
        /// <param name="predicate">The condition to check</param>
        /// <returns>True if any entity matches, false otherwise</returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Counts entities that match the specified predicate
        /// </summary>
        /// <param name="predicate">Optional condition to count specific entities</param>
        /// <returns>Number of matching entities</returns>
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);

        /// <summary>
        /// Retrieves entities with paging support
        /// </summary>
        /// <param name="pageNumber">Page number (1-based)</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <returns>Paged collection of entities</returns>
        Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Retrieves entities with paging and ordering support
        /// </summary>
        /// <param name="pageNumber">Page number (1-based)</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="orderBy">Ordering expression</param>
        /// <param name="ascending">True for ascending order, false for descending</param>
        /// <returns>Paged and ordered collection of entities</returns>
        Task<IEnumerable<T>> GetPagedAsync<TKey>(int pageNumber, int pageSize,
            Expression<Func<T, TKey>> orderBy, bool ascending = true);
    }
}