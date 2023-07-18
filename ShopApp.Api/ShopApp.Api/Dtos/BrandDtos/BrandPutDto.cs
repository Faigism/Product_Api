using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace ShopApp.Api.Dtos.BrandDtos
{
    public class BrandPutDto
    {
        [Required]
        [MaxLength(35)]
        public string Name { get; set; }
    }
    public class BrandPutDtoValidator : AbstractValidator<BrandPutDto>
    {
        public BrandPutDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().MinimumLength(2).MaximumLength(35);
        }
    }
}
