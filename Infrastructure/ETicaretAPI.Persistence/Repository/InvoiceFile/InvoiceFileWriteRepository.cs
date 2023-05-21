using ETicaretAPI.Application.Repository;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using entites = ETicaretAPI.Domain.Entites;


namespace ETicaretAPI.Persistence.Repository
{
    public class InvoiceFileWriteRepository : WriteRepository<entites.InvoiceFile>, IInvoiceFileWriteRepository
    {
        public InvoiceFileWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
