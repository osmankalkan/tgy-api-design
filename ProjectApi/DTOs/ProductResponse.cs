namespace ProjectApi.DTOs
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        
    }
}

// ============================================================
// DTO MANTIĞI
// ============================================================
// Model (Product) = veritabanındaki gerçek veri.
// DTO'lar ise API'nin dış dünyayla konuşurken kullandığı,
// senaryoya özel veri şekilleridir. Yön'e göre ikiye ayrılır:
//
// 1) REQUEST DTO'ları (Kullanıcı -> API)
//    - CreateProductRequest, UpdateProductRequest
//    - Kullanıcıdan API'ye giren veriyi temsil eder
//    - Bu yüzden Data Annotations ([Required], [StringLength],
//      [Range]) BURADA kullanılır -> amaç, API'ye girmeden önce
//      veriyi süzmek, geçersiz veriyi reddetmek
//    - CreatedAt, IsActive, Id gibi alanlar burada YOK çünkü
//      bunları kullanıcı değil, sunucu kendisi üretir
//
// 2) RESPONSE DTO'su (API -> Kullanıcı)
//    - ProductResponse
//    - API'den kullanıcıya giden veriyi temsil eder
//    - Data Annotations YOK çünkü veri zaten veritabanından
//      gelen, güvenilir/doğrulanmış veridir; tekrar kontrol
//      etmeye gerek yok
//    - Sadece kullanıcıya gösterilmek istenen alanlar seçilir
//      (Id dahil edilebilir ama CreatedAt/IsActive gösterilmeyebilir)
//
// ÖZET KURAL:
// Request  -> içeri giren, doğrulanması gereken veri
// Response -> dışarı çıkan, zaten güvenilir olan veri
// ============================================================