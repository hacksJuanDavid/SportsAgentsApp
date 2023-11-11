using System.Linq.Expressions;

namespace SportsAgentsContactService.Domain.Common;

public interface IRepository<T> where T : EntityBase
{
    // Add entity to database
    Task<T> AddAsync(T entity);

    // Get entity by id
    Task<T?> GetByIdAsync(int id);

    // Get all entities
    Task<IEnumerable<T>> GetAllAsync();

    // Get entities by predicate
    public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    // Update entity
    Task<T?> UpdateAsync(T? entity);

    // Delete entity
    Task<T?> DeleteAsync(T? entity);
}