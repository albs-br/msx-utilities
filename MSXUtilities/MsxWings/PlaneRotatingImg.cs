using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Text;

namespace MSXUtilities.MsxWings
{
    public static class PlaneRotatingImg
    {
        public static void SplitImage()
        {
            var fileNameSrc = @"plane_rotating.bmp";

            using (Bitmap bitmap = (Bitmap)Image.FromFile(fileNameSrc))
            {
                int width = bitmap.Width;
                int height = bitmap.Height;


                int endY_SplitImg = 78;

                Color bgColor = bitmap.GetPixel(0, 0);

                // find vertical start of plane (first pixel different of bg color)
                int? Xstart = null;
                for (int x = 0; x < width; x++)
                {
                    if (Xstart != null) break;

                    for (int y = 0; y < endY_SplitImg; y++) 
                    {
                        if (bitmap.GetPixel(x, y) != bgColor)
                        {
                            Xstart = x;
                            break;
                        }
                    }
                }
                if (Xstart == null) { throw new Exception("Xstart not found"); };

                // find vertical end of plane (first column of all pixels equal bg color)
                int? Xend = null;
                for (int x = (int)Xstart; x < width; x++)
                {
                    bool allPixelsEqualBgColor = true;
                    for (int y = 0; y < endY_SplitImg; y++)
                    {
                        if (bitmap.GetPixel(x, y) != bgColor)
                        {
                            allPixelsEqualBgColor = false;
                            break;
                        }
                    }
                    
                    if (allPixelsEqualBgColor) 
                    { 
                        Xend = x;
                        break;
                    }
                }
                if (Xend == null) { throw new Exception("Xend not found"); };



                var destBitmap = new Bitmap((int)Xend - (int)Xstart + 1, endY_SplitImg + 1);


                var srcRegion = new Rectangle((int)Xstart, 0, destBitmap.Width, destBitmap.Height);
                var destRegion = new Rectangle(0, 0, destBitmap.Width, destBitmap.Height);
                DuplicateLine_Class.CopyRegionIntoImage(bitmap, srcRegion, ref destBitmap, destRegion);

                var imageIndex = 0;

                var fileNameDest = String.Format(
                    @"plane_rotating_{0}_{1}x{2}.bmp",
                    imageIndex,
                    destBitmap.Width,
                    destBitmap.Height
                    );

                // save destiny bmp
                destBitmap.Save(fileNameDest, ImageFormat.Bmp);
            }
        }
    }
}
