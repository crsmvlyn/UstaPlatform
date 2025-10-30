using UstaPlatform.Domain;

namespace UstaPlatform.Infrastructure
{
    /// <summary>
    /// İş emri repository arayüzü - SOLID DIP prensibi
    /// </summary>
    public interface IİşEmriRepository
    {
        IEnumerable<İşEmri> TümünüGetir();
        İşEmri? IdyeGöreGetir(int id);
        IEnumerable<İşEmri> UstayaGöreGetir(int ustaId);
        IEnumerable<İşEmri> TariheGöreGetir(DateTime tarih);
        İşEmri Ekle(İşEmri işEmri);
        void Güncelle(İşEmri işEmri);
        void Sil(int id);
    }
}
