using Microsoft.EntityFrameworkCore;
using WebApplication1.Configuration;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product>  Products { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration()); // applying Db validation here  -- this is mainly same as data anotation validation
            base.OnModelCreating(modelBuilder);
        }
    }
}
