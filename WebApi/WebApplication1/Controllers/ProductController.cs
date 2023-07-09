using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.Dtos.Product;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    public class ProductController : BaseController
    {
        private readonly AppDbContext _appDbContext;

        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        [Route("productbyid/{id}")]
        [HttpGet]
        public IActionResult Get(int Id)
        {
            var product = _appDbContext.Products.FirstOrDefault(p => p.Id == Id&& p.IsDeleted == false || p.IsDeleted == null);
            if (product==null)
            {
                return NotFound();
            }
            return StatusCode(200, product);
        }
        [HttpGet("products")]
        public IActionResult GetAll()
        {
            var products = _appDbContext.Products.Where(p => p.IsDeleted== false|| p.IsDeleted==null).ToList();
            return StatusCode(StatusCodes.Status200OK, products);
        }

        [HttpPost("newproduct")]
        public IActionResult Create(ProductCreateDto product)
        {
            product.CreationDate = DateTime.Now;
            Product newProduct = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                CostPrice = product.CostPrice,
                SalePrice = product.SalePrice,
            };
            _appDbContext.Products.Add(newProduct);
            _appDbContext.SaveChanges();
            return StatusCode(200, product);
               
        }

        [HttpPut]
        public IActionResult Update(ProductCreateDto product)
        {
            var existProduct = _appDbContext.Products.FirstOrDefault(p => p.Id == product.id);
            existProduct.Name = product.Name;
            existProduct.Description = product.Description;
            existProduct.ModifiedDate = DateTime.Now;
            _appDbContext.SaveChanges();
            return StatusCode(200, product);
        }
        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var existProduct = _appDbContext.Products.FirstOrDefault(p => p.Id ==id);
            if (existProduct != null) return NotFound();
            _appDbContext.Remove(existProduct);
            _appDbContext.SaveChanges();
            return NoContent();
        }

        [Route("{id}")]
        [HttpPatch]
        public IActionResult Delete(int id, bool isDelete)
        {
            var existProduct = _appDbContext.Products.FirstOrDefault(p => p.Id == id);
            if (existProduct != null) return NotFound();
            existProduct.IsDeleted = isDelete; 
            _appDbContext.SaveChanges();
            return NoContent();
        }
    }
}
