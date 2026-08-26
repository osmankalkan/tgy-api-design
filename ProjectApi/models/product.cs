namespace ProjectApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;

        public int StockQuantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;


    }
}


// ============================================================
// - Bu class, veritabanındaki "Products" tablosunun kod karşılığı.
//   Her property = bir sütun.
// - Id: benzersiz kimlik (Primary Key). EF Core otomatik tanır.
// - Price: para değeri -> decimal (double/float yuvarlama hatası yapar).
// - Name: varsayılan değer (string.Empty) -> null hatasına karşı önlem.
// - CreatedAt: UtcNow -> saat dilimi farkı sorun yaratmasın diye.
// - IsActive: soft-delete mantığı -> veri silinmez, sadece pasif yapılır.
// ============================================================