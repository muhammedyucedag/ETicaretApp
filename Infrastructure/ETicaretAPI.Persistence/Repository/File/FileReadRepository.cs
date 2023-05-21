using ETicaretAPI.Application.Repository;
using ETicaretAPI.Persistence.Contexts;
using System.Linq.Expressions;
using entites = ETicaretAPI.Domain.Entites;

namespace ETicaretAPI.Persistence.Repository
{
    public class FileReadRepository : ReadRepository<entites.File>, IFileReadRepository
    {
        public FileReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
