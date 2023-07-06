using ETicaretAPI.Domain.Entites.Common;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Application.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
