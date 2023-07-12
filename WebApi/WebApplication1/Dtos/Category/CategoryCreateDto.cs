using FluentValidation;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Dtos.Product;

namespace WebApplication1.Dtos.Category
{
    public class CategoryCreateDto
    {

        [StringLength(10)]
        public string? Name { get; set; }
        public string? Description { get; set; }


        public class CategoryCreateDtoValiidator : AbstractValidator<ProductUpdateDto>
        {
            public CategoryCreateDtoValiidator()
            {
                RuleFor(c => c.Name)
                    .NotEmpty().WithMessage("Not leave empty")
                    .MaximumLength(20).WithMessage("Max lenght should be 20");

                RuleFor(c => c.Description)
                    .NotEmpty().WithMessage("Can not be null")
                    .MaximumLength(20).WithMessage("Max lenght should be 20");

            }
        }
    }
}
