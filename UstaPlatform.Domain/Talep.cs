using System;

namespace UstaPlatform.Domain
{
    
    public record Talep
    {
        public int Id { get; init; }
        public int VatandaşId { get; init; }
        public string Aciklama { get; init; } = string.Empty;
        public string UzmanlikAlani { get; init; } = string.Empty;
        public DateTime TalepZamani { get; init; }
        public DateTime? TercihEdilenTarih { get; init; }
        public string Adres { get; init; } = string.Empty;
        public bool AcilMi { get; init; }
        public TalepDurumu Durum { get; init; } = TalepDurumu.Beklemede;

        public override string ToString()
        {
            return $"{Aciklama} - {UzmanlikAlani} ({Durum})";
        }
    }

    public enum TalepDurumu
    {
        Beklemede,
        UstayaAtandi,
        Tamamlandi,
        IptalEdildi
    }
}
