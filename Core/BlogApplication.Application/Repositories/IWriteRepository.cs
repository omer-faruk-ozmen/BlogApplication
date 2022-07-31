using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApplication.Domain.Entities.Common;

namespace BlogApplication.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(List<T> entities);
        bool Update(T entity);
        Task<bool> RemoveAsync(string id);
        bool Remove(T entity);
        bool RemoveRange(List<T> entities);
        Task<int> SaveAsync();


    }
}
