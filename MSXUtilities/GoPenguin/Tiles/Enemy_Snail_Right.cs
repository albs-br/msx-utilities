﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSXUtilities.GoPenguin.Tiles
{
    public class Enemy_Snail_Right : TileBase
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
                "................" +
                "................" +
                "................" +
                "................" +
                "................" +
                ".....BB........." +
                "...BBBBBB......." +
                "................" +
                "................" +
                "............FF.." +
                "............F..." +
                "............CCCC" +
                "...........CCCCC" +
                "C.........CCCC.." +
                "CCC......CCCC...";

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


