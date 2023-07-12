using WebApplication1.Dtos.Category;

namespace WebApplication1.Dtos.Product
{
    public class ProductReturnDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? SalePrice { get; set; }
        public double? CostPrice { get; set; }
        public DateTime? CreationDate { get; set; }
        public categoryInProductReturnDto? Category { get; set; }


        public class categoryInProductReturnDto
        {
            public string? Name { get; set; }
            public string? Description { get; set; }
            public string ImageUrl { get; set; }
            public int ProductCount { get; set; }
        }
    }
}
