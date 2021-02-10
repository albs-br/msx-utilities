using System;
using System.Collections.Generic;
using System.Text;

namespace MSXUtilities.Tiles.Pacific2
{
    public class Rocks
    {
        public static void Load(
            out IList<string> input,
            out IList<string> inputColors
            )
        {

            #region land patterns (rocks)

            input = new List<string>();
            input.Add("00000001 b");
            input.Add("01100000 b");
            input.Add("01100000 b");
            input.Add("00000010 b");
            input.Add("00000010 b");
            input.Add("00000000 b");
            input.Add("00010000 b");
            input.Add("00010000 b");

            #endregion land patterns (rocks)

            inputColors = new List<string>();
            inputColors.Add("0x1b");
            inputColors.Add("0xfb");
            inputColors.Add("0x1b");
            inputColors.Add("0xfb");
            inputColors.Add("0x1b");
            inputColors.Add("0xbb");
            inputColors.Add("0xfb");
            inputColors.Add("0x1b");
        }
    }
}
