using Divtos.Application.Common.Interfaces.Persistence;
using Divtos.Domain.Entities;
using Divtos.Infraestructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Divtos.Infraestructure.Users.Persistence
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var userQuery = _dbSet.Where(u => u.Email == email);
            var result = await userQuery.FirstOrDefaultAsync();
            return result;
        }

    }
}
