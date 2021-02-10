using System;
using System.Collections.Generic;
using System.Text;

namespace MSXUtilities.Tiles.Pacific2
{
    public class Bg_Sea
    {
        public static void Load(
            out IList<string> bg,
            out IList<string> bgColors
            )
        {
            // background (sea)
            bg = new List<string>();
            bg.Add("10000000 b");
            bg.Add("00000000 b");
            bg.Add("00000000 b");
            bg.Add("00000000 b");
            bg.Add("00000000 b");
            bg.Add("00000100 b");
            bg.Add("00000000 b");
            bg.Add("00000000 b");


            // background (blue sea)
            bgColors = new List<string>();
            bgColors.Add("0x74");
            bgColors.Add("0x74");
            bgColors.Add("0x74");
            bgColors.Add("0x74");
            bgColors.Add("0x74");
            bgColors.Add("0x74");
            bgColors.Add("0x74");
            bgColors.Add("0x74");
        }
    }
}
