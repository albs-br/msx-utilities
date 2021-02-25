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
            IList<string> pattern_0, 
            IList<string> pattern_1, 
            IList<string> pattern_2, 
            IList<string> pattern_3, 
            string fileName
            )
        {

            // Tile pattern # 49
            //TODO: fix this text to a more generic text
            // e.g. var description = String.Format("; -------- Tile transitions from {0} to {1}", "Bg", "top left");
            var description = String.Format("; -------- Tile transitions from {0} to {1}", "Black", "Big brick - top left");
            CreateTilesForScrolling(pattern_Bg, pattern_0, fileName, description);

            // Tile pattern # 57
            description = String.Format("; -------- Tile transitions from {0} to {1}", "Big brick - top left", "Big brick - top right");
            CreateTilesForScrolling(pattern_0, pattern_1, fileName, description);

            // Tile pattern # 65
            description = String.Format("; -------- Tile transitions from {0} to {1}", "Big brick - top right", "Black");
            CreateTilesForScrolling(pattern_1, pattern_Bg, fileName, description);

            // Tile pattern # 73
            description = String.Format("; -------- Tile transitions from {0} to {1}", "Big brick - top right", "Big brick - top left");
            CreateTilesForScrolling(pattern_1, pattern_0, fileName, description);



            // Tile pattern # 81
            description = String.Format("; -------- Tile transitions from {0} to {1}", "Black", "Big brick - bottom left");
            CreateTilesForScrolling(pattern_Bg, pattern_2, fileName, description);

            // Tile pattern # 89
            description = String.Format("; -------- Tile transitions from {0} to {1}", "Big brick - bottom left", "Big brick - bottom");
            CreateTilesForScrolling(pattern_2, pattern_3, fileName, description);

            // Tile pattern # 97
            description = String.Format("; -------- Tile transitions from {0} to {1}", "Big brick - bottom right", "Black");
            CreateTilesForScrolling(pattern_3, pattern_Bg, fileName, description);

            // Tile pattern # 105
            description = String.Format("; -------- Tile transitions from {0} to {1}", "Big brick - bottom right", "Big brick - bottom left");
            CreateTilesForScrolling(pattern_3, pattern_2, fileName, description);



            //Console.WriteLine(String.Format("; -------- Colors for {0} {1}", "Big brick", "top left"));
            //ShowColors(color_BigBricks_0);

            //Console.WriteLine(String.Format("; -------- Colors for {0} {1}", "Big brick", "top right"));
            //ShowColors(color_BigBricks_1);

            //Console.WriteLine(String.Format("; -------- Colors for {0} {1}", "Big brick", "bottom left"));
            //ShowColors(color_BigBricks_2);

            //Console.WriteLine(String.Format("; -------- Colors for {0} {1}", "Big brick", "bottom right"));
            //ShowColors(color_BigBricks_3);
        }
    }
}