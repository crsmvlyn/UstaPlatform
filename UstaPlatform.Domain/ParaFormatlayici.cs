using System;
using System.Globalization;

namespace UstaPlatform.Domain
{
    /// Para formatlaması için static yardımcı sınıf
    public static class ParaFormatlayici
    {
        private static readonly CultureInfo _türkçeKültür = new("tr-TR");

        /// Para miktarını Türk Lirası formatında döndürür
        public static string Formatla(decimal miktar)
        {
            return miktar.ToString("C", _türkçeKültür);
        }

        /// Para miktarını belirtilen kültüre göre formatlar
        public static string Formatla(decimal miktar, CultureInfo kültür)
        {
            return miktar.ToString("C", kültür);
        }

        /// Para miktarını özel format ile döndürür
        public static string Formatla(decimal miktar, string format)
        {
            return miktar.ToString(format, _türkçeKültür);
        }
    }
}
