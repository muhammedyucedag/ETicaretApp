using ETicaretAPI.Application.Repository.ProductImageFile;
using ETicaretAPI.Persistence.Contexts;
using entites = ETicaretAPI.Domain.Entites;

namespace ETicaretAPI.Persistence.Repository
{
    internal class ProductImageFileReadRepository : ReadRepository<entites.ProductImageFile>, IProductImageFileReadRepository
    {
        public ProductImageFileReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
