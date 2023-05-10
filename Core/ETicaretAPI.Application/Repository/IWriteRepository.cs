using ETicaretAPI.Domain.Entites.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repository
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> datas);
        Task<bool> RemoveAsync(string id);
        bool RemoveRange(List<T> datas);
        bool Remove(T model);
        bool Update(T model);
        Task<int> SaveAsync();
    }
}
 