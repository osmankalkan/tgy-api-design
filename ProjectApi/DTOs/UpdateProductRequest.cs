using System.ComponentModel.DataAnnotations;

namespace ProjectApi.DTOs
{
    public class UpdateProductRequest
    {
        [Required(ErrorMessage = "Ürün Adı Boş Olamaz")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Ürün Adı 2-100 Karakter arasında olmalıdır")]
    
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Açıklama 500 Karakterden Uzun Olamaz")]
        public string Description { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stok Miktarı 0'dan büyük olmalıdır")]
        public int StockQuantity { get; set; }
    }
}

// ============================================================
// NEDEN DOĞRUDAN "Product" MODELİNİ KULLANMIYORUZ?
// ============================================================
// Diyelim ki DTO kullanmadık, Controller'da direkt Product
// nesnesini kullanıcıdan aldık. O zaman kullanıcı isterse
// JSON içine şunu da ekleyebilir:
//
//   { "name": "Kalem", "price": 10, "isActive": true, "id": 999 }
//
// Kullanıcı normalde göndermemesi gereken "id" veya "isActive"
// gibi alanları da gönderip sistemi kandırabilir. Buna
// "overposting" (fazladan/izinsiz veri gönderme) denir.
//
// DTO kullanınca bu mümkün değil, çünkü CreateProductRequest
// sınıfında zaten sadece Name, Description, Price, StockQuantity
// alanları TANIMLI. Kullanıcı başka bir şey gönderse bile,
// o alan DTO'da olmadığı için otomatik olarak YOK SAYILIR.
//
// ÖZET MANTIK:
// DTO, sadece "olması gereken" alanları içerdiği için
// bir nevi FİLTRE görevi görür -> kullanıcı sadece izin
// verilen alanları değiştirebilir, fazlasını değil.
// ============================================================