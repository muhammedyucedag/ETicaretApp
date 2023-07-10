using ETicaretAPI.Domain.Entites.Common;

namespace ETicaretAPI.Domain.Entites
{
    public class CompletedOrder : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

    }
}
