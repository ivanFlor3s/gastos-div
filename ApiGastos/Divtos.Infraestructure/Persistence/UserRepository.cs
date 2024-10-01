using Divtos.Application.Common.Interfaces.Persistence;
using Divtos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divtos.Infraestructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new();
        public void Add(User user)
        {
            _users.Add(user);
        }

        public User? GetByEmail(string email)
        {
            return _users.SingleOrDefault(u => u.Email == email);
        }

        public Task<User> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
