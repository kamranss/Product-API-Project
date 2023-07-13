namespace WebApplication1.Dtos.Category
{
    public class CategoryReturnDto
    {
        //public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int? ProductsCount { get; set; } // if we define naming of properties good mapper will map


    }
}
