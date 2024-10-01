using Divtos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divtos.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        User? GetByEmail(string email);
        Task<User> GetById(Guid id);
        void Add(User user);
    }
}
