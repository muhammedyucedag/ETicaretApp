using ETicaretAPI.Application.Repository;
using ETicaretAPI.Domain.Entites;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repository
{
    public class CompletedOrderWriteRepository : WriteRepository<CompletedOrder>, ICompletedOrderWriteRepository
    {
        public CompletedOrderWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
