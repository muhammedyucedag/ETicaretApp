using ETicaretAPI.Domain.Entites.Common;
using ETicaretAPI.Domain.Entites.Identity;

namespace ETicaretAPI.Domain.Entites
{
    public class Basket : BaseEntity
    {
        public string UserId { get; set; }

        public AppUser User { get; set; }
        public Order Order { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
