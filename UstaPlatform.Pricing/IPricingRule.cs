using UstaPlatform.Domain;

namespace UstaPlatform.Pricing
{
    
    public interface IPricingRule
    {
        
        string KuralAdi { get; }

        
        string Aciklama { get; }

        
        /// <param name="temelFiyat">Temel fiyat</param>
        /// <param name="talep">Talep bilgileri</param>
        /// <param name="işEmri">İş emri bilgileri</param>
        /// <returns>Hesaplanan fiyat</returns>
        decimal FiyatHesapla(decimal temelFiyat, Talep talep, İşEmri işEmri);

        
        /// <param name="talep">Talep bilgileri</param>
        /// <param name="işEmri">İş emri bilgileri</param>
        /// <returns>Kural uygulanacaksa true</returns>
        bool UygulanabilirMi(Talep talep, İşEmri işEmri);
    }
}
