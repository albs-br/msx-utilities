using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSXUtilities.Tiles.PenguinPlatformer
{
    public class Bg_Diamond : TileBase
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
                "................" +
                "........F......." +
                ".......F.F......" +
                "......FFFFF....." +
                ".....FFF.FFF...." +
                "....FFFFFFFFF..." +
                "...FFFFF.FFFFF.." +
                "....E.E.E.E.E..." +
                "...EEEEE.E.E.E.." +
                "....EEEEE.E.E..." +
                ".....EEE.E.E...." +
                "......EEE.E....." +
                ".......E.E......" +
                "........E......." +
                "................" +
                "................";

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


