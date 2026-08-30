using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design; 
using ProjectApi.Data;
using ProjectApi.MiddleWare;
using ProjectApi.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

IServiceCollection serviceCollection = builder.Services.AddScoped<IProductServices, ProductServices>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Project API",
        Version = "v1",
        Description = "Ürün yönetimi için kullanılan bir API",
    });
});



var app = builder.Build();
app.UseGlobalExceptionHandler(); // GlobalExceptionMiddleWare'i pipeline'a ekliyoruz


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Project API v1");
        options.RoutePrefix = string.Empty; // Swagger UI'yi kök dizine yönlendir
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
