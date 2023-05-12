using ETicaretAPI.Domain.Entites.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repository
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        // Sorgu üzeringe çalışacaksan IQueryable ama InMemory de çalışacaksan IEnumerable (List<>)
        // where, select
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true); // şartlı yapı için expression kullanırız (lambda)
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetByIdAsync(string id, bool tracking = true);
    }
}
