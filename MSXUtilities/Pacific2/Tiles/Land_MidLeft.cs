using System;
using System.Collections.Generic;
using System.Text;

namespace MSXUtilities.Pacific2.Tiles
{
    public class Land_MidLeft
    {
        public static void Load(
            out IList<string> input, out IList<string> input2, out IList<string> input3,
            out IList<string> inputColors, out IList<string> input2Colors, out IList<string> input3Colors
            )
        {
            #region land patterns (middle-left)

            // last tile (top)
            input3 = new List<string>();
            input3.Add("00010100 b"); //first line only water
            input3.Add("01000011 b");
            input3.Add("00001111 b");
            input3.Add("01011111 b");
            input3.Add("00011111 b");
            input3.Add("10011111 b");
            input3.Add("01111111 b");
            input3.Add("01111111 b");

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
            input.Add("00111111 b");
            input.Add("00001111 b");
            input.Add("01010111 b");
            input.Add("00000111 b");
            input.Add("00100011 b");
            input.Add("00000011 b");
            input.Add("01000100 b"); //last line only water

            #endregion land patterns (middle-left)

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
