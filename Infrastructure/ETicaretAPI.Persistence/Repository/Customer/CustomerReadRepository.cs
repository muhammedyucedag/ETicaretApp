using ETicaretAPI.Application.Repository;
using ETicaretAPI.Domain.Entites;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repository
{
    public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository // tüm metotları uygulamamak için bu yapılandırmayı tercih ettik 
    {
        public CustomerReadRepository(ETicaretAPIDbContext context) : base(context)
        {

        }
    }
}
