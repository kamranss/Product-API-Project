using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.Dtos.Category;
using WebApplication1.Helper.FileExten;
using WebApplication1.Migrations;
using WebApplication1.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class CategoryController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _appDbContext;

        public CategoryController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("categorybyid/{id}")]
        [HttpGet]
        public IActionResult GetCategory(int? id)
        {
           var category = _appDbContext.Categories
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Id == id);
       
            if (category == null) { return NotFound(); }

            CategoryReturnDto categoryreturnDto = new CategoryReturnDto()
            {
                Name = category.Name,
                Description = category.Description,
                ProductCount = category?.Products.Count() ?? 0

            };
            return Ok(categoryreturnDto);
        }

        [Route("allcategories")]
        [HttpGet]
        public IActionResult GetCategories()
        {
            var Categories = _appDbContext.Categories.ToList();
            return Ok(Categories);
        }

        [Route("newcategory")]
        [HttpPost]
        public IActionResult Create([FromForm]CategoryCreateDto category)
        {
            var existCategory = _appDbContext.Categories.FirstOrDefault(c => c.Name.ToLower() == category.Name.ToLower());

            if (!category.Image.CheckFileType()) return Conflict("File Type is not correct");

            if (category.Image.CheckFileLenght(1000)) return Conflict("File Size is bigger");

            if (existCategory != null)
            {
                return Conflict($"Category with the same {existCategory.Name} name already exist");
            }
            Category newCategory = new Category()
            {
                Name = category.Name,
                Description = category.Description,
                CreationDate = DateTime.Now
            };
            //_appDbContext.Categories.Add(new Category { Name = category.Name, Description = category.Description}) ; // another approach

            newCategory.ImagUrl = category.Image.SaveFile(_webHostEnvironment, "images/category");
       
           
            _appDbContext.Categories.Add(newCategory);
            _appDbContext.SaveChanges();
            return StatusCode(201, category);

        } // the params within method comes from body


        [Route("delete/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var existCategory = _appDbContext.Categories.FirstOrDefault(p => p.Id == id);
            if (existCategory == null) return NotFound();
            _appDbContext.Remove(existCategory);
            _appDbContext.SaveChanges();
            return NoContent();
        }


        [Route("modcategory/{id}")]
        [HttpPut]
        public IActionResult Update(int id, CategoryUpdateDto category)
        {
            var existCategory = _appDbContext.Categories.FirstOrDefault(p => p.Id == id);
            if (existCategory == null) { return BadRequest(); };

            if (existCategory.Name == category.Name)
            {
               return Conflict($"Category with the same  name {existCategory.Name} already exist");
            }
            if (_appDbContext.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower() && c.Id== existCategory.Id))
            {
                return BadRequest();
            }
            existCategory.Name = category.Name;
            existCategory.Description = category.Description;
            existCategory.ModifiedDate = DateTime.Now;
            _appDbContext.SaveChanges();
            return StatusCode(200, category);
        }


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
