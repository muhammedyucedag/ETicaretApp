using ETicaretAPI.Application.Repository;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        IProductReadRepository _productReadRepository;
        IProductWriteRepository _productWriteRepository;

        public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entites.Product product = await _productReadRepository.GetByIdAsync(request.Id);
            product.Stock = request.Stock;
            product.Name = request.Name;
            product.Price = request.Price;
            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
