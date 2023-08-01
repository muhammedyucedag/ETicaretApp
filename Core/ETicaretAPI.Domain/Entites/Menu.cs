using ETicaretAPI.Domain.Entites.Common;

namespace ETicaretAPI.Domain.Entites
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Endpoint> Endpoints { get; set; }
    }
}
