using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI.Domain.Entites.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string NameSurname { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public ICollection<Basket> Basket { get; set; }
    }
}
