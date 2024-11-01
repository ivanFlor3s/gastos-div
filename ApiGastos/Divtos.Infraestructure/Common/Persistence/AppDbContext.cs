using Divtos.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divtos.Infraestructure.Common.Persistence
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppDbContext(DbContextOptions options) : base(options)
        {
            _httpContextAccessor = this.GetService<IHttpContextAccessor>();
            
        }
        public DbSet<User> Users { get; set; }


    }
}
