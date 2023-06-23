using ETicaretAPI.Domain.Entites.Common;

namespace ETicaretAPI.Domain.Entites
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        //public ICollection<Order> Orders;
    }
}
