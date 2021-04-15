using System;
using System.Collections.Generic;
using System.Text;

namespace MSXUtilities.Pacific2.Tiles
{
    public class SmallIsland
    {
        public static void Load(
            out IList<string> input,
            out IList<string> inputColors
            )
        {
            // One single small island
            input = new List<string>();
            input.Add("01001100 b");
            input.Add("11111110 b");
            input.Add("11111110 b");
            input.Add("11111111 b");
            input.Add("01111111 b");
            input.Add("11111111 b");
            input.Add("01111111 b");
            input.Add("00111100 b");

            inputColors = new List<string>();
            inputColors.Add("0xb4");
            inputColors.Add("0xb4");
            inputColors.Add("0xb4");
            inputColors.Add("0xb4");
            inputColors.Add("0xb4");
            inputColors.Add("0xb4");
            inputColors.Add("0xb4");
            inputColors.Add("0xb4");
        }
    }
}
