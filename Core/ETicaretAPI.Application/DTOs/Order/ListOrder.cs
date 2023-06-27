namespace ETicaretAPI.Application.DTOs.Order
{
    public class ListOrder
    {
        public string OrderCode { get; set; }
        public string UsernName { get; set; }
        public float TotalPirce { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
