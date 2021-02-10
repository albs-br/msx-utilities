using System;
using System.Collections.Generic;
using System.Text;

namespace MSXUtilities.Tiles.PenguimPlatformer
{
    public class Bg_Bricks_Small
    {
        public static void Load(
            out IList<string> bgPattern_0,
            out IList<string> bgColor_0,
            out IList<string> bgPattern_1,
            out IList<string> bgColor_1
            )
        {
            bgPattern_0 = new List<string>();
            bgPattern_0.Add("00000000");
            bgPattern_0.Add("11111110");
            bgPattern_0.Add("11111110");
            bgPattern_0.Add("11111110");
            bgPattern_0.Add("00000000");
            bgPattern_0.Add("11101111");
            bgPattern_0.Add("11101111");
            bgPattern_0.Add("11101111");

            bgColor_0 = new List<string>();
            bgColor_0.Add("0x00");
            bgColor_0.Add("0x07");
            bgColor_0.Add("0x05");
            bgColor_0.Add("0x05");
            bgColor_0.Add("0x00");
            bgColor_0.Add("0x05");
            bgColor_0.Add("0x04");
            bgColor_0.Add("0x04");

            bgPattern_1 = new List<string>();
            bgPattern_1.Add("00000000");
            bgPattern_1.Add("11111110");
            bgPattern_1.Add("11101110");
            bgPattern_1.Add("11111110");
            bgPattern_1.Add("00000000");
            bgPattern_1.Add("11101111");
            bgPattern_1.Add("11101111");
            bgPattern_1.Add("11101111");

            bgColor_1 = new List<string>();
            bgColor_1.Add("0x00");
            bgColor_1.Add("0x07");
            bgColor_1.Add("0x05");
            bgColor_1.Add("0x05");
            bgColor_1.Add("0x00");
            bgColor_1.Add("0x05");
            bgColor_1.Add("0x04");
            bgColor_1.Add("0x04");
        }
    }
}
