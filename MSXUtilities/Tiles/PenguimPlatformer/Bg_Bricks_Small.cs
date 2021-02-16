using System;
using System.Collections.Generic;
using System.Text;

namespace MSXUtilities.Tiles.PenguimPlatformer
{
    public class Bg_Bricks_Small
    {
        public static void Load(
            out IList<string> bgPattern_0,      // top left
            out IList<string> bgColor_0,
            
            out IList<string> bgPattern_1,      // top right
            out IList<string> bgColor_1

            //out IList<string> bgPattern_2,      // bottom left
            //out IList<string> bgColor_2,

            //out IList<string> bgPattern_3,      // bottom right
            //out IList<string> bgColor_3
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
            bgColor_0.Add("0x01");
            bgColor_0.Add("0x71");
            bgColor_0.Add("0x51");
            bgColor_0.Add("0x51");
            bgColor_0.Add("0x01");
            bgColor_0.Add("0x51");
            bgColor_0.Add("0x41");
            bgColor_0.Add("0x41");

            bgPattern_1 = new List<string>();
            bgPattern_1.Add("00000000");
            bgPattern_1.Add("11111110");
            bgPattern_1.Add("11111110");
            bgPattern_1.Add("11111110");
            bgPattern_1.Add("00000000");
            bgPattern_1.Add("11101111");
            bgPattern_1.Add("11101111");
            bgPattern_1.Add("11101111");

            bgColor_1 = new List<string>();
            bgColor_1.Add("0x01");
            bgColor_1.Add("0x71");
            bgColor_1.Add("0x51");
            bgColor_1.Add("0x51");
            bgColor_1.Add("0x01");
            bgColor_1.Add("0x51");
            bgColor_1.Add("0x41");
            bgColor_1.Add("0x41");
        }
    }
}
