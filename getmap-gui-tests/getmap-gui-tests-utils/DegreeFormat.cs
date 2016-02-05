using System;

namespace GetMapTest.Utils
{
    public class DegreeFormat
    {
        public DegreeFormat(int deg, int min, int sec)
        {
            this.deg = deg;
            this.min = min;
            this.sec = sec;
        }
        public DegreeFormat(double deg)
        {
            this.deg = (int)deg;
            this.min = (int)Math.Round((deg - this.deg) * 60);
            this.sec = (int)(((deg - this.deg) * 60 - this.min) * 60);
        }
        public double getDecimalDegree()
        {
            return deg + min / 60.0 + sec / 3600.0;
        }
        public double getDecimalDegree(int digits)
        {
            return Math.Round(this.getDecimalDegree(), digits);
        }
        public static double getDecimalDegree(int gr, int min, int sec, int digits)
        {
            DegreeFormat df = new DegreeFormat(gr, min, sec);
            return df.getDecimalDegree(digits);
        }
        public int getDegree()
        { return deg; }
        public int getMinutes()
        { return min; }
        public int getSeconds()
        { return sec; }
        private int deg;
        private int min;
        private int sec;
    }
}
