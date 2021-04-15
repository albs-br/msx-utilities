using System;
using System.Collections.Generic;
using System.Text;

namespace MSXUtilities.Pacific2.Tiles
{
    public class Trees
    {
        public static void Load(
            out IList<string> input,
            out IList<string> inputColors
            )
        {

            #region land patterns (trees)

            input = new List<string>();
            input.Add("10000000 b");
            input.Add("01010100 b");
            input.Add("10100000 b");
            input.Add("01010010 b");
            input.Add("10100000 b");
            input.Add("00001000 b");
            input.Add("01000000 b");
            input.Add("00000000 b");

            //input.Add("11111111 b");
            //input.Add("11000001 b");
            //input.Add("10101010 b");
            //input.Add("11010000 b");
            //input.Add("10101010 b");
            //input.Add("11010000 b");
            //input.Add("10000100 b");
            //input.Add("11000001 b");


            #endregion land patterns (trees)

            //inputColors = new List<string>();
            //inputColors.Add("0xbc");
            //inputColors.Add("0x1c");
            //inputColors.Add("0x1c");
            //inputColors.Add("0x1c");
            //inputColors.Add("0x1c");
            //inputColors.Add("0x1c");
            //inputColors.Add("0x1c");
            //inputColors.Add("0x1c");

            inputColors = new List<string>();
            inputColors.Add("0xb3");
            inputColors.Add("0xc3");
            inputColors.Add("0xc3");
            inputColors.Add("0xc3");
            inputColors.Add("0xc3");
            inputColors.Add("0xc3");
            inputColors.Add("0xc3");
            inputColors.Add("0xc3");
        }
    }
}
