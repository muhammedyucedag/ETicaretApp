using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Repository;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        readonly IStorageService _storageService;
        readonly IProductReadRepository _productReadRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public UploadProductImageCommandHandler(IStorageService storageService, IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _storageService = storageService;
            _productReadRepository = productReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-images", request.Files);

            Domain.Entites.Product product = await _productReadRepository.GetByIdAsync(request.Id);

            await _productImageFileWriteRepository.AddRangeAsync(result.Select(productImage => new Domain.Entites.ProductImageFile
            {
                FileName = productImage.fileName,
                Path = productImage.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Domain.Entites.Product>() { product }
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();
            return new();
        }
    }
}
