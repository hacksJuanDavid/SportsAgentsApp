using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SportsAgentsContactService.Domain.Common;
using SportsAgentsContactService.Domain.Exceptions;
using SportsAgentsContactService.Infrastructure.Context;

namespace SportsAgentsContactService.Infrastructure.Common;

public class Repository<T> : IRepository<T> where T : EntityBase
{
    // Vars
    private readonly AppDbContext _appDbContext;

    // Constructor
    protected Repository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    // Methods
    // Add a new entity to the database
    public async Task<T> AddAsync(T entity)
    {
        _appDbContext.Set<T>().Add(entity);
        await _appDbContext.SaveChangesAsync();
        return entity;
    }

    // Get an entity by its Id
    public async Task<T?> GetByIdAsync(int id)
    {
        return await _appDbContext.Set<T>().FindAsync(id);
    }

    // Get all entities
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _appDbContext.Set<T>().ToListAsync();
    }

    // Get all entities that match a predicate
    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _appDbContext.Set<T>().Where(predicate).ToListAsync();
    }

    // Update an entity
    public async Task<T?> UpdateAsync(T? entity)
    {
        var id = entity?.Id;
        var original = await _appDbContext.Set<T>().FindAsync(id);

        if (original is null)
        {
            throw new NotFoundException($"Item with Id={id} Not Found");
        }

        _appDbContext.Entry(original).CurrentValues.SetValues(entity!);
        await _appDbContext.SaveChangesAsync();

        return entity!;
    }

    // Delete an entity
    public async Task<T?> DeleteAsync(T? entity)
    {
        var id = entity?.Id;
        var original = await _appDbContext.Set<T>().FindAsync(id);

        if (original is null)
        {
            throw new NotFoundException($"Item with Id={id} Not Found");
        }

        _appDbContext.Set<T>().Remove(entity!);
        await _appDbContext.SaveChangesAsync();

        return entity!;
    }
}