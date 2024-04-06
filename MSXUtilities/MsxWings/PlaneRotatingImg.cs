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
            var fileNameSrc = @"MsxWings\PlaneRotating.bmp";

            using (Bitmap bitmap = (Bitmap)Image.FromFile(fileNameSrc))
            {
                int width = bitmap.Width;
                int height = bitmap.Height;


                int endY_SplitImg = 78;

                Color bgColor = bitmap.GetPixel(0, 0);

                var imageIndex = 0;
                int xStart_Source = 0;
                bool endLoop = false;

                while (!endLoop)
                {
                    // find vertical start of plane (first pixel different of bg color)
                    int? Xstart_Dest = null;
                    for (int x = xStart_Source; x < width; x++)
                    {
                        if (Xstart_Dest != null) break;

                        for (int y = 0; y < endY_SplitImg; y++)
                        {
                            if (bitmap.GetPixel(x, y) != bgColor)
                            {
                                Xstart_Dest = x;
                                break;
                            }
                        }

                        if(x == width -1) endLoop = true;
                    }

                    if (!endLoop)
                    {
                        if (Xstart_Dest == null) { throw new Exception("Xstart not found"); };

                        // find vertical end of plane (first column of all pixels equal bg color)
                        int? Xend_Dest = null;
                        for (int x = (int)Xstart_Dest; x < width; x++)
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
                                Xend_Dest = x;
                                break;
                            }
                        }
                        if (Xend_Dest == null) { throw new Exception("Xend not found"); };



                        var destBitmap = new Bitmap((int)Xend_Dest - (int)Xstart_Dest + 1, endY_SplitImg + 1);


                        var srcRegion = new Rectangle((int)Xstart_Dest, 0, destBitmap.Width, destBitmap.Height);
                        var destRegion = new Rectangle(0, 0, destBitmap.Width, destBitmap.Height);
                        DuplicateLine_Class.CopyRegionIntoImage(bitmap, srcRegion, ref destBitmap, destRegion);


                        var fileNameDest = String.Format(
                            @"plane_rotating_{0}_{1}x{2}.bmp",
                            imageIndex,
                            destBitmap.Width,
                            destBitmap.Height
                            );

                        // save destiny bmp
                        Console.WriteLine("Saving file: " + fileNameDest);
                        destBitmap.Save(fileNameDest, ImageFormat.Bmp);

                        imageIndex++;
                        xStart_Source = (int)Xend_Dest;
                    }
                }
            }
        }
    }
}
