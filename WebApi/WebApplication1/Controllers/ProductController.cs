using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DAL;
using WebApplication1.Dtos.Product;
using WebApplication1.Migrations;
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
            ProductReturnDto productReturnDto = new ProductReturnDto()
            {
                Name = product.Name,
                Description = product.Description,
                SalePrice = product.SalePrice,
                CostPrice = product.CostPrice,
                CreationDate = product.CreationDate,
            };
            return StatusCode(200, productReturnDto);
        }
        [HttpGet("products")]
        public IActionResult GetAll()
        {
           
            var query = _appDbContext.Products.Where(p => p.IsDeleted==false || p.IsDeleted == null);
            List<ProductListItemDto> listProducts = new List<ProductListItemDto>();
            foreach (var item in query.ToList())
            {
                ProductListItemDto itemDto = new ProductListItemDto()
                {
                    Name = item.Name,
                    Description = item.Description,
                    SalePrice = item.SalePrice,
                    CostPrice = item.CostPrice,
                    CreationDate = item.CreationDate,
                    ModifiedDate = item.ModifiedDate,
                };
                listProducts.Add(itemDto);
            }
            var proList = new ProductListDto()
            {
                TotalCount = query.Count(),
                Items = listProducts
            };
            return StatusCode(StatusCodes.Status200OK, proList);
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

        [Route("isdelete/{id}")]
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
