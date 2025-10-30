using System;

namespace UstaPlatform.Domain
{
    /// <summary>
    /// Onaylanmış, ustaya atanmış ve planlanmış iş
    /// </summary>
    public record İşEmri
    {
        public int Id { get; init; }
        public int TalepId { get; init; }
        public int UstaId { get; init; }
        public decimal Fiyat { get; init; }
        public DateTime PlanlananTarih { get; init; }
        public TimeSpan TahminiSure { get; init; }
        public string Adres { get; init; } = string.Empty;
        public İşEmriDurumu Durum { get; init; } = İşEmriDurumu.Planlandi;
        public DateTime OlusturmaZamani { get; init; }

        public override string ToString()
        {
            return $"İş Emri #{Id} - {Fiyat:C} - {PlanlananTarih:dd.MM.yyyy HH:mm} ({Durum})";
        }
    }

    public enum İşEmriDurumu
    {
        Planlandi,
        DevamEdiyor,
        Tamamlandi,
        IptalEdildi
    }
}
