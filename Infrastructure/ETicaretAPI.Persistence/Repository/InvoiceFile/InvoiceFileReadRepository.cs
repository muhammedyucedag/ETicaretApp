using ETicaretAPI.Application.Repository;
using ETicaretAPI.Persistence.Contexts;
using entites = ETicaretAPI.Domain.Entites;


namespace ETicaretAPI.Persistence.Repository
{
    public class InvoiceFileReadRepository : ReadRepository<entites.InvoiceFile>, IInvoiceFileReadRepository
    {
        public InvoiceFileReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }       
    }
}
