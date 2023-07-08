using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
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


        [Route("product/{id}")]
        [HttpGet]
        public IActionResult Get(int Id)
        {
            var product = _appDbContext.Products.FirstOrDefault(x => x.Id == Id&& !x.IsDeleted);
            return StatusCode(200, product);
        }
        [HttpGet("products")]
        public IActionResult GetAll()
        {
            var products = _appDbContext.Products.Where(p => !p.IsDeleted).ToList();
            return StatusCode(StatusCodes.Status200OK, products);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _appDbContext.Products.Add(product);
            _appDbContext.SaveChanges();
            return StatusCode(200, product);
               
        }
    }
}
