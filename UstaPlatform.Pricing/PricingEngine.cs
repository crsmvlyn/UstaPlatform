using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UstaPlatform.Domain;

namespace UstaPlatform.Pricing
{
    /// <summary>
    /// Fiyatlandırma motoru - Plug-in mimarisini destekler
    /// SOLID DIP prensibi: Somut sınıflara değil IPricingRule arayüzüne bağımlı
    /// </summary>
    public class PricingEngine
    {
        private readonly List<IPricingRule> _kurallar = new();
        private readonly string _pluginKlasoru;

        public PricingEngine(string pluginKlasoru = "Plugins")
        {
            _pluginKlasoru = pluginKlasoru;
            TemelKurallarıYükle();
            PluginKurallarınıYükle();
        }

        /// <summary>
        /// Temel fiyatlandırma kurallarını yükler
        /// </summary>
        private void TemelKurallarıYükle()
        {
            _kurallar.Add(new HaftasonuEkUcretiKurali());
            _kurallar.Add(new AcilCagriUcretiKurali());
            _kurallar.Add(new MesafeEkUcretiKurali());
        }

        /// <summary>
        /// Plugin klasöründeki DLL'lerden fiyatlandırma kurallarını yükler
        /// </summary>
        private void PluginKurallarınıYükle()
        {
            try
            {
                if (!Directory.Exists(_pluginKlasoru))
                {
                    Directory.CreateDirectory(_pluginKlasoru);
                    return;
                }

                var dllDosyalari = Directory.GetFiles(_pluginKlasoru, "*.dll");
                foreach (var dllDosyasi in dllDosyalari)
                {
                    try
                    {
                        var assembly = Assembly.LoadFrom(dllDosyasi);
                        var kuralTipleri = assembly.GetTypes()
                            .Where(t => typeof(IPricingRule).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                        foreach (var kuralTipi in kuralTipleri)
                        {
                            var kural = Activator.CreateInstance(kuralTipi) as IPricingRule;
                            if (kural != null)
                            {
                                _kurallar.Add(kural);
                                Console.WriteLine($"Plugin yüklendi: {kural.KuralAdi} - {dllDosyasi}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Plugin yüklenirken hata: {dllDosyasi} - {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Plugin klasörü okunurken hata: {ex.Message}");
            }
        }

        /// <summary>
        /// Tüm kuralları uygulayarak final fiyatı hesaplar
        /// </summary>
        public decimal FiyatHesapla(decimal temelFiyat, Talep talep, İşEmri işEmri)
        {
            var mevcutFiyat = temelFiyat;
            var uygulananKurallar = new List<string>();

            foreach (var kural in _kurallar)
            {
                if (kural.UygulanabilirMi(talep, işEmri))
                {
                    mevcutFiyat = kural.FiyatHesapla(mevcutFiyat, talep, işEmri);
                    uygulananKurallar.Add(kural.KuralAdi);
                }
            }

            Console.WriteLine($"Fiyat hesaplama: {ParaFormatlayici.Formatla(temelFiyat)} → {ParaFormatlayici.Formatla(mevcutFiyat)}");
            Console.WriteLine($"Uygulanan kurallar: {string.Join(", ", uygulananKurallar)}");

            return mevcutFiyat;
        }

        /// <summary>
        /// Yeni kuralları yeniden yükler (runtime'da plugin eklenmesi durumunda)
        /// </summary>
        public void KurallarıYenile()
        {
            _kurallar.Clear();
            TemelKurallarıYükle();
            PluginKurallarınıYükle();
        }

        /// <summary>
        /// Yüklü kuralları listeler
        /// </summary>
        public IEnumerable<IPricingRule> YüklüKurallar => _kurallar.AsReadOnly();

        /// <summary>
        /// Kural sayısı
        /// </summary>
        public int KuralSayisi => _kurallar.Count;
    }
}
