using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSXUtilities.Tiles.PenguimPlatformer
{
    public class Bg_Bricks_Big
    {
        public static void Load(
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
            bgPattern_0 = new List<string>();
            bgPattern_0.Add("00000000");
            bgPattern_0.Add("01011111");
            bgPattern_0.Add("10101101");
            bgPattern_0.Add("01011111");
            bgPattern_0.Add("11110110");
            bgPattern_0.Add("10111111");
            bgPattern_0.Add("11111111");
            bgPattern_0.Add("11101111");

            bgColor_0 = new List<string>();
            bgColor_0.Add("0x01");
            bgColor_0.Add("0x51");
            bgColor_0.Add("0x41");
            bgColor_0.Add("0x41");
            bgColor_0.Add("0x41");
            bgColor_0.Add("0x41");
            bgColor_0.Add("0x41");
            bgColor_0.Add("0xd1");

            bgPattern_1 = new List<string>();
            bgPattern_1.Add("00000000");
            bgPattern_1.Add("11111110");
            bgPattern_1.Add("11011110");
            bgPattern_1.Add("11111110");
            bgPattern_1.Add("11111110");
            bgPattern_1.Add("11111110");
            bgPattern_1.Add("11110110");
            bgPattern_1.Add("11111110");

            bgColor_1 = new List<string>();
            bgColor_1.Add("0x01");
            bgColor_1.Add("0x51");
            bgColor_1.Add("0x41");
            bgColor_1.Add("0x41");
            bgColor_1.Add("0x41");
            bgColor_1.Add("0x41");
            bgColor_1.Add("0x41");
            bgColor_1.Add("0xd1");



            bgPattern_2 = new List<string>();
            bgPattern_2.Add("00000000");
            bgPattern_2.Add("11111110");
            bgPattern_2.Add("11111110");
            bgPattern_2.Add("11111110");
            bgPattern_2.Add("00000000");
            bgPattern_2.Add("11101111");
            bgPattern_2.Add("11101111");
            bgPattern_2.Add("11101111");

            bgColor_2 = new List<string>();
            bgColor_2.Add("0x01");
            bgColor_2.Add("0x51");
            bgColor_2.Add("0x41");
            bgColor_2.Add("0x41");
            bgColor_2.Add("0x01");
            bgColor_2.Add("0x51");
            bgColor_2.Add("0x41");
            bgColor_2.Add("0x41");

            bgPattern_3 = new List<string>();
            bgPattern_3.Add("00000000");
            bgPattern_3.Add("11111110");
            bgPattern_3.Add("11111110");
            bgPattern_3.Add("11111110");
            bgPattern_3.Add("00000000");
            bgPattern_3.Add("11101111");
            bgPattern_3.Add("11101111");
            bgPattern_3.Add("11101111");

            bgColor_3 = new List<string>();
            bgColor_3.Add("0x01");
            bgColor_3.Add("0x51");
            bgColor_3.Add("0x41");
            bgColor_3.Add("0x41");
            bgColor_3.Add("0x01");
            bgColor_3.Add("0x51");
            bgColor_3.Add("0x41");
            bgColor_3.Add("0x41");
        }

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

            bgPattern_0 = new List<string>();
            bgPattern_1 = new List<string>();
            bgPattern_2 = new List<string>();
            bgPattern_3 = new List<string>();

            bgColor_0 = new List<string>();
            bgColor_1 = new List<string>();
            bgColor_2 = new List<string>();
            bgColor_3 = new List<string>();

            for (int i = 0; i < 16; i += 2)
            {
                var sourcePattern = input.Substring(i * 8, 8);
                ConvertStringToPatternAndColor(bgPattern_0, bgColor_0, sourcePattern);

                sourcePattern = input.Substring((i+1) * 8, 8);
                ConvertStringToPatternAndColor(bgPattern_1, bgColor_1, sourcePattern);
            }
            for (int i = 16; i < 32; i += 2)
            {
                var sourcePattern = input.Substring(i * 8, 8);
                ConvertStringToPatternAndColor(bgPattern_2, bgColor_2, sourcePattern);

                sourcePattern = input.Substring((i + 1) * 8, 8);
                ConvertStringToPatternAndColor(bgPattern_3, bgColor_3, sourcePattern);
            }

        }

        private static void ConvertStringToPatternAndColor(IList<string> bgPattern_0, IList<string> bgColor_0, string sourcePattern)
        {
            if (sourcePattern.Length != 8)
            {
                throw new Exception("Pattern must be 8 bits long");
            }

            var colorCount = sourcePattern.ToCharArray().Distinct().Count();
            if (colorCount > 2)
            {
                throw new Exception("Pattern has more than 2 colors");
            }

            // TIL: color vs colour (American vs British spelling)
            var colourOf1stBit = Char.Parse(sourcePattern.Substring(0, 1));

            var fgColor = 'f';
            var bgColor = '1';
            if (colorCount == 1)
            {
                if (colourOf1stBit != '1')
                {
                    fgColor = colourOf1stBit;
                }
                else
                {
                    fgColor = '1';
                }
            }
            else if (!sourcePattern.Contains("1"))
            {
                bgColor = colourOf1stBit;
                //throw new Exception("Error");
            }

            var destinyPattern = "";
            foreach (char c in sourcePattern)
            {
                if (c == bgColor)
                {
                    destinyPattern += "0";
                }
                else
                {
                    destinyPattern += "1";
                    fgColor = c;
                }
            }
            bgPattern_0.Add(destinyPattern);
            bgColor_0.Add(fgColor.ToString() + bgColor.ToString());
        }
    }
}


