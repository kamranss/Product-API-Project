using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.Product
{
    public class ProductUpdateDto
    {
        public int id { get; set; }

        [StringLength(10)]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? SalePrice { get; set; }
        public double? CostPrice { get; set; }
        public DateTime? CreationDate { get; set; }
    }

    public class ProductUpdateDtoValiidator : AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValiidator()
        {
            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("Not leave empty")
               .MaximumLength(20).WithMessage("Max lenght should be 20");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Can not be null")
                .MaximumLength(20).WithMessage("Max lenght should be 20");

            RuleFor(p => p.SalePrice)
                .NotNull().WithMessage("Can not be null")
                .GreaterThanOrEqualTo(0).WithMessage("Should be greater than 0 or equal");

            RuleFor(p => p.CostPrice)
                .NotNull().WithMessage("Can not  be null")
                .GreaterThanOrEqualTo(0).WithMessage("greater then ");

            // Taking all table and implem=nting custom validation
            RuleFor(p => p).Custom((p, context) =>
            {
                if (p.CostPrice > p.SalePrice)
                {
                    context.AddFailure("CostPrice", "CostPrice cannot be greater than SalePrice");
                }
            });
        }
    }
}
