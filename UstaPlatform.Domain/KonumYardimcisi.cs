using System;

namespace UstaPlatform.Domain
{
    


    public static class KonumYardimcisi
    {

        public static double ManhattanMesafesi(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x2 - x1) + Math.Abs(y2 - y1);
        }


        public static double ÖklidMesafesi(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }


        public static (int X, int Y) EnYakınNokta(int hedefX, int hedefY, params (int X, int Y)[] noktalar)
        {
            if (noktalar.Length == 0)
                throw new ArgumentException("En az bir nokta belirtilmelidir.");

            var enYakın = noktalar[0];
            var enKısaMesafe = ManhattanMesafesi(hedefX, hedefY, enYakın.X, enYakın.Y);

            for (int i = 1; i < noktalar.Length; i++)
            {
                var mesafe = ManhattanMesafesi(hedefX, hedefY, noktalar[i].X, noktalar[i].Y);
                if (mesafe < enKısaMesafe)
                {
                    enKısaMesafe = mesafe;
                    enYakın = noktalar[i];
                }
            }

            return enYakın;
        }


        public static bool GeçerliKoordinat(int x, int y, int minX = -1000, int maxX = 1000, int minY = -1000, int maxY = 1000)
        {
            return x >= minX && x <= maxX && y >= minY && y <= maxY;
        }
    }
}
