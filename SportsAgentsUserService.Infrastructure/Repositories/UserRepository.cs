using SportsAgentsUserService.Domain.Entities;
using SportsAgentsUserService.Domain.Interfaces.Repositories;
using SportsAgentsUserService.Infrastructure.Common;
using SportsAgentsUserService.Infrastructure.Context;

namespace SportsAgentsUserService.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}