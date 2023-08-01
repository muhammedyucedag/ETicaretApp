using ETicaretAPI.Domain.Entites.Common;
using ETicaretAPI.Domain.Entites.Identity;

namespace ETicaretAPI.Domain.Entites
{
    public class Endpoint : BaseEntity
    {
        public string ActionType { get; set; }
        public string HttpType { get; set; }
        public string Definition { get; set; }
        public string Code { get; set; }
        public Menu Menu { get; set; }
        public ICollection<AppRole> Roles { get; set; }
    }
}
