using FirstWeb.API.Data;
using FirstWeb.API.Mappings;
using FirstWeb.API.Repositories;
using FirstWeb.API.Repositories.ADO.Net;
using FirstWeb.API.Repositories.Dapper;
using FirstWeb.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddScoped<IProductRepositoryEFCore,SQLProductRepositoryEFCore>();
builder.Services.AddScoped<IProductRepoitoryDapper, SQLProductRepositoryDapper>();
builder.Services.AddScoped<IProductRepositoryADO, ProductRepositoryADO>();
builder.Services.AddScoped<ICacheService, CacheService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
