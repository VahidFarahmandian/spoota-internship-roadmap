using FirstWeb.API.Data;
using FirstWeb.API.Mappings;
using FirstWeb.API.Repositories;
using FirstWeb.API.Repositories.ADO.Net;
using FirstWeb.API.Repositories.Dapper;
using FirstWeb.API.Services;
using FirstWeb.API.Services.In_Memory_Caching;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// output caching
builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder => builder.Expire(TimeSpan.FromMinutes(1)));
    options.AddBasePolicy(builder =>
    {
        builder
          .With(r => r.HttpContext.Request.Path.StartsWithSegments("/api/product/all"))
          .Tag("tag-product")
          .Expire(TimeSpan.FromMinutes(2));
    });
    options.AddPolicy("ExpireIn30s", builder => builder.Expire(TimeSpan.FromSeconds(30)));
    options.AddPolicy("NoCache", builder => builder.NoCache());
    options.AddPolicy("evict", builder => builder.Expire(TimeSpan.FromSeconds(90)));
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddScoped<IProductRepositoryEFCore,SQLProductRepositoryEFCore>();
builder.Services.AddScoped<IProductRepoitoryDapper, SQLProductRepositoryDapper>();
builder.Services.AddScoped<IProductRepositoryADO, ProductRepositoryADO>();
builder.Services.AddScoped<ICacheServiceDistributed, CacheServiceDistributed>();
builder.Services.AddScoped<ICacheServiceInMemory, CacheServiceInMemory>();

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

// output cache
app.UseOutputCache();

app.MapControllers();

app.Run();
