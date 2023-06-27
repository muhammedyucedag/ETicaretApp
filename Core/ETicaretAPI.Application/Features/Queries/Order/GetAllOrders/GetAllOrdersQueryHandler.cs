using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Order.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, List<GetAllOrdersQueryResponse>>
    {
        private readonly IOrderService _orderService;

        public GetAllOrdersQueryHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<List<GetAllOrdersQueryResponse>> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
        {
           var data = await _orderService.GetAllOrdersAsync();
            return data.Select(o => new GetAllOrdersQueryResponse
            {
                CreatedDate = o.CreatedDate,
                OrderCode = o.OrderCode,
                TotalPirce = o.TotalPirce,
                UsernName = o.UsernName
            }).ToList();
        }
    }
}
