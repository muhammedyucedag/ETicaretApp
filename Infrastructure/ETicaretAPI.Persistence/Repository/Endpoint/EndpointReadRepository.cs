using ETicaretAPI.Application.Repository;
using ETicaretAPI.Domain.Entites;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repository
{
    public class EndpointReadRepository : ReadRepository<Endpoint>, IEndpointReadRepository
    {
        public EndpointReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
