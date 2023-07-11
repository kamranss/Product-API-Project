using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.Dtos.Category;
using WebApplication1.Migrations;
using WebApplication1.Models;

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


        [HttpPost("newcategory")]
        public IActionResult Create(CategoryCreateDto category)
        {
            var existCategory = _appDbContext.Categories.FirstOrDefault(c => c.Name == category.Name);
            if (existCategory != null)
            {
                return BadRequest(existCategory.Name);
            }
            Category newCategory = new Category()
            {
                Name = category.Name,
                Description = category.Description,
            };
            newCategory.CreationDate = DateTime.Now;
            _appDbContext.Categories.Add(newCategory);
            _appDbContext.SaveChanges();
            return StatusCode(200, category);

        }


        //[HttpPut("modcategory")]
        //public IActionResult Update(CAtegoryUpdateDto category)
        //{
        //    var existCategory = _appDbContext.Categories.FirstOrDefault(p => p.Id == category.id);
        //    existCategory.Name = category.Name;
        //    existCategory.Description = category.Description;
        //    existCategory.ModifiedDate = DateTime.Now;
        //    _appDbContext.SaveChanges();
        //    return StatusCode(200, category);
        //}

        //[Route("{id}")]
        //[HttpDelete]
        //public IActionResult Delete(int id)
        //{
        //    var existCategory = _appDbContext.Categories.FirstOrDefault(p => p.Id == id);
        //    if (existCategory == null) return NotFound();
        //    _appDbContext.Remove(existCategory);
        //    _appDbContext.SaveChanges();
        //    return NoContent();
        //}

        //[Route("isdelete/{id}")]
        //[HttpPatch]
        //public IActionResult Delete(int id, bool isDelete)
        //{
        //    var existProduct = _appDbContext.Products.FirstOrDefault(p => p.Id == id);
        //    if (existProduct == null) return NotFound();
        //    existProduct.IsDeleted = isDelete;
        //    _appDbContext.SaveChanges();
        //    return NoContent();
        //}
    }
}
