using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divtos.Application.Common.Interfaces.Persistence
{
    public interface IRepository<T, TKey> where T : class
    {
        Task<T?> GetByIdAsync(TKey id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }

}
