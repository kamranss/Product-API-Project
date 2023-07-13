namespace WebApplication1.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedbyId { get; set; }
        public int? ModifiedById { get; set; }
        public string? ImagUrl { get; set; }

        public List<Product> Products { get; set; }
    }
}
