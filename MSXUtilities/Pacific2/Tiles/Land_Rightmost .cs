using System;
using System.Collections.Generic;
using System.Text;

namespace MSXUtilities.Pacific2.Tiles
{
    public class Land_Rightmost
    {
        public static void Load(
            out IList<string> input,
            out IList<string> inputColors
            )
        {

            #region land patterns (rightmost)

            input = new List<string>();
            input.Add("10000000 b");
            input.Add("00010000 b");
            input.Add("10000000 b");
            input.Add("00100000 b");
            input.Add("10000000 b");
            input.Add("10001000 b");
            input.Add("00000000 b");
            input.Add("01000000 b");

            #endregion land patterns (rightmost)

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
