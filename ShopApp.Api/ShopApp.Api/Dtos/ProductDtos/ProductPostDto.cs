using FluentValidation;

namespace ShopApp.Api.Dtos.ProductDtos
{
    public class ProductPostDto
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
    }
    public class ProductPostDtoValidator : AbstractValidator<ProductPostDto>
    {
        public ProductPostDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().MaximumLength(100).MinimumLength(2);
            RuleFor(x => x.BrandId).GreaterThanOrEqualTo(1);
            RuleFor(x => x.CostPrice).GreaterThanOrEqualTo(0);
            RuleFor(x => x.SalePrice).GreaterThanOrEqualTo(x=>x.CostPrice);
        }
    }
}
