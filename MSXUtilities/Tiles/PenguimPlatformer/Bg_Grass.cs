using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSXUtilities.Tiles.PenguimPlatformer
{
    public class Bg_Grass : TileBase
    {
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
                "..CCC..CC...CCC." +
                "CC333CC33CCC333C" +
                "3333333333333333" +
                "3.3..3.3..3.3.3." +
                "................" +
                "................" +
                "99..999.99.9999." +
                "666.66..66.6666." +
                "66..66.6...6666." +
                ".........6..66.." +
                ".99.99..999....." +
                "666.66.66666...6" +
                "66..6...666..6.6" +
                "......9.....99.." +
                "..999.9999.9999." +
                ".6666.6666.6666.";

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


