using System;
using System.Collections.Generic;
using System.Text;

namespace MSXUtilities.Pacific2.Tiles
{
    public class Bg_Land
    {
        public static void Load(
            out IList<string> bg,
            out IList<string> bgColors
            )
        {
            bg = new List<string>();
            bg.Add("11011111 b");
            bg.Add("11111101 b");
            bg.Add("11111101 b");
            bg.Add("11111111 b");
            bg.Add("10111111 b");
            bg.Add("10111111 b");
            bg.Add("11110111 b");
            bg.Add("11111111 b");

            bgColors = new List<string>();
            bgColors.Add("0xbf");
            bgColors.Add("0xbf");
            bgColors.Add("0xbe");
            bgColors.Add("0xbf");
            bgColors.Add("0xbf");
            bgColors.Add("0xbe");
            bgColors.Add("0xbf");
            bgColors.Add("0xbf");
        }
    }
}
