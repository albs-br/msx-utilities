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
        private const string OUTPUT_PATTERNS_FILE = "patterns.s";
        private const string OUTPUT_COLORS_FILE = "colors.s";

        public struct ConversionParams
        {
            public int xStart;
            public int yStart;
            public int numberOfChars; 
        }

        public static void BatchConversion(string inputFileName, IList<ConversionParams> list)
        {
            Console.WriteLine("Batch converting NeoGeo sprites to MSX 2 sprites:");

            // delete output file, if exists
            if (File.Exists(OUTPUT_PATTERNS_FILE)) File.Delete(OUTPUT_PATTERNS_FILE);

            int startCharNumber = 0;
            foreach (var item in list)
            {
                CreatePatternsFile(inputFileName, item.xStart, item.yStart, item.numberOfChars, startCharNumber);
                startCharNumber += item.numberOfChars;
            }

            CreateColorsFile();
        }

        public static void CreatePatternsFile(string inputFileName, int xStart, int yStart, int numberOfChars, int startCharNumber)
        {
            Console.WriteLine(String.Format("---Converting NeoGeo sprites to MSX 2 sprites: {2} chars, starting from ({0}, {1})", xStart, yStart, numberOfChars));

            var patternsFile = new StringBuilder();

            using (Bitmap bmpSource = new Bitmap(inputFileName))
            {
                for (int i = 0; i < numberOfChars; i++)
                {
                    var xOrigin = xStart + (i * 80);
                    var yOrigin = yStart;

                    var bmpDestiny = new Bitmap(16, 16);

                    patternsFile.AppendLine(";------------------------- char #" + (i + startCharNumber).ToString() + " -------------------------");

                    //bmpDestiny = bmpSource.Clone(new Rectangle(xStart, yStart, 16, 16), System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                    // get original 64x64 neogeo image (actually is a 16x16 image, but each pixel is 4x4)
                    // and turn into 16x16 image
                    var outputFilename = String.Format("temp_{0}.bmp", i);
                    for (int y = 0; y < 16; y++)
                    {
                        for (int x = 0; x < 16; x++)
                        {
                            var pixel = bmpSource.GetPixel((x * 4) + xOrigin, (y * 4) + yOrigin);

                            bmpDestiny.SetPixel(x, y, pixel);
                        }
                    }
                    bmpDestiny.Save(outputFilename, ImageFormat.Bmp);



                    // convert from 16x16 bmp to MSX2 sprite format
                    var tempPattern_0 = new StringBuilder();
                    var tempPattern_1 = new StringBuilder();
                    for (int y = 0; y < 16; y++)
                    {
                        for (int x = 0; x < 16; x++)
                        {
                            var pixel = bmpDestiny.GetPixel(x, y);

                            if (pixel.ToArgb() == -1182473) // transparent
                            {
                                tempPattern_0.Append("0");
                                tempPattern_1.Append("0");
                            }
                            else if (pixel.ToArgb() == -6822916) // color 0
                            {
                                tempPattern_0.Append("1");
                                tempPattern_1.Append("0");
                            }
                            else // color1
                            {
                                tempPattern_0.Append("0");
                                tempPattern_1.Append("1");
                            }
                        }
                        //tempPattern_0.Append(" b");
                        //tempPattern_1.Append(" b");
                        tempPattern_0.AppendLine();
                        tempPattern_1.AppendLine();
                    }

                    // convert sprite patterns from 16 lines of 16 bits to MSX format (32 lines of 8 bits)
                    var pattern_0 = new StringBuilder("; ------ pattern 0" + Environment.NewLine);
                    var pattern_1 = new StringBuilder("; ------ pattern 1" + Environment.NewLine);
                    foreach (var line in tempPattern_0.ToString().Split(Environment.NewLine))
                    {
                        if (line != "")
                        {
                            pattern_0.AppendLine("\tdb\t" + line.Substring(0, 8) + " b");
                        }
                    }
                    foreach (var line in tempPattern_0.ToString().Split(Environment.NewLine))
                    {
                        if (line != "")
                        {
                            pattern_0.AppendLine("\tdb\t" + line.Substring(8, 8) + " b");
                        }
                    }
                    foreach (var line in tempPattern_1.ToString().Split(Environment.NewLine))
                    {
                        if (line != "")
                        {
                            pattern_1.AppendLine("\tdb\t" + line.Substring(0, 8) + " b");
                        }
                    }
                    foreach (var line in tempPattern_1.ToString().Split(Environment.NewLine))
                    {
                        if (line != "")
                        {
                            pattern_1.AppendLine("\tdb\t" + line.Substring(8, 8) + " b");
                        }
                    }

                    patternsFile.AppendLine(pattern_0.ToString() + pattern_1.ToString());
                }
            }

            // save text file with patterns (append if already exists)
            File.AppendAllText(OUTPUT_PATTERNS_FILE, patternsFile.ToString());
        }

        private static void CreateColorsFile()
        {
            var colorsFile = new StringBuilder();

            var color_0 = new StringBuilder("; ------ color 0" + Environment.NewLine);
            var color_1 = new StringBuilder("; ------ color 1" + Environment.NewLine);
            for (int y = 0; y < 16; y++)
            {
                color_0.AppendLine("\tdb\t" + "0x08");
                color_1.AppendLine("\tdb\t" + "0x03");
            }
            colorsFile.AppendLine(color_0.ToString() + color_1.ToString());

            // save text file with colors
            File.WriteAllText(OUTPUT_COLORS_FILE, colorsFile.ToString());
        }
    }
}
