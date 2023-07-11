using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;

namespace WebApplication1.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CategoryController : BaseController
    {
        private readonly AppDbContext _appDbContext;

        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [Route("categorybyid/{id}")]
        [HttpGet]
        public IActionResult GetCategory(int? id)
        {
           var Category = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (Category == null) { return NotFound(); }
            return Ok(Category);
        }

        [Route("allcategories/{id}")]
        [HttpGet]
        public IActionResult GetCategories()
        {
            var Categories = _appDbContext.Categories.ToList();
            return Ok(Categories);
        }

    }
}
