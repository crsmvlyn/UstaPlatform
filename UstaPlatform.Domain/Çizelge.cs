using System;
using System.Collections.Generic;
using System.Linq;

namespace UstaPlatform.Domain
{
    /// Ustaların iş emri takvimi
    /// Tarihe göre iş emirlerini listeleyen Dizinleyici (Indexer) içerir
    public class Çizelge
    {
        private readonly Dictionary<int, Dictionary<DateOnly, List<İşEmri>>> _ustalarınÇizelgesi = new();

        /// Belirli bir usta ve tarih için iş emirlerini getirir
        public List<İşEmri> this[int ustaId, DateOnly tarih]
        {
            get
            {
                if (!_ustalarınÇizelgesi.ContainsKey(ustaId))
                {
                    _ustalarınÇizelgesi[ustaId] = new Dictionary<DateOnly, List<İşEmri>>();
                }

                if (!_ustalarınÇizelgesi[ustaId].ContainsKey(tarih))
                {
                    _ustalarınÇizelgesi[ustaId][tarih] = new List<İşEmri>();
                }

                return _ustalarınÇizelgesi[ustaId][tarih];
            }
        }

        /// Belirli bir tarih için tüm ustaların iş emirlerini getirir
        public Dictionary<int, List<İşEmri>> this[DateOnly tarih]
        {
            get
            {
                var sonuc = new Dictionary<int, List<İşEmri>>();
                foreach (var ustaId in _ustalarınÇizelgesi.Keys)
                {
                    if (_ustalarınÇizelgesi[ustaId].ContainsKey(tarih))
                    {
                        sonuc[ustaId] = _ustalarınÇizelgesi[ustaId][tarih];
                    }
                }
                return sonuc;
            }
        }

        /// İş emrini çizelgeye ekler
        public void İşEmriEkle(İşEmri işEmri)
        {
            var tarih = DateOnly.FromDateTime(işEmri.PlanlananTarih);
            this[işEmri.UstaId, tarih].Add(işEmri);
        }

        /// Belirli bir usta ve tarih için iş emirlerini kaldırır
        public bool İşEmriKaldır(int ustaId, DateOnly tarih, int işEmriId)
        {
            if (!_ustalarınÇizelgesi.ContainsKey(ustaId) || 
                !_ustalarınÇizelgesi[ustaId].ContainsKey(tarih))
            {
                return false;
            }

            var işEmri = _ustalarınÇizelgesi[ustaId][tarih].FirstOrDefault(ie => ie.Id == işEmriId);
            if (işEmri != null)
            {
                _ustalarınÇizelgesi[ustaId][tarih].Remove(işEmri);
                return true;
            }

            return false;
        }

        /// Belirli bir usta için tüm iş emirlerini getirir
        public List<İşEmri> UstaİşEmirleri(int ustaId)
        {
            var sonuc = new List<İşEmri>();
            if (_ustalarınÇizelgesi.ContainsKey(ustaId))
            {
                foreach (var tarihListesi in _ustalarınÇizelgesi[ustaId].Values)
                {
                    sonuc.AddRange(tarihListesi);
                }
            }
            return sonuc.OrderBy(ie => ie.PlanlananTarih).ToList();
        }

        /// Belirli bir tarih aralığındaki tüm iş emirlerini getirir
        public List<İşEmri> TarihAralığıİşEmirleri(DateOnly başlangıç, DateOnly bitiş)
        {
            var sonuc = new List<İşEmri>();
            foreach (var ustaÇizelgesi in _ustalarınÇizelgesi.Values)
            {
                foreach (var tarihListesi in ustaÇizelgesi)
                {
                    if (tarihListesi.Key >= başlangıç && tarihListesi.Key <= bitiş)
                    {
                        sonuc.AddRange(tarihListesi.Value);
                    }
                }
            }
            return sonuc.OrderBy(ie => ie.PlanlananTarih).ToList();
        }

        /// Çizelgedeki toplam iş emri sayısı
        public int ToplamİşEmriSayısı => _ustalarınÇizelgesi.Values
            .SelectMany(u => u.Values)
            .SelectMany(liste => liste)
            .Count();

        public override string ToString()
        {
            return $"Çizelge - {_ustalarınÇizelgesi.Count} usta, {ToplamİşEmriSayısı} iş emri";
        }
    }
}
