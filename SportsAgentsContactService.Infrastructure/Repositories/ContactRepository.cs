using SportsAgentsContactService.Domain.Entities;
using SportsAgentsContactService.Domain.Interfaces.Repositories;
using SportsAgentsContactService.Infrastructure.Common;
using SportsAgentsContactService.Infrastructure.Context;

namespace SportsAgentsContactService.Infrastructure.Repositories;

public class ContactRepository : Repository<Contact>, IContactRepository
{
    public ContactRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}