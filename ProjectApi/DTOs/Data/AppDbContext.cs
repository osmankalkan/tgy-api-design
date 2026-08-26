using Microsoft.EntityFrameworkCore;
using ProjectApi.Models;
namespace ProjectApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
/*
Product (Model)          -> "Ürün" kavramının veri şekli
        ↓
AppDbContext              -> Product'ı veritabanına bağlayan köprü
        ↓
DbSet<Product> Products   -> Products tablosuna erişim noktası
        ↓
Program.cs'de AddDbContext -> hangi veritabanına bağlanılacağını söyler
        ↓
Controller                -> AppDbContext'i kullanarak CRUD işlemleri yapar
*/
