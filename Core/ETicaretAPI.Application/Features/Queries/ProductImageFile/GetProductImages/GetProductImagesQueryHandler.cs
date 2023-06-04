using MediatR;

namespace ETicaretAPI.Application.Features.Queries.ProductImageFile.GetProductImages
{
    internal class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, GetProductImagesQueryResponse>
    {
        public Task<GetProductImagesQueryResponse> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
