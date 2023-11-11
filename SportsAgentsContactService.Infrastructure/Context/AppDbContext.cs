using Microsoft.EntityFrameworkCore;
using SportsAgentsContactService.Domain.Entities;

namespace SportsAgentsContactService.Infrastructure.Context;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Contact> Contacts { get; set; } = null!;
}