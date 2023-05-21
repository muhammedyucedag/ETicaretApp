using ETicaretAPI.Application.Repository;
using ETicaretAPI.Persistence.Contexts;
using entites = ETicaretAPI.Domain.Entites;

namespace ETicaretAPI.Persistence.Repository
{
    public class ProductImageFileWriteRepository : WriteRepository<entites.ProductImageFile>,IProductImageFileWriteRepository
    {
        public ProductImageFileWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
