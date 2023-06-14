using ETicaretAPI.Application.Repository;
using ETicaretAPI.Application.Repository.ProductImageFile;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Application.Features.Commands.ProductImageFile.ChangeShowcaseImage
{
    public class ChangeShowcaseImageCommandHandler : IRequestHandler<ChangeShowcaseImageCommandRequest, ChangeShowcaseImageCommandResponse>
    {
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public ChangeShowcaseImageCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<ChangeShowcaseImageCommandResponse> Handle(ChangeShowcaseImageCommandRequest request, CancellationToken cancellationToken)
        {
            var query = _productImageFileWriteRepository.Table
                .Include(product => product.Products)
                .SelectMany(product => product.Products, (productimagefile, product) => new
                {
                    productimagefile,
                    product
                });

            var data = await query.FirstOrDefaultAsync(product => product.product.Id == Guid.Parse(request.ProductId) && product.productimagefile.Showcase );

            if (data != null)
                data.productimagefile.Showcase = false;
            

            var image = await query.FirstOrDefaultAsync(product => product.productimagefile.Id == Guid.Parse(request.ImageId));

            if (image != null)
            image.productimagefile.Showcase = true;

            await _productImageFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
