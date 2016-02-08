using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace GetMapTest.Utils
{
    public class ImageComparer
    {
        private Bitmap result;
        private int nDifferentPixels;
        private int nPixels;
        private Color compColor = Color.FromArgb(0, 255, 0, 0);

        public ImageComparer(Bitmap image1, Bitmap image2)
        {
            this.nDifferentPixels = 0;
            this.result = ImageDiff(image1, image2, this.compColor);
        }

        public bool IsEqual()
        {
            return (this.result != null && this.nDifferentPixels == 0);
        }

        public int getDifference()
        {
            if (this.IsEqual())
                return 0;
            return (int)Math.Round(this.nDifferentPixels / (double)this.nPixels * 100.0);
        }

        private Bitmap ImageDiff(Bitmap image1, Bitmap image2, Color color)
        {
            if (image1 == null || image2 == null)
                return null;
            if (image1.Width != image2.Width || image1.Height != image2.Height)
                return null;

            Bitmap diffImage = image2.Clone() as Bitmap;
            int height = image1.Height;
            int width = image1.Width;
            this.nPixels = width * height;

            for (int x = 0; x < height; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    Color c1 = image1.GetPixel(x, y);
                    Color c2 = image2.GetPixel(x, y);
                    if (c1.ToArgb() != c2.ToArgb())
                    {
                        diffImage.SetPixel(x, y, color);
                        this.nDifferentPixels++;
                    }
                }
            }
            return diffImage;
        }

        public void Save(string fileName)
        {
            this.result.Save(fileName, ImageFormat.Jpeg);
        }
    }
}
