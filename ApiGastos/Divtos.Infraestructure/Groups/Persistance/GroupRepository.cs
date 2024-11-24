using Divtos.Application.Common.Interfaces.Persistence;
using Divtos.Domain.Entities;
using Divtos.Infraestructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Divtos.Infraestructure.Groups.Persistance
{
    public class GroupRepository : Repository<Group, int>, IGroupRepository
    {
        public GroupRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Group> GetAll()
        {
            var query = _dbSet.Include(groupDb => groupDb.GroupUsers)
                .ThenInclude(groupUserDb => groupUserDb.User)
                .Include(group => group.Spents)
                .AsQueryable();
                
            return query;
        }

        public IQueryable<Group?> GetDetail(int idGroup)
        {
            var query = this._dbSet.Include(groupDb => groupDb.GroupUsers)
                .ThenInclude(groupUserDb => groupUserDb.User)
                .Include(group => group.Spents)
                .ThenInclude(spents => spents.Author)
                .Include(group => group.Spents)
                .ThenInclude(spents => spents.Participants)
                .ThenInclude(participant => participant.User)
                .OrderByDescending(group => group.CreatedAt)
                .Where(groupDb => groupDb.Id == idGroup);
            
            return query;
        }
        
        public async Task<Group?> GetDetailAsync(int idGroup)
        {
            var group = await GetDetail(idGroup).FirstOrDefaultAsync();
            return group;
        }
    }
}
