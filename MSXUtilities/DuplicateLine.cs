using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace MSXUtilities
{
    public static class DuplicateLine_Class
    {
		public static void DuplicateLine(string fileNameSrc, int lineToBeDuplicated, string fileNameDest)
		{
			using (Bitmap bitmap = (Bitmap)Image.FromFile(fileNameSrc))
			{
				int width = bitmap.Width;
				int height = bitmap.Height;

				var destBitmap = new Bitmap(width, height + 1);

				// copy entire image
				{
					var srcRegion = new Rectangle(0, 0, width, height);
					var destRegion = new Rectangle(0, 0, width, height);
					CopyRegionIntoImage(bitmap, srcRegion, ref destBitmap, destRegion);
				}

				// copy line + area under line to be duplicated
				{
					var srcRegion = new Rectangle(0, lineToBeDuplicated, width, height - lineToBeDuplicated);
					var destRegion = new Rectangle(0, lineToBeDuplicated + 1, width, height - lineToBeDuplicated);
					CopyRegionIntoImage(bitmap, srcRegion, ref destBitmap, destRegion);
				}

				//// copy line
				//{
				//    var srcRegion = new Rectangle(0, lineToBeDuplicated, width, 1);
				//    var destRegion = new Rectangle(0, lineToBeDuplicated + 1, width, 1);
				//    CopyRegionIntoImage(bitmap, srcRegion, ref destBitmap, destRegion);
				//}

				// save destiny bmp
				destBitmap.Save(fileNameDest, ImageFormat.Bmp);
			}
		}

		public static void CopyRegionIntoImage(Bitmap srcBitmap, Rectangle srcRegion, ref Bitmap destBitmap, Rectangle destRegion)
		{
			using (Graphics grD = Graphics.FromImage(destBitmap))
			{
				grD.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
				grD.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
				grD.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
				grD.DrawImage(srcBitmap, destRegion, srcRegion, GraphicsUnit.Pixel);
			}
		}

	}
}
