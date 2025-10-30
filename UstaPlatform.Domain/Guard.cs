using System;

namespace UstaPlatform.Domain
{
    /// Doğrulama işlemleri için static yardımcı sınıf
    public static class Guard
    {
        /// Değerin null olmadığını kontrol eder
        public static void NotNull<T>(T value, string parameterName) where T : class
        {
            if (value == null)
                throw new ArgumentNullException(parameterName);
        }

        /// String değerin boş olmadığını kontrol eder
        public static void NotNullOrEmpty(string value, string parameterName)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException($"'{parameterName}' boş olamaz.", parameterName);
        }

        /// String değerin boş veya sadece boşluk karakterlerinden oluşmadığını kontrol eder
        public static void NotNullOrWhiteSpace(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"'{parameterName}' boş veya sadece boşluk karakterlerinden oluşamaz.", parameterName);
        }

        /// Sayısal değerin pozitif olduğunu kontrol eder
        public static void Positive(int value, string parameterName)
        {
            if (value <= 0)
                throw new ArgumentException($"'{parameterName}' pozitif olmalıdır.", parameterName);
        }

        /// Sayısal değerin pozitif olduğunu kontrol eder
        public static void Positive(decimal value, string parameterName)
        {
            if (value <= 0)
                throw new ArgumentException($"'{parameterName}' pozitif olmalıdır.", parameterName);
        }

        /// Sayısal değerin negatif olmadığını kontrol eder
        public static void NotNegative(int value, string parameterName)
        {
            if (value < 0)
                throw new ArgumentException($"'{parameterName}' negatif olamaz.", parameterName);
        }

        /// Sayısal değerin negatif olmadığını kontrol eder
        public static void NotNegative(decimal value, string parameterName)
        {
            if (value < 0)
                throw new ArgumentException($"'{parameterName}' negatif olamaz.", parameterName);
        }

        /// Değerin belirtilen aralıkta olduğunu kontrol eder
        public static void InRange(int value, int min, int max, string parameterName)
        {
            if (value < min || value > max)
                throw new ArgumentOutOfRangeException(parameterName, $"'{parameterName}' {min} ile {max} arasında olmalıdır.");
        }

        /// Değerin belirtilen aralıkta olduğunu kontrol eder
        public static void InRange(decimal value, decimal min, decimal max, string parameterName)
        {
            if (value < min || value > max)
                throw new ArgumentOutOfRangeException(parameterName, $"'{parameterName}' {min} ile {max} arasında olmalıdır.");
        }
    }
}
