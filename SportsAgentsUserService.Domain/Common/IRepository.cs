using System.Linq.Expressions;

namespace SportsAgentsUserService.Domain.Common;

public interface IRepository<T> where T : EntityBase
{
    // Add entity to database 
    public Task<T> AddAsync(T entity);

    // Get entity by id
    public Task<T?> GetByIdAsync(int id);

    // Get entity by email
    public Task<T> GetByEmailAsync(string email);

    // Get all entities
    public Task<IEnumerable<T>> GetAllAsync();

    // Get all entities by predicate
    public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    // Update entity
    public Task<T?> UpdateAsync(T? entity);

    // Delete entity
    public Task<T?> DeleteAsync(T? entity);
}