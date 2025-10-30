using System;

namespace UstaPlatform.Domain
{
    
    public record Usta
    {
        public int Id { get; init; }
        public string Ad { get; init; } = string.Empty;
        public string Soyad { get; init; } = string.Empty;
        public string UzmanlikAlani { get; init; } = string.Empty;
        public int Puan { get; init; }
        public int Yogunluk { get; init; }
        public DateTime KayitZamani { get; init; }
        public string Telefon { get; init; } = string.Empty;
        public string Adres { get; init; } = string.Empty;

        public override string ToString()
        {
            return $"{Ad} {Soyad} - {UzmanlikAlani} (Puan: {Puan}, Yoğunluk: {Yogunluk})";
        }
    }
}
