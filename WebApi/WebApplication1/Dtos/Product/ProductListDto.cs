namespace WebApplication1.Dtos.Product
{
    public class ProductListDto
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int CurrentPage { get; set; }
        public List<ProductListItemDto> Items { get; set; }
    }
}
