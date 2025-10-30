using UstaPlatform.Domain;

namespace UstaPlatform.Infrastructure
{
    /// <summary>
    /// Usta repository arayüzü - SOLID DIP prensibi
    /// </summary>
    public interface IUstaRepository
    {
        IEnumerable<Usta> TümünüGetir();
        Usta? IdyeGöreGetir(int id);
        IEnumerable<Usta> UzmanlikAlaninaGöreGetir(string uzmanlikAlani);
        Usta Ekle(Usta usta);
        void Güncelle(Usta usta);
        void Sil(int id);
    }
}
