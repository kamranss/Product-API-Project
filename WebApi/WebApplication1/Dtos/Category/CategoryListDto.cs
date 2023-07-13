namespace WebApplication1.Dtos.Category
{
    public class CategoryListDto
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int CurrentPage { get; set; }

        public List<CategoryListitemDto>? Items { get; set; }
    }
}
