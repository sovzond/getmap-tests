using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetMapTest.Utils
{

    public class XY
    {
        private int x;
        private int y;

        public XY(string xy)
        {
            String[] arr = xy.Split(new Char[] { ',', '=' });
            try
            {
                this.x = Int32.Parse(arr[1]);
                this.y = Int32.Parse(arr[3]);
            }
            catch (Exception)
            {
                throw new Exception("Строка " + xy + "не является коректным для XY");
            }
        }

        public XY(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int getX
        {
            get
            {
                return x;
            }

        }

        public int getY
        {
            get
            {
                return y;
            }
        }
    }
}
