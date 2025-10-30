using System;
using UstaPlatform.Domain;

namespace UstaPlatform.Pricing
{
    /// <summary>
    /// Hafta sonu ek ücreti kuralı
    /// </summary>
    public class HaftasonuEkUcretiKurali : IPricingRule
    {
        public string KuralAdi => "Hafta Sonu Ek Ücreti";
        public string Aciklama => "Hafta sonu işler için %20 ek ücret";

        public decimal FiyatHesapla(decimal temelFiyat, Talep talep, İşEmri işEmri)
        {
            return temelFiyat * 1.20m;
        }

        public bool UygulanabilirMi(Talep talep, İşEmri işEmri)
        {
            var haftaGunu = işEmri.PlanlananTarih.DayOfWeek;
            return haftaGunu == DayOfWeek.Saturday || haftaGunu == DayOfWeek.Sunday;
        }
    }
}
