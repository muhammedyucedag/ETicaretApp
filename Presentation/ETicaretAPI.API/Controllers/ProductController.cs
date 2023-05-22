using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Repository;
using ETicaretAPI.Application.Repository.ProductImageFile;
using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entites;
using ETicaretAPI.Persistence.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            IStorageService storageService)
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
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalCount = productReadRepository.GetAll(false).Count();
            var products = productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdateDate

            }).ToList();

            return Ok(new
            {
                totalCount,
                products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await productReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            await productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            });
            await productWriteRepository.SaveAsync();
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
        public async Task<IActionResult> Upload()
        {
            var datas = await storageService.UploadAsync("resource/files", Request.Form.Files);

            //var datas = await fileService.UploadAsync("resource/file-images", Request.Form.Files);

            await productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.pathOrContainerName,
                Storage = storageService.StorageName
            }).ToList());
            await productImageFileWriteRepository.SaveAsync();

            //await fileWriteRepository.AddRangeAsync(datas.Select(d => new ETicaretAPI.Domain.Entites.File()
            //{
            //    FileName = d.fileName,
            //    Path = d.path,
            //}).ToList());
            //await fileWriteRepository.SaveAsync();

            return Ok();
        }
    }
}
