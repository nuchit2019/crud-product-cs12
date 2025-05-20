using ProductAPI3.Application.Interfaces;
using ProductAPI3.Application.Service;
using ProductAPI3.Infrastructure.Repositories;
using ProductAPI3.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Register Global Exception ก่อนทุก Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>(); 

// จากนั้น Response Wrapper Middleware
app.UseMiddleware<ApiResponseWrapperMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
