using System;
using System.Collections.Generic;
using System.Text;

namespace MSXUtilities.Tiles.Pacific2
{
    public class Land_MidRight
    {
        public static void Load(
            out IList<string> input, out IList<string> input2, out IList<string> input3,
            out IList<string> inputColors, out IList<string> input2Colors, out IList<string> input3Colors
            )
        {
            #region land patterns (middle-right)

            // last tile (top)
            input3 = new List<string>();
            input3.Add("00100100 b"); //first line only water
            input3.Add("10000000 b");
            input3.Add("11001000 b");
            input3.Add("11110001 b");
            input3.Add("11111100 b");
            input3.Add("11111110 b");
            input3.Add("11111111 b");
            input3.Add("11111111 b");

            input2 = new List<string>();
            input2.Add("11011111 b");
            input2.Add("11111101 b");
            input2.Add("11111101 b");
            input2.Add("11111111 b");
            input2.Add("10111111 b");
            input2.Add("10111111 b");
            input2.Add("11110111 b");
            input2.Add("11111111 b");

            // first tile (bottom)
            input = new List<string>();
            input.Add("11111111 b");
            input.Add("11111100 b");
            input.Add("11110000 b");
            input.Add("11101010 b");
            input.Add("11100000 b");
            input.Add("10001000 b");
            input.Add("00000010 b");
            input.Add("00100000 b"); //last line only water

            #endregion land patterns (middle-right)

            #region land colors (middle)

            // Colors
            input3Colors = new List<string>();
            input3Colors.Add("0xb4");
            input3Colors.Add("0xb4");
            input3Colors.Add("0xb4");
            input3Colors.Add("0xb4");
            input3Colors.Add("0xb4");
            input3Colors.Add("0xb4");
            input3Colors.Add("0xb4");
            input3Colors.Add("0xb4");

            input2Colors = new List<string>();
            input2Colors.Add("0xbf");
            input2Colors.Add("0xbf");
            input2Colors.Add("0xbe");
            input2Colors.Add("0xbf");
            input2Colors.Add("0xbf");
            input2Colors.Add("0xbe");
            input2Colors.Add("0xbf");
            input2Colors.Add("0xbf");

            inputColors = new List<string>();
            inputColors.Add("0xb4");
            inputColors.Add("0xb4");
            inputColors.Add("0xb4");
            inputColors.Add("0xb4");
            inputColors.Add("0xb4");
            inputColors.Add("0xb4");
            inputColors.Add("0xb4");
            inputColors.Add("0xb4");

            #endregion land colors (middle)
        }
    }
}
