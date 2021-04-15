using System;
using System.Collections.Generic;
using System.Text;

namespace MSXUtilities.Pacific2.Tiles
{
    public class Land_Leftmost
    {
        public static void Load(
            out IList<string> input,
            out IList<string> inputColors
            )
        {
            #region land patterns (leftmost)

            input = new List<string>();
            input.Add("00000001 b");
            input.Add("00010000 b");
            input.Add("00000001 b");
            input.Add("00000101 b");
            input.Add("00000000 b");
            input.Add("00001000 b");
            input.Add("00000001 b");
            input.Add("00000100 b");

            #endregion land patterns (leftmost)

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
