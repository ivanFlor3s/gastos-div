using Divtos.Application.Common.Interfaces.Persistence;
using Divtos.Infraestructure.Groups.Persistance;
using Divtos.Infraestructure.Users.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divtos.Infraestructure.Common.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; private set; }
        public IGroupRepository Groups { get; private set; }
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Groups = new GroupRepository(_context);
        }


        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
