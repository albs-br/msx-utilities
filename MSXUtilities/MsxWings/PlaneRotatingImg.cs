using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Text;
using System.IO;

namespace MSXUtilities.MsxWings
{
    public static class PlaneRotatingImg
    {
        public static void SplitImage(int startY_SplitImg, int endY_SplitImg, int imageIndex)
        {
            var fileNameSrc = @"MsxWings\PlaneRotating.bmp";

            StringBuilder sbCommands = new StringBuilder();
            StringBuilder sbCommands1 = new StringBuilder();

            using (Bitmap bitmap = (Bitmap)Image.FromFile(fileNameSrc))
            {

                int width = bitmap.Width;
                int height = bitmap.Height;


                //int endY_SplitImg = 78;

                Color bgColor = bitmap.GetPixel(0, 0);

                //var imageIndex = 0;
                int xStart_Source = 0;
                bool endLoop = false;

                while (!endLoop)
                {
                    // find vertical start of plane (first pixel different of bg color)
                    int? Xstart_Dest = null;
                    for (int x = xStart_Source; x < width; x++)
                    {
                        if (Xstart_Dest != null) break;

                        for (int y = startY_SplitImg; y < endY_SplitImg; y++)
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
                            for (int y = startY_SplitImg; y < endY_SplitImg; y++)
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

                        // find horizontal start of split image (first line with some pixel != bgColor)
                        int Ystart_Dest = startY_SplitImg;
                        bool endLoop_1 = false;
                        for (int y = Ystart_Dest; y < endY_SplitImg; y++)
                        {
                            for (int x = (int)Xstart_Dest; x < (int)Xend_Dest; x++)
                            {
                                if (bitmap.GetPixel(x, y) != bgColor)
                                {
                                    Ystart_Dest = y;
                                    endLoop_1 = true;
                                    break;
                                }
                            }

                            if (endLoop_1) break;
                        }

                        // find horizontal end of split image (last line with some pixel != bgColor)
                        int YEnd_Dest = endY_SplitImg;
                        bool endLoop_2 = false;
                        for (int y = YEnd_Dest; y > Ystart_Dest; y--)
                        {
                            for (int x = (int)Xstart_Dest; x < (int)Xend_Dest; x++)
                            {
                                if (bitmap.GetPixel(x, y) != bgColor)
                                {
                                    YEnd_Dest = y;
                                    endLoop_2 = true;
                                    break;
                                }
                            }

                            if (endLoop_2) break;
                        }

                        var destBitmap = new Bitmap((int)Xend_Dest - (int)Xstart_Dest + 1, YEnd_Dest - Ystart_Dest + 1);


                        var srcRegion = new Rectangle((int)Xstart_Dest, Ystart_Dest, destBitmap.Width, destBitmap.Height);
                        var destRegion = new Rectangle(0, 0, destBitmap.Width, destBitmap.Height);
                        DuplicateLine_Class.CopyRegionIntoImage(bitmap, srcRegion, ref destBitmap, destRegion);


                        var fileNameDest = String.Format(
                            @"plane_rotating_{0}_size_{1}x{2}_position_{3}_{4}",
                            imageIndex,
                            destBitmap.Width,
                            destBitmap.Height,
                            Xstart_Dest,
                            Ystart_Dest - startY_SplitImg
                            );

                        // save destiny bmp
                        Console.WriteLine("Saving file: " + fileNameDest + ".bmp");
                        destBitmap.Save(fileNameDest + ".bmp", ImageFormat.Bmp);

                        sbCommands.AppendLine(String.Format(".frame_{0}:", imageIndex));
                        sbCommands.AppendLine(String.Format("	INCBIN \"ChooseInputScreen/zx0_images/{0}.sc5_small.zx0\"", fileNameDest));

                        sbCommands1.AppendLine(String.Format("  dw	PlaneRotating_Images_?.frame_{0} 	db {1}, {2}	dw {3} * 128", 
                            imageIndex,
                            (int)Math.Ceiling(((decimal)destBitmap.Width / 2)), // width in bytes
                            destBitmap.Height,
                            Ystart_Dest - startY_SplitImg
                            ));

                        imageIndex++;
                        xStart_Source = (int)Xend_Dest;
                    }
                }
            }

            Console.WriteLine("---------------");
            Console.WriteLine(sbCommands.ToString());
            Console.WriteLine("---------------");
            Console.WriteLine(sbCommands1.ToString());
        }

        public static void List_PrepareSC5Image(int firstIndex, int lastIndex)
        {
            DirectoryInfo d = new DirectoryInfo(@"C:\Users\albs_\source\repos\msx-utilities\MSXUtilities\bin\Debug\netcoreapp3.1");

            FileInfo[] Files = d.GetFiles("plane_rotating_*.sc5");

            foreach (FileInfo file in Files)
            {
                int imageIndex = int.Parse(file.Name.Split('_')[2]);
                if (imageIndex >= firstIndex && imageIndex <= lastIndex)
                {
                    PrepareSC5Image(file.Name.ToLower().Replace(".sc5", ""));
                }
            }


            //PrepareSC5Image(@"plane_rotating_0_size_103x71_position_5_3");
        }

        /// <summary>
        /// Remove 7-byte header and unused columns (from 256 cols to the size of image)
        /// </summary>
        public static void PrepareSC5Image(string filename)
        {
            // open SC5 file
            byte[] byteArraySource = File.ReadAllBytes(filename + ".sc5");
            //byte[] byteArrayDestiny = new byte[] { };
            List<byte> byteListDestiny = new List<byte>();

            // get image width and height from file name
            var temp = filename.Split('_')[4].Split('x');
            int widthInPixels = int.Parse(temp[0]);
            int widthInBytes = (int)Math.Ceiling(((decimal)widthInPixels / 2));
            int heightInPixels = int.Parse(temp[1]);

            // remove first 7 bytes (header)
            int columnCounter = 0;
            int lineCounter = 0;
            for (int i = 7; i < byteArraySource.Length; i++)
            {
                if (columnCounter == 128)
                {
                    columnCounter = 0; // 128 bytes per line (SC 5)
                    lineCounter++;
                }

                // keep only source width bytes (div by 2) per line from SC5
                // keep only source height from SC5
                if ((columnCounter < widthInBytes) && (lineCounter < heightInPixels))
                {
                    byteListDestiny.Add(byteArraySource[i]);
                }

                columnCounter++;
            }

            File.WriteAllBytes(filename + ".sc5_small", byteListDestiny.ToArray());
            Console.WriteLine(filename + ".sc5_small");
        }
    }
}
