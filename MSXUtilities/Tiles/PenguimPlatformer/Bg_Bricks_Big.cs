using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSXUtilities.Tiles.PenguimPlatformer
{
    public class Bg_Bricks_Big : TileBase
    {
        //public static void Load(
        //    out IList<string> bgPattern_0,      // top left
        //    out IList<string> bgColor_0,

        //    out IList<string> bgPattern_1,      // top right
        //    out IList<string> bgColor_1,

        //    out IList<string> bgPattern_2,      // bottom left
        //    out IList<string> bgColor_2,

        //    out IList<string> bgPattern_3,      // bottom right
        //    out IList<string> bgColor_3
        //    )
        //{
        //    bgPattern_0 = new List<string>();
        //    bgPattern_0.Add("00000000");
        //    bgPattern_0.Add("01011111");
        //    bgPattern_0.Add("10101101");
        //    bgPattern_0.Add("01011111");
        //    bgPattern_0.Add("11110110");
        //    bgPattern_0.Add("10111111");
        //    bgPattern_0.Add("11111111");
        //    bgPattern_0.Add("11101111");

        //    bgColor_0 = new List<string>();
        //    bgColor_0.Add("0x01");
        //    bgColor_0.Add("0x51");
        //    bgColor_0.Add("0x41");
        //    bgColor_0.Add("0x41");
        //    bgColor_0.Add("0x41");
        //    bgColor_0.Add("0x41");
        //    bgColor_0.Add("0x41");
        //    bgColor_0.Add("0xd1");

        //    bgPattern_1 = new List<string>();
        //    bgPattern_1.Add("00000000");
        //    bgPattern_1.Add("11111110");
        //    bgPattern_1.Add("11011110");
        //    bgPattern_1.Add("11111110");
        //    bgPattern_1.Add("11111110");
        //    bgPattern_1.Add("11111110");
        //    bgPattern_1.Add("11110110");
        //    bgPattern_1.Add("11111110");

        //    bgColor_1 = new List<string>();
        //    bgColor_1.Add("0x01");
        //    bgColor_1.Add("0x51");
        //    bgColor_1.Add("0x41");
        //    bgColor_1.Add("0x41");
        //    bgColor_1.Add("0x41");
        //    bgColor_1.Add("0x41");
        //    bgColor_1.Add("0x41");
        //    bgColor_1.Add("0xd1");



        //    bgPattern_2 = new List<string>();
        //    bgPattern_2.Add("00000000");
        //    bgPattern_2.Add("11111110");
        //    bgPattern_2.Add("11111110");
        //    bgPattern_2.Add("11111110");
        //    bgPattern_2.Add("00000000");
        //    bgPattern_2.Add("11101111");
        //    bgPattern_2.Add("11101111");
        //    bgPattern_2.Add("11101111");

        //    bgColor_2 = new List<string>();
        //    bgColor_2.Add("0x01");
        //    bgColor_2.Add("0x51");
        //    bgColor_2.Add("0x41");
        //    bgColor_2.Add("0x41");
        //    bgColor_2.Add("0x01");
        //    bgColor_2.Add("0x51");
        //    bgColor_2.Add("0x41");
        //    bgColor_2.Add("0x41");

        //    bgPattern_3 = new List<string>();
        //    bgPattern_3.Add("00000000");
        //    bgPattern_3.Add("11111110");
        //    bgPattern_3.Add("11111110");
        //    bgPattern_3.Add("11111110");
        //    bgPattern_3.Add("00000000");
        //    bgPattern_3.Add("11101111");
        //    bgPattern_3.Add("11101111");
        //    bgPattern_3.Add("11101111");

        //    bgColor_3 = new List<string>();
        //    bgColor_3.Add("0x01");
        //    bgColor_3.Add("0x51");
        //    bgColor_3.Add("0x41");
        //    bgColor_3.Add("0x41");
        //    bgColor_3.Add("0x01");
        //    bgColor_3.Add("0x51");
        //    bgColor_3.Add("0x41");
        //    bgColor_3.Add("0x41");
        //}

        public static void LoadFromTinySpriteBackup(
            out IList<string> bgPattern_0,      // top left
            out IList<string> bgColor_0,

            out IList<string> bgPattern_1,      // top right
            out IList<string> bgColor_1,

            out IList<string> bgPattern_2,      // bottom left
            out IList<string> bgColor_2,

            out IList<string> bgPattern_3,      // bottom right
            out IList<string> bgColor_3
        )
        {
            var input =
                "1111111111111111" +
                "1515555555555551" +
                "4141441444144441" +
                "1414444444444441" +
                "4444144144444441" +
                "4144444444444441" +
                "4444444444441441" +
                "DDD1DDDDDDDDDDD1" +
                "1111111111111111" +
                "5555555151555555" +
                "4444444114141441" +
                "4444414144414444" +
                "4444444141444444" +
                "4414444144444144" +
                "4444444144144444" +
                "DDDDDDD1DDDDDDDD";

            LoadFromTinySpriteBackup(
                input,

                out bgPattern_0,      // top left
                out bgColor_0,

                out bgPattern_1,      // top right
                out bgColor_1,

                out bgPattern_2,      // bottom left
                out bgColor_2,

                out bgPattern_3,      // bottom right
                out bgColor_3
                );

        }
    }
}


