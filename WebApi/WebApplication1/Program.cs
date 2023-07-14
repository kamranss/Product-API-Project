
using Microsoft.EntityFrameworkCore;
using WebApplication1;
using WebApplication1.DAL;

using WebApplication1.Profiles;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.


builder.Services.ServiceRegister();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(config.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(option =>
{
    option.AddProfile<MapProfile>();
}); // Config for Automapper package

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
