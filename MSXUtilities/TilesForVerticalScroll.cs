using System;
using System.Collections.Generic;
using System.Text;

namespace MSXUtilities
{
    public class TilesForVerticalScroll
    {
        //TODO: There is no need for all these methods, just one solves it all (check TilesForHorizontalScroll)

        /// <summary>
        /// Take an input frame (base frame) and creates 8 tiles for vertical scrolling effect on MSX1
        /// Entering line
        /// </summary>
        /// <param name="input"></param>
        /// <param name="bg"></param>
        public void CreateTilesForScrolling_Entering(IList<string> input, IList<string> bg)
        {
            int index;

            // Entering line
            var startLine = 7;
            for (var frame = 0; frame <= 7; frame++)
            {
                var bgRotated = RotateTileForScrolling_Vertical(bg, frame);

                index = 0;
                for (var line = startLine; line <= 7; line++)
                {
                    Console.WriteLine("\tdb\t" + input[line]);
                    index++;
                }
                for (var line = 1; line <= startLine; line++)
                {
                    //Console.WriteLine("\tdb\t" + "00000000 b");
                    Console.WriteLine("\tdb\t" + bgRotated[index]);
                    index++;
                }

                startLine--;

                Console.WriteLine();
            }

            Console.WriteLine("; ----------------------------");
        }

        /// <summary>
        /// Take an input frame (base frame) and creates 8 tiles for vertical scrolling effect on MSX1
        /// Exiting line
        /// </summary>
        /// <param name="bg"></param>
        /// <param name="input"></param>
        public void CreateTilesForScrolling_Exiting(IList<string> bg, IList<string> input)
        {
            int index;

            // Exiting line
            var endLine = 6;
            for (var frame = 0; frame <= 7; frame++)
            {
                var bgRotated = RotateTileForScrolling_Vertical(bg, frame);

                index = 0;
                for (var line = 0; line <= frame; line++)
                {
                    Console.WriteLine("\tdb\t" + bgRotated[index]);
                    index++;
                }
                for (var line = 0; line <= endLine; line++)
                {
                    Console.WriteLine("\tdb\t" + input[line]);
                    index++;
                }

                endLine--;

                Console.WriteLine();
            }

            Console.WriteLine("; ----------------------------");
        }



        /// <summary>
        /// Take an input frame (base frame) and creates 8 tiles for vertical scrolling effect on MSX1
        /// </summary>
        /// <param name="inputEntering">Top tile (entering)</param>
        /// <param name="inputExiting">Bottom tile (exiting)</param>
        /// <param name="bg"></param>
        public void CreateTilesForScrolling_1(IList<string> inputEntering, IList<string> inputExiting)
        {
            int index;

            // Input 2 entering line / input 1 exiting line
            var startLine = 7;
            for (var frame = 0; frame <= 7; frame++)
            {
                index = 0;
                for (var line = startLine; line <= 7; line++)
                {
                    Console.WriteLine("\tdb\t" + inputEntering[line]);
                    index++;
                }
                for (var line = 0; line <= startLine - 1; line++)
                {
                    //Console.WriteLine("\tdb\t" + "00000000 b");
                    Console.WriteLine("\tdb\t" + inputExiting[line]);
                    index++;
                }

                startLine--;

                Console.WriteLine();
            }

            Console.WriteLine("; ----------------------------");
        }


        public IList<string> RotateTileForScrolling_Vertical(IList<string> input, int frame)
        {
            var output = new List<string>();

            for (var line = 0; line <= 7; line++)
            {
                //var index = ((line - frame) > 0) ? line - frame : 

                int index;
                if ((line - frame) >= 0)
                {
                    index = line - frame;
                }
                else
                {
                    index = 8 + (line - frame);
                }

                output.Add(input[index]);
            }

            return output;
        }
    }
}
