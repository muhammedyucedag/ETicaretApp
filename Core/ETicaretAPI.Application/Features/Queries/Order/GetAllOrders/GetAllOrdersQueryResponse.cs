namespace ETicaretAPI.Application.Features.Queries.Order.GetAllOrders
{
    public class GetAllOrdersQueryResponse
    {
        public string OrderCode { get; set; }
        public string UsernName { get; set; }
        public float TotalPirce { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
