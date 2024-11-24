using Divtos.Domain.Entities;

namespace Divtos.Application.Common.Interfaces.Persistence
{
    public interface IGroupRepository: IRepository<Group,int>
    {
        public IQueryable<Group> GetAll();
        public IQueryable<Group?> GetDetail(int idGroup);
        public Task<Group?> GetDetailAsync(int idGroup);
    }
}
