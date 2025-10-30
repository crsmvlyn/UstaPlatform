using System;
using System.Collections.Generic;
using System.Linq;
using UstaPlatform.Domain;

namespace UstaPlatform.Infrastructure
{
    /// <summary>
    /// In-memory İş emri repository implementasyonu
    /// </summary>
    public class InMemoryİşEmriRepository : IİşEmriRepository
    {
        private readonly List<İşEmri> _işEmirleri = new();
        private int _sonrakiId = 1;

        public IEnumerable<İşEmri> TümünüGetir()
        {
            return _işEmirleri.ToList();
        }

        public İşEmri? IdyeGöreGetir(int id)
        {
            return _işEmirleri.FirstOrDefault(ie => ie.Id == id);
        }

        public IEnumerable<İşEmri> UstayaGöreGetir(int ustaId)
        {
            return _işEmirleri.Where(ie => ie.UstaId == ustaId);
        }

        public IEnumerable<İşEmri> TariheGöreGetir(DateTime tarih)
        {
            return _işEmirleri.Where(ie => ie.PlanlananTarih.Date == tarih.Date);
        }

        public İşEmri Ekle(İşEmri işEmri)
        {
            var yeniİşEmri = işEmri with { Id = _sonrakiId++ };
            _işEmirleri.Add(yeniİşEmri);
            return yeniİşEmri;
        }

        public void Güncelle(İşEmri işEmri)
        {
            var mevcutİşEmri = _işEmirleri.FirstOrDefault(ie => ie.Id == işEmri.Id);
            if (mevcutİşEmri != null)
            {
                var index = _işEmirleri.IndexOf(mevcutİşEmri);
                _işEmirleri[index] = işEmri;
            }
        }

        public void Sil(int id)
        {
            var işEmri = _işEmirleri.FirstOrDefault(ie => ie.Id == id);
            if (işEmri != null)
            {
                _işEmirleri.Remove(işEmri);
            }
        }
    }
}
