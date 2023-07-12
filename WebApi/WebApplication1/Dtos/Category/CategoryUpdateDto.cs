using FluentValidation;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Dtos.Product;

namespace WebApplication1.Dtos.Category
{
    public class CategoryUpdateDto
    {
        //public int Id { get; set; }
        [StringLength(10)]
        public string? Name { get; set; }

        [StringLength(10)]
        public string? Description { get; set; }

        public class CategoryUpdateDtoValiidator : AbstractValidator<ProductUpdateDto>
        {
            public CategoryUpdateDtoValiidator()
            {
                RuleFor(p => p.Name)
                   .NotEmpty().WithMessage("Not leave empty")
                   .MaximumLength(20).WithMessage("Max lenght should be 20");

                RuleFor(p => p.Description)
                    .NotEmpty().WithMessage("Can not be null")
                    .MaximumLength(20).WithMessage("Max lenght should be 20");

            }
        }
    }
}
