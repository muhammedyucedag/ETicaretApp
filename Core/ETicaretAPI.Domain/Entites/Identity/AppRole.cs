using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI.Domain.Entites.Identity
{
    public class AppRole : IdentityRole<string>
    {
        public ICollection<Endpoint> Endpoints { get; set; }
    }
}
