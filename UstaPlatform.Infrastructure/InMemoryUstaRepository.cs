using System;
using System.Collections.Generic;
using System.Linq;
using UstaPlatform.Domain;

namespace UstaPlatform.Infrastructure
{
    /// <summary>
    /// In-memory Usta repository implementasyonu
    /// </summary>
    public class InMemoryUstaRepository : IUstaRepository
    {
        private readonly List<Usta> _ustalar = new();
        private int _sonrakiId = 1;

        public IEnumerable<Usta> TümünüGetir()
        {
            return _ustalar.ToList();
        }

        public Usta? IdyeGöreGetir(int id)
        {
            return _ustalar.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Usta> UzmanlikAlaninaGöreGetir(string uzmanlikAlani)
        {
            return _ustalar.Where(u => u.UzmanlikAlani.Equals(uzmanlikAlani, StringComparison.OrdinalIgnoreCase));
        }

        public Usta Ekle(Usta usta)
        {
            var yeniUsta = usta with { Id = _sonrakiId++ };
            _ustalar.Add(yeniUsta);
            return yeniUsta;
        }

        public void Güncelle(Usta usta)
        {
            var mevcutUsta = _ustalar.FirstOrDefault(u => u.Id == usta.Id);
            if (mevcutUsta != null)
            {
                var index = _ustalar.IndexOf(mevcutUsta);
                _ustalar[index] = usta;
            }
        }

        public void Sil(int id)
        {
            var usta = _ustalar.FirstOrDefault(u => u.Id == id);
            if (usta != null)
            {
                _ustalar.Remove(usta);
            }
        }
    }
}
