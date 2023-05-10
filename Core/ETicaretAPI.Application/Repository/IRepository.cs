using ETicaretAPI.Domain.Entites.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Repository
{
    public interface IRepository<T> where T : BaseEntity // class olma garantisi veriyoruz 
    {
        DbSet<T> Table { get; }
    }
}
