using System.ComponentModel.DataAnnotations;

namespace ProjectApi.DTOs
{
    public class CreateProductRequest
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
// NEDEN "Id" SADECE Product ve ProductResponse'ta VAR,
// CreateProductRequest'te YOK?
// ============================================================
// Yeni bir ürün eklenirken, o ürünün henüz bir kimliği (Id)
// olamaz -> çünkü Id, kayıt veritabanına eklenirken OTOMATİK
// olarak üretilir (auto-increment).
//
// Eğer CreateProductRequest içine Id eklenirse, kullanıcı
// kendi seçtiği bir Id gönderebilir. Bu durum:
// - var olan bir kaydı yanlışlıkla etkileyebilir
// - sistemin kontrolünü kullanıcıya bırakır (güvenlik riski)
//
// DOĞRU MANTIK:
// Id, kayıt OLUŞTUKTAN SONRA ortaya çıkan bir bilgidir.
// Bu yüzden sadece "var olan kaydı temsil eden" yapılarda
// (Product modeli, ProductResponse) bulunur; "henüz var
// olmayan kaydı oluşturma isteği"nde (CreateProductRequest)
// asla yer almaz.
// ============================================================