using System;
using System.Collections.Generic;
using System.Text;

namespace MSXUtilities.Tiles.PenguimPlatformer
{
    public class Bg_Black
    {
        public static void Load(
            out IList<string> bgPattern_0,
            out IList<string> bgColor_0
            )
        {
            bgPattern_0 = new List<string>();
            bgPattern_0.Add("00000000");
            bgPattern_0.Add("00000000");
            bgPattern_0.Add("00000000");
            bgPattern_0.Add("00000000");
            bgPattern_0.Add("00000000");
            bgPattern_0.Add("00000000");
            bgPattern_0.Add("00000000");
            bgPattern_0.Add("00000000");

            bgColor_0 = new List<string>();
            bgColor_0.Add("0x01");
            bgColor_0.Add("0x01");
            bgColor_0.Add("0x01");
            bgColor_0.Add("0x01");
            bgColor_0.Add("0x01");
            bgColor_0.Add("0x01");
            bgColor_0.Add("0x01");
            bgColor_0.Add("0x01");
        }
    }
}
