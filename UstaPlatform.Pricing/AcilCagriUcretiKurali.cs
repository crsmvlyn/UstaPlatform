using UstaPlatform.Domain;

namespace UstaPlatform.Pricing
{
    /// <summary>
    /// Acil çağrı ek ücreti kuralı
    /// </summary>
    public class AcilCagriUcretiKurali : IPricingRule
    {
        public string KuralAdi => "Acil Çağrı Ek Ücreti";
        public string Aciklama => "Acil işler için %50 ek ücret";

        public decimal FiyatHesapla(decimal temelFiyat, Talep talep, İşEmri işEmri)
        {
            return temelFiyat * 1.50m;
        }

        public bool UygulanabilirMi(Talep talep, İşEmri işEmri)
        {
            return talep.AcilMi;
        }
    }
}
