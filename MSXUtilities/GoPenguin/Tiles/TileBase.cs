using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSXUtilities.GoPenguin.Tiles
{
    public class TileBase
    {
        public static void LoadFromTinySpriteBackup(
            string input,

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

                sourcePattern = input.Substring((i + 1) * 8, 8);
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

            sourcePattern = sourcePattern.Replace(".", "1").Replace("0", "1");

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
            }
            else if (!sourcePattern.Contains("1"))
            {
                throw new Exception("2 colour pattern must contain the color black");
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

