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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration()); // applying Db validation here
            base.OnModelCreating(modelBuilder);
        }
    }
}
