using Microsoft.EntityFrameworkCore;
using SportsAgentsUserService.Domain.Entities;

namespace SportsAgentsUserService.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
}