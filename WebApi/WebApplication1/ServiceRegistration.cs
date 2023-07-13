using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using WebApplication1.DAL;
using WebApplication1.Dtos.Product;
using WebApplication1.Models;

namespace WebApplication1
{
    public static class ServiceRegistration
    {
        public static void ServiceRegister(this IServiceCollection services)
        {
            //setting up validation if we include one there is no need to include other validation classes
            // this comes with fluentApi package
            services.AddControllers().AddFluentValidation(option =>
            {
                option.RegisterValidatorsFromAssemblyContaining<ProductCreateDtoValiidator>();
                option.RegisterValidatorsFromAssemblyContaining<ProductUpdateDtoValiidator>();
            });


            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.SignIn.RequireConfirmedEmail = true;

                options.User.RequireUniqueEmail = true;
                options.Lockout.AllowedForNewUsers = true;

                // this is mainly controlling user attemps and locking for some period if something goes wrong -- mainly used together
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            }).AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders(); // this basicly serves for token generation
            //.AddErrorDescriber<CustomidentityErrorDescriber>(); // this is serving for get error descriptions which we indicated within helper 


        }
    }
}
