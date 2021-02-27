using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MSXUtilities
{
    public class TilesForHorizontalScroll
    {
        /// <summary>
        /// Take two inputs and creates 8 tiles for horizontal scrolling effect on MSX1
        /// Entering line
        /// </summary>
        /// <param name="inputExiting">Left tile (exiting)</param>
        /// <param name="inputEntering">Right tile (entering)</param>
        public void CreateTilesForScrolling(IList<string> inputExiting, IList<string> inputEntering, string fileName, string description)
        {
            var extension = ".s";
            fileName = "Pattern_" + fileName + extension;
            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                sw.WriteLine(description);

                var startColumn_TileEntering = 7;
                var startColumn_TileExiting = 1;
                for (var frame = 0; frame <= 7; frame++)
                {
                    IList<string> output = new List<string>();
                    output.Add("00000000 b");
                    output.Add("00000000 b");
                    output.Add("00000000 b");
                    output.Add("00000000 b");
                    output.Add("00000000 b");
                    output.Add("00000000 b");
                    output.Add("00000000 b");
                    output.Add("00000000 b");

                    var srcColumn = 0;
                    for (var destColumn = startColumn_TileEntering; destColumn <= 7; destColumn++)
                    {
                        for (var lineNumber = 0; lineNumber <= 7; lineNumber++)
                        {
                            var src = inputEntering[lineNumber].Substring(srcColumn, 1).ToCharArray()[0];
                            var newLine = output[lineNumber].ReplaceAt(destColumn, src);
                            output[lineNumber] = newLine;
                        }
                        srcColumn++;
                    }

                    startColumn_TileEntering--;

                    srcColumn = startColumn_TileExiting;
                    for (var destColumn = 0; destColumn <= startColumn_TileEntering; destColumn++)
                    {
                        for (var lineNumber = 0; lineNumber <= 7; lineNumber++)
                        {
                            var src = inputExiting[lineNumber].Substring(srcColumn, 1).ToCharArray()[0];
                            var newLine = output[lineNumber].ReplaceAt(destColumn, src);
                            output[lineNumber] = newLine;
                        }
                        srcColumn++;
                    }

                    startColumn_TileExiting++;

                    // Print resulting tile
                    //Console.WriteLine(";Frame # " + frame);
                    //foreach (var line in output)
                    //{
                    //    Console.WriteLine("\tdb\t" + line);
                    //}
                    //Console.WriteLine();

                    // Save file
                    sw.WriteLine(";Frame # " + frame);
                    foreach (var line in output)
                    {
                        sw.WriteLine("\tdb\t" + line);
                    }
                    sw.WriteLine();
                }


                sw.WriteLine("; ----------------------------");
            }
        }

        public void CreateCompleteSetOfTilesForScrolling(
            IList<string> pattern_Bg,
            IList<string> pattern_0_top_left, // top left
            IList<string> pattern_1_top_right, // top right
            IList<string> pattern_2_bottom_left, // bottom left
            IList<string> pattern_3_bottom_right, // bottom right
            IList<string> color_0_top_left, // top left
            IList<string> color_1_top_right, // top right
            IList<string> color_2_bottom_left, // bottom left
            IList<string> color_3_bottom_right, // bottom right
            string fileName
            )
        {
            var description = String.Format("; -------- Tile transitions from {0} to {1}", "Bg", fileName + " - top left");
            CreateTilesForScrolling(pattern_Bg, pattern_0_top_left, fileName, description);

            description = String.Format("; -------- Tile transitions from {0} to {1}", fileName + " - top left", fileName + " - top right");
            CreateTilesForScrolling(pattern_0_top_left, pattern_1_top_right, fileName, description);

            description = String.Format("; -------- Tile transitions from {0} to {1}", fileName + " - top right", "Bg");
            CreateTilesForScrolling(pattern_1_top_right, pattern_Bg, fileName, description);

            description = String.Format("; -------- Tile transitions from {0} to {1}", fileName + " - top right", fileName + " - top left");
            CreateTilesForScrolling(pattern_1_top_right, pattern_0_top_left, fileName, description);



            description = String.Format("; -------- Tile transitions from {0} to {1}", "Bg", fileName + " - bottom left");
            CreateTilesForScrolling(pattern_Bg, pattern_2_bottom_left, fileName, description);

            description = String.Format("; -------- Tile transitions from {0} to {1}", fileName + " - bottom left", fileName + " - bottom");
            CreateTilesForScrolling(pattern_2_bottom_left, pattern_3_bottom_right, fileName, description);

            description = String.Format("; -------- Tile transitions from {0} to {1}", fileName + " - bottom right", "Bg");
            CreateTilesForScrolling(pattern_3_bottom_right, pattern_Bg, fileName, description);

            description = String.Format("; -------- Tile transitions from {0} to {1}", fileName + " - bottom right", fileName + " - bottom left");
            CreateTilesForScrolling(pattern_3_bottom_right, pattern_2_bottom_left, fileName, description);



            //Console.WriteLine(String.Format("; -------- Colors for {0} {1}", "Big brick", "top left"));
            CreateColorPattern(color_0_top_left, fileName + "_top_left");

            //Console.WriteLine(String.Format("; -------- Colors for {0} {1}", "Big brick", "top right"));
            CreateColorPattern(color_1_top_right, fileName + "_top_right");

            //Console.WriteLine(String.Format("; -------- Colors for {0} {1}", "Big brick", "bottom left"));
            CreateColorPattern(color_2_bottom_left, fileName + "_bottom_left");

            //Console.WriteLine(String.Format("; -------- Colors for {0} {1}", "Big brick", "bottom right"));
            CreateColorPattern(color_3_bottom_right, fileName + "_bottom_right");
        }

        static void CreateColorPattern(IList<string> colors, string fileName)
        {
            var extension = ".s";
            fileName = "Color_" + fileName + extension;
            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                foreach (var line in colors)
                {
                    sw.WriteLine("\tdb  0x" + line);
                }
                sw.WriteLine("; -----------------------");
            }
        }
    }
}