using ETicaretAPI.Application.Repository;
using ETicaretAPI.Domain.Entites.Common;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repository
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly ETicaretAPIDbContext context;

        public ReadRepository(ETicaretAPIDbContext context)
        {
            this.context = context;
        }

        public DbSet<T> Table => context.Set<T>();

        public IQueryable<T> GetAll(bool tarcking = true)
        {
            var query = Table.AsQueryable();
            if (!tarcking) // tracking istenmiyorsa
            {
               query = query.AsNoTracking(); // AsNoTracking ile tracking kesilir
            }
            return query;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tarcking = true)
        {
            var query = Table.Where(method);
            if (!tarcking)
            {
                query = query.AsNoTracking();
            }
            return query;
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tarcking = true)
        {

            var query = Table.AsQueryable();
            if (!tarcking)
            {
                query = Table.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetByIdAsync(string id, bool tarcking = true)
        //=> await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        //=> await Table.FindAsync(Guid.Parse(id));
        {
            // AsQueryble ile çalışıyorsak find metodu yoktur. O yüzden marker interface kullanacağız
            var query = Table.AsQueryable();
            if (!tarcking)
            {
                query = Table.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
        }





    }
}
