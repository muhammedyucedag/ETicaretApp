using ETicaretAPI.Application.Repository;
using ETicaretAPI.Domain.Entites;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repository
{
    public class EndpointWriteRepository : WriteRepository<Endpoint>, IEndpointWriteRepository
    {
        public EndpointWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
