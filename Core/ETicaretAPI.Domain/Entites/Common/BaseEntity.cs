namespace ETicaretAPI.Domain.Entites.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        virtual public DateTime UpdateDate { get; set; } // Ovverride ediliyor. (eziliyor)
    }
}
