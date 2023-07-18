using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Api.Dtos.BrandDtos
{
    public class BrandPostDto
    {
        [Required]
        [MaxLength(35)]
        public string Name { get; set; }
    }
    public class BrandPostDtoValidator : AbstractValidator<BrandPostDto>
    {
        public BrandPostDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().MinimumLength(2).MaximumLength(35);
        }
    }
}
