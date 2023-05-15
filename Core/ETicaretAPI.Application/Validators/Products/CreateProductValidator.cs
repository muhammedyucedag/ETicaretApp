using ETicaretAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator:AbstractValidator<VM_Create_Product>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Ürün adını boş geçmeyiniz.")
                .MaximumLength(150)
                .MinimumLength(5)
                .WithMessage("Ürünü adını 5 ile 150 karakter arasında giriniz.");

            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                .WithMessage("Stok bilgisini boş geçmeyiniz.")
                .Must(s => s >= 0)
                .WithMessage("Stok bilgisi negatif olamaz!");

            RuleFor(p => p.Price)
               .NotEmpty()
               .NotNull()
               .WithMessage("Fiyat bilgisini boş geçmeyiniz.")
               .Must(s => s >= 0)
               .WithMessage("Fiyat bilgisi negatif olamaz!");
        }
    }
}
