using System;

namespace GetMapTest.Utils
{
    public class LonLat
    {
        private double lon;
        private double lat;

        public LonLat(string lonlat)
        {
            String[] arr = lonlat.Split(new Char[] { ',', '=' });
            try
            {
                this.lon = Double.Parse(arr[1]);
                this.lat = Double.Parse(arr[3]);
            }
            catch (Exception ex)
            {
                throw new Exception("Строка '" + lonlat + "'не является корректным LonLat", ex);
            }
        }
        public LonLat(double lon, double lat)
        {
            this.lon = (double)lon;
            this.lat = (double)lat;
        }
        public double getLon()
        {
            return Math.Round(lon, 2);
        }
        public double getLat()
        {
            return Math.Round(lat, 2);
        }
        public static Boolean equalLonLat(LonLat changedPoint, LonLat startPoint) //сравниваем начальные значения центра с изменившимися координатами заданными нами
        {
            if (changedPoint.lat != startPoint.lat || changedPoint.lon != startPoint.lon)
            {
                return true;
            }
            else {
                return false;
            }
        }

    }
}
