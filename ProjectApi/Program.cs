using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design; 
using ProjectApi.Data;
using ProjectApi.MiddleWare;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();
app.UseGlobalExceptionHandler(); // GlobalExceptionMiddleWare'i pipeline'a ekliyoruz


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
