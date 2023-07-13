using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DAL;
using WebApplication1.Dtos.Product;
using WebApplication1.Migrations;
using WebApplication1.Models;
using static WebApplication1.Dtos.Product.ProductReturnDto;

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
        public IActionResult Get(int? id)   
        {
            var product = _appDbContext.Products
                .Include(p => p.Category)
                .ThenInclude(c => c.Products)
                .FirstOrDefault(p => p.Id == id&& (p.IsDeleted == false || p.IsDeleted == null));
            if (product==null)
            {
                return NotFound();
            }
            var productReturnDto = new ProductReturnDto()
            {
                Name = product?.Name,
                Description = product?.Description,
                SalePrice = product?.SalePrice,
                CostPrice = product?.CostPrice,
                CreationDate = product?.CreationDate,
                Category = new categoryInProductReturnDto
                {
                    Name = product.Category?.Name,
                    Description = product.Category?.Description,
                    ImageUrl = product.Category?.ImagUrl,
                    ProductCount = product.Category?.Products.Count()??0
                }
            };
            return StatusCode(200, productReturnDto);
        }
        [HttpGet("products")]
        public IActionResult GetAll(string? search, int? take=2)
        {

            int ourTake = take ?? 2;

            var query = _appDbContext.Products
                .Include(p => p.Category)
                .ThenInclude(c=> c.Products)
                .Where(p => p.IsDeleted==false || p.IsDeleted == null);
            if (!string.IsNullOrEmpty(search))
            {
                query.Where(p => p.Name.ToLower().Contains(search.ToLower())).Take(ourTake);
            }
            List<ProductListItemDto> listProducts = new List<ProductListItemDto>();
            foreach (var item in query.ToList())
            {
                ProductListItemDto itemDto = new ProductListItemDto()
                {
                    Name = item.Name,
                    Description = item?.Description,
                    SalePrice = item?.SalePrice,
                    CostPrice = item?.CostPrice,
                    CreationDate = item?.CreationDate,
                    ModifiedDate = item?.ModifiedDate,
                    Category = new categoryInProductReturnDto()
                    {
                        Name = item.Category?.Name,
                        Description = item.Category?.Description,
                        ImageUrl = item.Category?.ImagUrl,
                        ProductCount = item.Category?.Products.Count()?? 0
                    }
                };
                listProducts.Add(itemDto);
            }
            var proList = new ProductListDto()
            {
                TotalCount = query.Count(),
                Items = listProducts
            };

            // another approach for retrieving data from query
            //var proListt = new ProductListDto()
            //{
            //    TotalCount = query.Count(),
            //    Items = query.Select(p => new ProductListItemDto
            //    {
            //        Name = p.Name
            //    }).ToList()
            //};
            return StatusCode(StatusCodes.Status200OK, proList);
        }

        [HttpPost("newproduct")]
        public IActionResult Create(ProductCreateDto product)
        {
            var existCategory = _appDbContext.Categories.Where(c => c.Id == product.CategoryId).FirstOrDefault();
            if (existCategory ==null)
            {
                return Conflict("the procided category not exist in db");
            }
            
            Product newProduct = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                CostPrice = product.CostPrice,
                SalePrice = product.SalePrice,
                CategoryId = product.CategoryId,
                CreationDate = DateTime.Now
            };
            _appDbContext.Products.Add(newProduct);
            _appDbContext.SaveChanges();
            return StatusCode(200, product);
               
        }

       
        [HttpPut("modproduct")]
        public IActionResult Update(ProductUpdateDto product)
        {
            var existProduct = _appDbContext.Products.FirstOrDefault(p => p.Id == product.id);
            existProduct.Name = product.Name;
            existProduct.Description = product.Description;
            existProduct.CostPrice = product.CostPrice;
            existProduct.SalePrice = product.SalePrice;
            existProduct.CategoryId = product.CatecoryId;
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
            if (existProduct == null) return NotFound();
            existProduct.IsDeleted = isDelete; 
            _appDbContext.SaveChanges();
            return NoContent();
        }
    }
}
