using ETicaretAPI.Application.Repository;
using ETicaretAPI.SignalR;
using MediatR;


namespace ETicaretAPI.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IProductWriteRepository productWriteRepository;
        readonly IProductHubService productHubService;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductHubService productHubService)
        {
            this.productWriteRepository = productWriteRepository;
            this.productHubService = productHubService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            });
            await productWriteRepository.SaveAsync();
            await productHubService.ProductAddedMessageAsync($"{request.Name} isminde ürün eklenmiştir");
            return new();
        }
    }
}
