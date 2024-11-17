using Divtos.Application.Common.Interfaces.Persistence;
using Divtos.Domain.Entities;
using Divtos.Infraestructure.Common.Persistence;

namespace Divtos.Infraestructure.Groups.Persistance
{
    public class GroupRepository : Repository<Group, int>, IGroupRepository
    {
        public GroupRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

    }
}
