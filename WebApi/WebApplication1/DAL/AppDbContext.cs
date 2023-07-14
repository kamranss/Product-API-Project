using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebApplication1.Configuration;
using WebApplication1.Helper.Roles;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product>  Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<AppUser> Users { get; set; }


        protected override  void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration()); // applying Db validation here  -- this is mainly same as data anotation validation
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            base.OnModelCreating(modelBuilder);

          
        }

      




        //private async Task SeedRolesAsync()
        //{
        //    var roleManager = this.GetService<RoleManager<IdentityRole>>();

        //    foreach (var item in Enum.GetValues(typeof(RoleEnum)).Cast<RoleEnum>())
        //    {
        //        if (!await roleManager.RoleExistsAsync(item.ToString()))
        //        {
        //            await roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
        //        }
        //    }
        //}
    }
}
