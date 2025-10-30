using System;
using System.Collections.Generic;
using System.Linq;
using UstaPlatform.Domain;

namespace UstaPlatform.Infrastructure
{
    /// <summary>
    /// In-memory Talep repository implementasyonu
    /// </summary>
    public class InMemoryTalepRepository : ITalepRepository
    {
        private readonly List<Talep> _talepler = new();
        private int _sonrakiId = 1;

        public IEnumerable<Talep> TümünüGetir()
        {
            return _talepler.ToList();
        }

        public Talep? IdyeGöreGetir(int id)
        {
            return _talepler.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Talep> VatandaşaGöreGetir(int vatandaşId)
        {
            return _talepler.Where(t => t.VatandaşId == vatandaşId);
        }

        public IEnumerable<Talep> DurumaGöreGetir(TalepDurumu durum)
        {
            return _talepler.Where(t => t.Durum == durum);
        }

        public Talep Ekle(Talep talep)
        {
            var yeniTalep = talep with { Id = _sonrakiId++ };
            _talepler.Add(yeniTalep);
            return yeniTalep;
        }

        public void Güncelle(Talep talep)
        {
            var mevcutTalep = _talepler.FirstOrDefault(t => t.Id == talep.Id);
            if (mevcutTalep != null)
            {
                var index = _talepler.IndexOf(mevcutTalep);
                _talepler[index] = talep;
            }
        }

        public void Sil(int id)
        {
            var talep = _talepler.FirstOrDefault(t => t.Id == id);
            if (talep != null)
            {
                _talepler.Remove(talep);
            }
        }
    }
}
