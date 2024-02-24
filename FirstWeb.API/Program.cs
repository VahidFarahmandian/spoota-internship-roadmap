using FirstWeb.API.Data;
using FirstWeb.API.Mappings;
using FirstWeb.API.Repositories;
using FirstWeb.API.Repositories.Account;
using FirstWeb.API.Repositories.ADO.Net;
using FirstWeb.API.Repositories.Dapper;
using FirstWeb.API.Services;
using FirstWeb.API.Services.In_Memory_Caching;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Rate limiting fixed window
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("FixedWindowPolicy", opt =>
    {
        opt.Window = TimeSpan.FromSeconds(10);
        opt.PermitLimit = 2;
        opt.QueueLimit = 4;
        opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
    }).RejectionStatusCode = 429; // Too many request
});

// Rate limiter sliding window
builder.Services.AddRateLimiter(option =>
{
    option.AddSlidingWindowLimiter("SlidingWindowPolicy", opt =>
    {
        opt.Window = TimeSpan.FromSeconds(10);
        opt.PermitLimit = 4;
        opt.QueueLimit = 3;
        opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
        opt.SegmentsPerWindow = 3;
    }).RejectionStatusCode = 429;
});

// Rate limiter concurrency
builder.Services.AddRateLimiter(option =>
{
    option.AddConcurrencyLimiter("ConcurrencyPolicy", opt =>
    {
        opt.PermitLimit = 1;
        opt.QueueLimit = 10;
        opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
    }).RejectionStatusCode = 429;
});

// Rate limiter token buket
builder.Services.AddRateLimiter(options =>
{
    options.AddTokenBucketLimiter("TokenBucketPolicy", opt =>
    {
        opt.TokenLimit = 4;
        opt.QueueLimit = 2;
        opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
        opt.ReplenishmentPeriod = TimeSpan.FromSeconds(10);
        opt.TokensPerPeriod = 4;
        opt.AutoReplenishment = true;
    }).RejectionStatusCode = 429;
});

// Response caching
builder.Services.AddResponseCaching();

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

// Response compression
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

// configure response compression service
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.SmallestSize;
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
throw new InvalidOperationException("Connection string is not found")));

// Add identity JWT authentication
//Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddRoles<IdentityRole>();

// JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

//Add authentication to swagger UI
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddScoped<IProductRepositoryEFCore, SQLProductRepositoryEFCore>();
builder.Services.AddScoped<IProductRepoitoryDapper, SQLProductRepositoryDapper>();
builder.Services.AddScoped<IProductRepositoryADO, ProductRepositoryADO>();
builder.Services.AddScoped<ICacheServiceDistributed, CacheServiceDistributed>();
builder.Services.AddScoped<ICacheServiceInMemory, CacheServiceInMemory>();
builder.Services.AddScoped<IUserAccount, UserAccount>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

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

// Raet limit
app.UseRateLimiter();

// output cache
app.UseOutputCache();

// Response cache
app.UseResponseCaching();

// Response compression
app.UseResponseCompression();

app.MapControllers();

app.UseCors("AllowAll");

app.Run();
