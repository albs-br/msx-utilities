using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace MSXUtilities
{
    public static class ConvertNeoGeoSpritesToMsx2Sprites
    {
        public static void DoConversion(string fileName)
        {
            var bmpDestiny = new Bitmap(16, 16);

            using (Bitmap bmpSource = new Bitmap(fileName))
            {
                int xStart = 10, yStart = 342;

                bmpDestiny = bmpSource.Clone(new Rectangle(xStart, yStart, 16, 16), System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                var outputFilename = "temp.bmp";

                bmpDestiny.Save(outputFilename, ImageFormat.Bmp);
            }
        }
    }
}
