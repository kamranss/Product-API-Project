namespace WebApplication1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }
        public double SalePrice { get; set; }
        public double CostPrice { get; set; }
    }
}
