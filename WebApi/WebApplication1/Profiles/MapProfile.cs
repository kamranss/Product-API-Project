using AutoMapper;
using WebApplication1.Dtos.Category;
using WebApplication1.Dtos.Product;
using WebApplication1.Models;
using static WebApplication1.Dtos.Product.ProductReturnDto;

namespace WebApplication1.Profiles
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Category, CategoryReturnDto>() // we can use reverse map to change back the mapping
                .ForMember(mc => mc.ImageUrl, map => map.MapFrom(c => "https://localhost:7268/" + c.ImagUrl))
                .ForMember(ds => ds.ProductsCount, map => map.MapFrom(c => c.Products.Count)); // this approach works when property names are not same


            CreateMap<Category, categoryInProductReturnDto>()
                .ForMember(ds => ds.ProductCount, map => map.MapFrom(c => c.Products.Count)); // we are doing it because of types are different within mapped objects


            CreateMap<Product, ProductReturnDto>()
                .ForMember(ds => ds.Profit, map => map.MapFrom(p => p.SalePrice - p.CostPrice));
                // if two objects having property with same name but types are different the automapper will provide missingType error for client
           
        }
    }
}
