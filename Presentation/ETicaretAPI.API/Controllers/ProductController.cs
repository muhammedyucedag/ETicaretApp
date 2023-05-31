using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Features.Commands.CreateProduct;
using ETicaretAPI.Application.Features.Queries.GetAllProduct;
using ETicaretAPI.Application.Repository;
using ETicaretAPI.Application.Repository.ProductImageFile;
using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entites;
using ETicaretAPI.Persistence.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly private IProductWriteRepository productWriteRepository;
        readonly private IProductReadRepository productReadRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        readonly IFileWriteRepository fileWriteRepository;
        readonly IFileReadRepository fileReadRepository;
        readonly IProductImageFileReadRepository productImageFileReadRepository;
        readonly IProductImageFileWriteRepository productImageFileWriteRepository;
        readonly IInvoiceFileReadRepository invoiceFileReadRepository;
        readonly IInvoiceFileWriteRepository invoiceFileWriteRepository;
        readonly IStorageService storageService;
        readonly IConfiguration configuration;

        readonly IMediator mediator;

        public ProductController(
            IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository,
            IWebHostEnvironment webHostEnvironment,
            IFileWriteRepository fileWriteRepository,
            IFileReadRepository fileReadRepository,
            IProductImageFileReadRepository productImageFileReadRepository,
            IProductImageFileWriteRepository productImageFileWriteRepository,
            IInvoiceFileReadRepository invoiceFileReadRepository,
            IInvoiceFileWriteRepository invoiceFileWriteRepository,
            IStorageService storageService,
            IMediator mediator,
            IConfiguration configuration)
        {
            this.productWriteRepository = productWriteRepository;
            this.productReadRepository = productReadRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.fileWriteRepository = fileWriteRepository;
            this.fileReadRepository = fileReadRepository;
            this.productImageFileReadRepository = productImageFileReadRepository;
            this.productImageFileWriteRepository = productImageFileWriteRepository;
            this.invoiceFileReadRepository = invoiceFileReadRepository;
            this.invoiceFileWriteRepository = invoiceFileWriteRepository;
            this.storageService = storageService;
            this.mediator = mediator;
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse response = await mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await productReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse response = await mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            Product product = await productReadRepository.GetByIdAsync(model.Id);
            product.Stock = model.Stock;
            product.Name = model.Name;
            product.Price = model.Price;
            await productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await productWriteRepository.RemoveAsync(id);
            await productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(string id)
        {
           List<(string fileName, string pathOrContainerName)> result =  await storageService.UploadAsync("photo-images", Request.Form.Files);

            Product product = await productReadRepository.GetByIdAsync(id);

            await productImageFileWriteRepository.AddRangeAsync(result.Select(productImage => new ProductImageFile
            {
                FileName = productImage.fileName,
                Path = productImage.pathOrContainerName,
                Storage = storageService.StorageName,
                Products = new List<Product>() { product }

            }).ToList());

            await productImageFileWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages(string id)
        {
            Product? product = await  productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

            return Ok(product.ProductImageFiles.Select(p => new
            {
                Path = $"{configuration["BaseStorageUrl"]}/{p.Path}",
                p.FileName,
                p.Id
            }));
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage(string id, string ImageId)
        {
            Product? product = await productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

            ProductImageFile productImageFile = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(ImageId));

            product.ProductImageFiles.Remove(productImageFile);
            await productWriteRepository.SaveAsync();

            return Ok();
        }
    }
}
