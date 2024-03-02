using testingweb.Middleware;
using testingweb.Models;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddSingleton<List<Product>>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.Map("/SAMPLE", task1);
app.UseRequestProduct();
app.UseRouting();

app.MapControllers();

app.Run();

static void task1(IApplicationBuilder app)
{
    app.Run(async context => await context.Response.WriteAsync("Hello uu"));   
}