using static WebApplication1.Dtos.Product.ProductReturnDto;

namespace WebApplication1.Dtos.Product
{
    public class ProductListItemDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public double? SalePrice { get; set; }
        public double? CostPrice { get; set; }
        public categoryInProductReturnDto Category { get; set; }

      
    }
}
