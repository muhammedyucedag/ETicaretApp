namespace ETicaretAPI.Application.DTOs.Order
{
    public class CompletedOrderDto
    {
        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserName { get; set; }
        public string To { get; set; }
    }
}
