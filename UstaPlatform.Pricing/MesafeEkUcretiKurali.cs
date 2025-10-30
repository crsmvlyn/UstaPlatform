using System;
using UstaPlatform.Domain;

namespace UstaPlatform.Pricing
{
    /// <summary>
    /// Mesafe ek ücreti kuralı
    /// </summary>
    public class MesafeEkUcretiKurali : IPricingRule
    {
        private const int MerkezX = 0;
        private const int MerkezY = 0;
        private const int MesafeLimit = 10; // km
        private const decimal KmBasinaUcret = 5m;

        public string KuralAdi => "Mesafe Ek Ücreti";
        public string Aciklama => $"Merkezden {MesafeLimit} km uzaklık için km başına {ParaFormatlayici.Formatla(KmBasinaUcret)}";

        public decimal FiyatHesapla(decimal temelFiyat, Talep talep, İşEmri işEmri)
        {
            // Basit mesafe hesaplama (gerçek uygulamada adres koordinatları kullanılır)
            var mesafe = KonumYardimcisi.ManhattanMesafesi(MerkezX, MerkezY, 15, 15); // Örnek koordinat
            if (mesafe > MesafeLimit)
            {
                var ekMesafe = mesafe - MesafeLimit;
                return temelFiyat + (decimal)ekMesafe * KmBasinaUcret;
            }
            return temelFiyat;
        }

        public bool UygulanabilirMi(Talep talep, İşEmri işEmri)
        {
            // Her zaman uygulanabilir, mesafe kontrolü FiyatHesapla içinde yapılır
            return true;
        }
    }
}
