namespace WebApplication1.Dtos.Product
{
    public class ProductCreateDto
    {
        public int id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? SalePrice { get; set; }
        public double? CostPrice { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
