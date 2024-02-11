
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetProject.Data;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
}
);
builder.Services.AddScoped<DapperRepository>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new DapperRepository(connectionString);
});

// Add Redis caching
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost"; // Replace with your Redis server address
    options.InstanceName = "YourInstanceName"; // Optional: Add an instance name if needed
});


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();

// UseRouting should be before UseCors
app.UseRouting();
app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
