using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos.Category
{
    public class CAtegoryUpdateDto
    {
        [StringLength(10)]
        public string? Name { get; set; }

        [StringLength(10)]
        public string? Description { get; set; }
    }
}
