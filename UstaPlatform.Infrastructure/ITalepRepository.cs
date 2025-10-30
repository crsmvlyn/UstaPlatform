using UstaPlatform.Domain;

namespace UstaPlatform.Infrastructure
{
    /// <summary>
    /// Talep repository arayüzü - SOLID DIP prensibi
    /// </summary>
    public interface ITalepRepository
    {
        IEnumerable<Talep> TümünüGetir();
        Talep? IdyeGöreGetir(int id);
        IEnumerable<Talep> VatandaşaGöreGetir(int vatandaşId);
        IEnumerable<Talep> DurumaGöreGetir(TalepDurumu durum);
        Talep Ekle(Talep talep);
        void Güncelle(Talep talep);
        void Sil(int id);
    }
}
