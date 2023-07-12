using FluentValidation;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Dtos.Product;

namespace WebApplication1.Dtos.Category
{
    public class CategoryCreateDto
    {

        [StringLength(10)] // it is same as configuration implemented below
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile Image { get; set; }


        public class CategoryCreateDtoValiidator : AbstractValidator<CategoryCreateDto>
        {
            public CategoryCreateDtoValiidator()
            {
                RuleFor(c => c.Name)
                    .NotEmpty().WithMessage("Not leave empty") // same as reguired anotation
                    .MaximumLength(20).WithMessage("Max lenght should be 10");

                RuleFor(c => c.Description)
                    .NotEmpty().WithMessage("Can not be null")
                    .MaximumLength(20).WithMessage("Max lenght should be 20");

            }
        }
    }
}
