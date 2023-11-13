using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SportsAgentsUserService.Domain.Common;
using SportsAgentsUserService.Domain.Exceptions;
using SportsAgentsUserService.Infrastructure.Context;

namespace SportsAgentsUserService.Infrastructure.Common;

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
    // Add Entity
    public async Task<T> AddAsync(T entity)
    {
        _appDbContext.Set<T>().Add(entity);
        await _appDbContext.SaveChangesAsync();
        return entity;
    }

    // Get Entity
    public async Task<T?> GetByIdAsync(int id)
    {
        return await _appDbContext.Set<T>().FindAsync(id);
    }

    // Get Entity by Email
    public async Task<T> GetByEmailAsync(string email)
    {
        var entity = await _appDbContext.Set<T>().FirstOrDefaultAsync(e => EF.Property<string>(e, "Email") == email);

        if (entity == null)
        {
            throw new NotFoundException($"Item with Email={email} Not Found");
        }

        return entity;
    }

    // Get All Entities
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _appDbContext.Set<T>().ToListAsync();
    }

    // Find Entity
    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _appDbContext.Set<T>().Where(predicate).ToListAsync();
    }

    // Update Entity
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

    // Delete Entity
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