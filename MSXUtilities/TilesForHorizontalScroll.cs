using System;
using System.Collections.Generic;
using System.Text;

namespace MSXUtilities
{
    public class TilesForHorizontalScroll
    {
        /// <summary>
        /// Take two inputs and creates 8 tiles for horizontal scrolling effect on MSX1
        /// Entering line
        /// </summary>
        /// <param name="inputEntering">Right tile (entering)</param>
        /// <param name="inputExiting">Left tile (exiting)</param>
        public void CreateTilesForScrolling_Entering(IList<string> inputEntering, IList<string> inputExiting)
        {
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
                Console.WriteLine(";Frame # " + frame);
                foreach (var line in output)
                {
                    Console.WriteLine("\tdb\t" + line);
                }

                Console.WriteLine();
            }


            Console.WriteLine("; ----------------------------");
        }
    }
}
