using System;
using Xunit;
using UstaPlatform.Domain;
using UstaPlatform.Pricing;

namespace UstaPlatform.Tests
{
    /// <summary>
    /// PricingEngine testleri
    /// </summary>
    public class PricingEngineTests
    {
        [Fact]
        public void FiyatHesapla_NormalTalep_DoğruFiyatDöndürür()
        {
            // Arrange
            var pricingEngine = new PricingEngine();
            var talep = new Talep
            {
                Id = 1,
                VatandaşId = 1,
                Aciklama = "Test talep",
                UzmanlikAlani = "Tesisatçı",
                TalepZamani = DateTime.Now,
                Adres = "Test Adres",
                AcilMi = false,
                Durum = TalepDurumu.Beklemede
            };

            var işEmri = new İşEmri
            {
                Id = 1,
                TalepId = 1,
                UstaId = 1,
                Fiyat = 100m,
                PlanlananTarih = DateTime.Now.AddDays(1), // Hafta içi
                TahminiSure = TimeSpan.FromHours(2),
                Adres = "Test Adres",
                Durum = İşEmriDurumu.Planlandi,
                OlusturmaZamani = DateTime.Now
            };

            // Act
            var sonuç = pricingEngine.FiyatHesapla(100m, talep, işEmri);

            // Assert
            Assert.Equal(100m, sonuç); // Normal talep için ek ücret yok
        }

        [Fact]
        public void FiyatHesapla_AcilTalep_EkÜcretUygular()
        {
            // Arrange
            var pricingEngine = new PricingEngine();
            var talep = new Talep
            {
                Id = 1,
                VatandaşId = 1,
                Aciklama = "Acil talep",
                UzmanlikAlani = "Elektrikçi",
                TalepZamani = DateTime.Now,
                Adres = "Test Adres",
                AcilMi = true, // Acil talep
                Durum = TalepDurumu.Beklemede
            };

            var işEmri = new İşEmri
            {
                Id = 1,
                TalepId = 1,
                UstaId = 1,
                Fiyat = 100m,
                PlanlananTarih = DateTime.Now.AddDays(1),
                TahminiSure = TimeSpan.FromHours(1),
                Adres = "Test Adres",
                Durum = İşEmriDurumu.Planlandi,
                OlusturmaZamani = DateTime.Now
            };

            // Act
            var sonuç = pricingEngine.FiyatHesapla(100m, talep, işEmri);

            // Assert
            Assert.Equal(150m, sonuç); // %50 ek ücret
        }

        [Fact]
        public void FiyatHesapla_HaftaSonuTalep_EkÜcretUygular()
        {
            // Arrange
            var pricingEngine = new PricingEngine();
            var talep = new Talep
            {
                Id = 1,
                VatandaşId = 1,
                Aciklama = "Hafta sonu talep",
                UzmanlikAlani = "Marangoz",
                TalepZamani = DateTime.Now,
                Adres = "Test Adres",
                AcilMi = false,
                Durum = TalepDurumu.Beklemede
            };

            var işEmri = new İşEmri
            {
                Id = 1,
                TalepId = 1,
                UstaId = 1,
                Fiyat = 100m,
                PlanlananTarih = new DateTime(2024, 1, 6), // Cumartesi
                TahminiSure = TimeSpan.FromHours(3),
                Adres = "Test Adres",
                Durum = İşEmriDurumu.Planlandi,
                OlusturmaZamani = DateTime.Now
            };

            // Act
            var sonuç = pricingEngine.FiyatHesapla(100m, talep, işEmri);

            // Assert
            Assert.Equal(120m, sonuç); // %20 ek ücret
        }
    }

    /// <summary>
    /// Çizelge indexer testleri
    /// </summary>
    public class ÇizelgeTests
    {
        [Fact]
        public void Indexer_UstaVeTarih_DoğruİşEmirleriniDöndürür()
        {
            // Arrange
            var çizelge = new Çizelge();
            var işEmri = new İşEmri
            {
                Id = 1,
                TalepId = 1,
                UstaId = 1,
                Fiyat = 100m,
                PlanlananTarih = new DateTime(2024, 1, 15),
                TahminiSure = TimeSpan.FromHours(2),
                Adres = "Test Adres",
                Durum = İşEmriDurumu.Planlandi,
                OlusturmaZamani = DateTime.Now
            };

            // Act
            çizelge.İşEmriEkle(işEmri);
            var tarih = DateOnly.FromDateTime(işEmri.PlanlananTarih);
            var sonuç = çizelge[1, tarih];

            // Assert
            Assert.Single(sonuç);
            Assert.Equal(işEmri.Id, sonuç[0].Id);
        }

        [Fact]
        public void Indexer_Tarih_TümUstalarınİşEmirleriniDöndürür()
        {
            // Arrange
            var çizelge = new Çizelge();
            var işEmri1 = new İşEmri
            {
                Id = 1,
                TalepId = 1,
                UstaId = 1,
                Fiyat = 100m,
                PlanlananTarih = new DateTime(2024, 1, 15),
                TahminiSure = TimeSpan.FromHours(2),
                Adres = "Test Adres",
                Durum = İşEmriDurumu.Planlandi,
                OlusturmaZamani = DateTime.Now
            };

            var işEmri2 = new İşEmri
            {
                Id = 2,
                TalepId = 2,
                UstaId = 2,
                Fiyat = 150m,
                PlanlananTarih = new DateTime(2024, 1, 15),
                TahminiSure = TimeSpan.FromHours(3),
                Adres = "Test Adres",
                Durum = İşEmriDurumu.Planlandi,
                OlusturmaZamani = DateTime.Now
            };

            // Act
            çizelge.İşEmriEkle(işEmri1);
            çizelge.İşEmriEkle(işEmri2);
            var tarih = DateOnly.FromDateTime(işEmri1.PlanlananTarih);
            var sonuç = çizelge[tarih];

            // Assert
            Assert.Equal(2, sonuç.Count);
            Assert.Contains(1, sonuç.Keys);
            Assert.Contains(2, sonuç.Keys);
        }
    }
}
