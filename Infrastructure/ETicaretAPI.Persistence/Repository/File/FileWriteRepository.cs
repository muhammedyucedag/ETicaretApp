using ETicaretAPI.Application.Repository;
using ETicaretAPI.Persistence.Contexts;
using entites = ETicaretAPI.Domain.Entites;


namespace ETicaretAPI.Persistence.Repository.File
{
    public class FileWriteRepository : WriteRepository<entites.File>,IFileWriteRepository
    {
        public FileWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }

    }
}
