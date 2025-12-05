using Microsoft.EntityFrameworkCore;
using RepositoryMVC.Data;
using RepositoryMVC.Repositories.Interfaces;
using System.Linq.Expressions;

namespace RepositoryMVC.Repositories.Implementations
{
    /// <summary>
    /// Generic repository implementation providing common CRUD operations
    /// This class serves as the base implementation for all entity repositories
    /// </summary>
    /// <typeparam name="T">The entity type that this repository manages</typeparam>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// Constructor that receives the database context
        /// </summary>
        /// <param name="context">The application database context</param>
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Retrieves all entities from the database asynchronously
        /// </summary>
        /// <returns>Collection of all entities</returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving all entities of type {typeof(T).Name}", ex);
            }
        }

        /// <summary>
        /// Retrieves a single entity by its primary key asynchronously
        /// </summary>
        /// <param name="id">The primary key value</param>
        /// <returns>The entity if found, null otherwise</returns>
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving entity of type {typeof(T).Name} with ID {id}", ex);
            }
        }

        /// <summary>
        /// Finds entities that match the specified predicate
        /// </summary>
        /// <param name="predicate">The condition to match</param>
        /// <returns>Collection of matching entities</returns>
        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _dbSet.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error finding entities of type {typeof(T).Name}", ex);
            }
        }

        /// <summary>
        /// Adds a new entity to the database asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>Task representing the asynchronous operation</returns>
        public virtual async Task AddAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                await _dbSet.AddAsync(entity);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error adding entity of type {typeof(T).Name}", ex);
            }
        }

        /// <summary>
        /// Adds multiple entities to the database asynchronously
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>Task representing the asynchronous operation</returns>
        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                await _dbSet.AddRangeAsync(entities);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error adding multiple entities of type {typeof(T).Name}", ex);
            }
        }

        /// <summary>
        /// Updates an existing entity in the database
        /// </summary>
        /// <param name="entity">The entity to update</param>
        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _dbSet.Update(entity);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error updating entity of type {typeof(T).Name}", ex);
            }
        }

        /// <summary>
        /// Removes an entity from the database
        /// </summary>
        /// <param name="entity">The entity to remove</param>
        public virtual void Remove(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error removing entity of type {typeof(T).Name}", ex);
            }
        }

        /// <summary>
        /// Removes multiple entities from the database
        /// </summary>
        /// <param name="entities">The entities to remove</param>
        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                _dbSet.RemoveRange(entities);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error removing multiple entities of type {typeof(T).Name}", ex);
            }
        }

        /// <summary>
        /// Checks if any entity matches the specified predicate
        /// </summary>
        /// <param name="predicate">The condition to check</param>
        /// <returns>True if any entity matches, false otherwise</returns>
        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _dbSet.AnyAsync(predicate);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error checking existence of entities of type {typeof(T).Name}", ex);
            }
        }

        /// <summary>
        /// Counts entities that match the specified predicate
        /// </summary>
        /// <param name="predicate">Optional condition to count specific entities</param>
        /// <returns>Number of matching entities</returns>
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            try
            {
                if (predicate == null)
                    return await _dbSet.CountAsync();

                return await _dbSet.CountAsync(predicate);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error counting entities of type {typeof(T).Name}", ex);
            }
        }

        /// <summary>
        /// Retrieves entities with paging support
        /// </summary>
        /// <param name="pageNumber">Page number (1-based)</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <returns>Paged collection of entities</returns>
        public virtual async Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize)
        {
            try
            {
                if (pageNumber < 1)
                    throw new ArgumentException("Page number must be greater than 0", nameof(pageNumber));

                if (pageSize < 1)
                    throw new ArgumentException("Page size must be greater than 0", nameof(pageSize));

                return await _dbSet
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving paged entities of type {typeof(T).Name}", ex);
            }
        }

        /// <summary>
        /// Retrieves entities with paging and ordering support
        /// </summary>
        /// <param name="pageNumber">Page number (1-based)</param>
        /// <param name="pageSize">Number of items per page</param>
        /// <param name="orderBy">Ordering expression</param>
        /// <param name="ascending">True for ascending order, false for descending</param>
        /// <returns>Paged and ordered collection of entities</returns>
        public virtual async Task<IEnumerable<T>> GetPagedAsync<TKey>(int pageNumber, int pageSize,
            Expression<Func<T, TKey>> orderBy, bool ascending = true)
        {
            try
            {
                if (pageNumber < 1)
                    throw new ArgumentException("Page number must be greater than 0", nameof(pageNumber));

                if (pageSize < 1)
                    throw new ArgumentException("Page size must be greater than 0", nameof(pageSize));

                if (orderBy == null)
                    throw new ArgumentNullException(nameof(orderBy));

                var query = ascending
                    ? _dbSet.OrderBy(orderBy)
                    : _dbSet.OrderByDescending(orderBy);

                return await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving paged and ordered entities of type {typeof(T).Name}", ex);
            }
        }
    }
}