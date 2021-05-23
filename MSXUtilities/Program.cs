using MSXUtilities.GoPenguin.TileMaps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MSXUtilities
{
    class Program
    {
        /// <summary>
        /// MSX Utilities for homebrew game development
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //var command = "OUTI";
            //var number = 128;
            //var unrolledCommands = MakeUnrolledCommands(command, number);
            //return;

            //PocMegaROM();

            //ConvertColorTable();

            //NameTableWithOffset();

            //CalcPositionsPseudo3DEffect();

            //CreateTilesForPacific2()




            //CreateTilesForGoPenguin();
            
            // Create Tilemap for Go Penguin
            GoPenguin.TileMaps.CreateTileMap.Execute();

            Console.WriteLine("Done.");
            Console.ReadLine();
        }


        static void CreateTilesForGoPenguin()
        {
            var builder = new TilesForHorizontalScroll();


            IList<string> patternBgBlack = new List<string>();
            IList<string> colorBgBlack = new List<string>();

            IList<string> pattern_SmallBricks_0 = new List<string>();
            IList<string> pattern_SmallBricks_1 = new List<string>();
            IList<string> pattern_SmallBricks_2 = new List<string>();
            IList<string> pattern_SmallBricks_3 = new List<string>();
            IList<string> color_SmallBricks_0 = new List<string>();
            IList<string> color_SmallBricks_1 = new List<string>();
            IList<string> color_SmallBricks_2 = new List<string>();
            IList<string> color_SmallBricks_3 = new List<string>();

            IList<string> pattern_BigBricks_0 = new List<string>();
            IList<string> pattern_BigBricks_1 = new List<string>();
            IList<string> pattern_BigBricks_2 = new List<string>();
            IList<string> pattern_BigBricks_3 = new List<string>();
            IList<string> color_BigBricks_0 = new List<string>();
            IList<string> color_BigBricks_1 = new List<string>();
            IList<string> color_BigBricks_2 = new List<string>();
            IList<string> color_BigBricks_3 = new List<string>();

            IList<string> pattern_Grass_0 = new List<string>();
            IList<string> pattern_Grass_1 = new List<string>();
            IList<string> pattern_Grass_2 = new List<string>();
            IList<string> pattern_Grass_3 = new List<string>();
            IList<string> color_Grass_0 = new List<string>();
            IList<string> color_Grass_1 = new List<string>();
            IList<string> color_Grass_2 = new List<string>();
            IList<string> color_Grass_3 = new List<string>();

            IList<string> pattern_Rocks_0 = new List<string>();
            IList<string> pattern_Rocks_1 = new List<string>();
            IList<string> pattern_Rocks_2 = new List<string>();
            IList<string> pattern_Rocks_3 = new List<string>();
            IList<string> color_Rocks_0 = new List<string>();
            IList<string> color_Rocks_1 = new List<string>();
            IList<string> color_Rocks_2 = new List<string>();
            IList<string> color_Rocks_3 = new List<string>();

            IList<string> pattern_Diamond_0 = new List<string>();
            IList<string> pattern_Diamond_1 = new List<string>();
            IList<string> pattern_Diamond_2 = new List<string>();
            IList<string> pattern_Diamond_3 = new List<string>();
            IList<string> color_Diamond_0 = new List<string>();
            IList<string> color_Diamond_1 = new List<string>();
            IList<string> color_Diamond_2 = new List<string>();
            IList<string> color_Diamond_3 = new List<string>();

            IList<string> pattern_TestSquare_0 = new List<string>();
            IList<string> pattern_TestSquare_1 = new List<string>();
            IList<string> pattern_TestSquare_2 = new List<string>();
            IList<string> pattern_TestSquare_3 = new List<string>();
            IList<string> color_TestSquare_0 = new List<string>();
            IList<string> color_TestSquare_1 = new List<string>();
            IList<string> color_TestSquare_2 = new List<string>();
            IList<string> color_TestSquare_3 = new List<string>();

            IList<string> pattern_EnemyLadybug_0 = new List<string>();
            IList<string> pattern_EnemyLadybug_1 = new List<string>();
            IList<string> pattern_EnemyLadybug_2 = new List<string>();
            IList<string> pattern_EnemyLadybug_3 = new List<string>();
            IList<string> color_EnemyLadybug_0 = new List<string>();
            IList<string> color_EnemyLadybug_1 = new List<string>();
            IList<string> color_EnemyLadybug_2 = new List<string>();
            IList<string> color_EnemyLadybug_3 = new List<string>();

            IList<string> pattern_EnemySnail_0 = new List<string>();
            IList<string> pattern_EnemySnail_1 = new List<string>();
            IList<string> pattern_EnemySnail_2 = new List<string>();
            IList<string> pattern_EnemySnail_3 = new List<string>();
            IList<string> color_EnemySnail_0 = new List<string>();
            IList<string> color_EnemySnail_1 = new List<string>();
            IList<string> color_EnemySnail_2 = new List<string>();
            IList<string> color_EnemySnail_3 = new List<string>();

            // old code:
            GoPenguin.Tiles.Bg_Black.Load(out patternBgBlack, out colorBgBlack);
            //GoPenguin.Tiles.Bg_Bricks_Small.Load(out pattern_SmallBricks_0, out color_SmallBricks_0, out pattern_SmallBricks_1, out color_SmallBricks_1, out pattern_SmallBricks_2, out color_SmallBricks_2, out pattern_SmallBricks_3, out color_SmallBricks_3);

            //GoPenguin.Tiles.Bg_Bricks_Big.LoadFromTinySpriteBackup(out pattern_BigBricks_0, out color_BigBricks_0, out pattern_BigBricks_1, out color_BigBricks_1, out pattern_BigBricks_2, out color_BigBricks_2, out pattern_BigBricks_3, out color_BigBricks_3);
            GoPenguin.Tiles.Bg_Grass.LoadFromTinySpriteBackup(out pattern_Grass_0, out color_Grass_0, out pattern_Grass_1, out color_Grass_1, out pattern_Grass_2, out color_Grass_2, out pattern_Grass_3, out color_Grass_3);
            GoPenguin.Tiles.Bg_Rocks.LoadFromTinySpriteBackup(out pattern_Rocks_0, out color_Rocks_0, out pattern_Rocks_1, out color_Rocks_1, out pattern_Rocks_2, out color_Rocks_2, out pattern_Rocks_3, out color_Rocks_3);
            GoPenguin.Tiles.Bg_Diamond.LoadFromTinySpriteBackup(out pattern_Diamond_0, out color_Diamond_0, out pattern_Diamond_1, out color_Diamond_1, out pattern_Diamond_2, out color_Diamond_2, out pattern_Diamond_3, out color_Diamond_3);
            GoPenguin.Tiles.Bg_TestSquare.LoadFromTinySpriteBackup(out pattern_TestSquare_0, out color_TestSquare_0, out pattern_TestSquare_1, out color_TestSquare_1, out pattern_TestSquare_2, out color_TestSquare_2, out pattern_TestSquare_3, out color_TestSquare_3);
            GoPenguin.Tiles.Enemy_Ladybug.LoadFromTinySpriteBackup(out pattern_EnemyLadybug_0, out color_EnemyLadybug_0, out pattern_EnemyLadybug_1, out color_EnemyLadybug_1, out pattern_EnemyLadybug_2, out color_EnemyLadybug_2, out pattern_EnemyLadybug_3, out color_EnemyLadybug_3);
            GoPenguin.Tiles.Enemy_Snail.LoadFromTinySpriteBackup(out pattern_EnemySnail_0, out color_EnemySnail_0, out pattern_EnemySnail_1, out color_EnemySnail_1, out pattern_EnemySnail_2, out color_EnemySnail_2, out pattern_EnemySnail_3, out color_EnemySnail_3);

            // TODO: Fix these numbers, all of them are worng, as the small bricks now are 24 tiles, not 48
            
            // Tile pattern # 49    bg              --> top left
            // Tile pattern # 57    top left        --> top right
            // Tile pattern # 65    top right       --> bg
            // Tile pattern # 73    top right       --> top left
            // Tile pattern # 81    bg              --> bottom left
            // Tile pattern # 89    bottom left     --> bottom right
            // Tile pattern # 97    bottom right    --> bg
            // Tile pattern # 105   bottom right    --> bottom left
            //builder.CreateCompleteSetOfTilesForScrolling(patternBgBlack,
            //    pattern_BigBricks_0, pattern_BigBricks_1, pattern_BigBricks_2, pattern_BigBricks_3,
            //    color_BigBricks_0, color_BigBricks_1, color_BigBricks_2, color_BigBricks_3,
            //    "BigBricks");

            // Tile pattern # 113   bg              --> top left
            // Tile pattern # 121   top left        --> top right
            // Tile pattern # 129   top right       --> bg
            // Tile pattern # 137   top right       --> top left
            // Tile pattern # 145   bg              --> bottom left
            // Tile pattern # 153   bottom left     --> bottom right
            // Tile pattern # 161   bottom right    --> bg
            // Tile pattern # 169   bottom right    --> bottom left
            builder.CreateCompleteSetOfTilesForScrolling(patternBgBlack,
                pattern_Grass_0, pattern_Grass_1, pattern_Grass_2, pattern_Grass_3,
                color_Grass_0, color_Grass_1, color_Grass_2, color_Grass_3,
                "Grass");

            // Tile pattern # 177   bg              --> top left
            // Tile pattern # 185   top left        --> top right
            // Tile pattern # 193   top right       --> bg
            // Tile pattern # 201   top right       --> top left
            // Tile pattern # 209   bg              --> bottom left
            // Tile pattern # 217   bottom left     --> bottom right
            // Tile pattern # 225   bottom right    --> bg
            // Tile pattern # 233   bottom right    --> bottom left
            builder.CreateCompleteSetOfTilesForScrolling(patternBgBlack,
                pattern_Rocks_0, pattern_Rocks_1, pattern_Rocks_2, pattern_Rocks_3,
                color_Rocks_0, color_Rocks_1, color_Rocks_2, color_Rocks_3,
                "Rocks");

            builder.CreateCompleteSetOfTilesForScrolling(patternBgBlack,
                pattern_Diamond_0, pattern_Diamond_1, pattern_Diamond_2, pattern_Diamond_3,
                color_Diamond_0, color_Diamond_1, color_Diamond_2, color_Diamond_3,
                "Diamonds");

            builder.CreateCompleteSetOfTilesForScrolling(patternBgBlack,
                pattern_TestSquare_0, pattern_TestSquare_1, pattern_TestSquare_2, pattern_TestSquare_3,
                color_TestSquare_0, color_TestSquare_1, color_TestSquare_2, color_TestSquare_3,
                "TestSquare");

            builder.CreateCompleteSetOfTilesForScrolling_Enemy(patternBgBlack,
                pattern_EnemyLadybug_0, pattern_EnemyLadybug_1, pattern_EnemyLadybug_2, pattern_EnemyLadybug_3,
                color_EnemyLadybug_0, color_EnemyLadybug_1, color_EnemyLadybug_2, color_EnemyLadybug_3,
                "EnemyLadybug");

            builder.CreateCompleteSetOfTilesForScrolling_Enemy(patternBgBlack,
                pattern_EnemySnail_0, pattern_EnemySnail_1, pattern_EnemySnail_2, pattern_EnemySnail_3,
                color_EnemySnail_0, color_EnemySnail_1, color_EnemySnail_2, color_EnemySnail_3,
                "EnemySnail");

            // old code:
            #region Small bricks

            // Tile pattern # 1
            //Console.WriteLine(String.Format("; -------- Tile transitions from {0} to {1}", "Black", "Small brick - top"));
            //builder.CreateTilesForScrolling(patternBgBlack, pattern_SmallBricks_0);

            // Tile pattern # 9
            //Console.WriteLine(String.Format("; -------- Tile transitions from {0} to {1}", "Small brick - top", "Small brick - top"));
            //builder.CreateTilesForScrolling(pattern_SmallBricks_0, pattern_SmallBricks_1);

            // Tile pattern # 17
            //Console.WriteLine(String.Format("; -------- Tile transitions from {0} to {1}", "Small brick - top", "Black"));
            //builder.CreateTilesForScrolling(pattern_SmallBricks_0, patternBgBlack);


            // Tile pattern # 25
            //Console.WriteLine(String.Format("; -------- Tile transitions from {0} to {1}", "Black", "Small brick - bottom"));
            //builder.CreateTilesForScrolling(patternBgBlack, pattern_SmallBricks_2);

            // Tile pattern # 33
            //Console.WriteLine(String.Format("; -------- Tile transitions from {0} to {1}", "Small brick - bottom", "Small brick - bottom"));
            //builder.CreateTilesForScrolling(pattern_SmallBricks_2, pattern_SmallBricks_3);

            // Tile pattern # 41
            //Console.WriteLine(String.Format("; -------- Tile transitions from {0} to {1}", "Small brick - bottom", "Black"));
            //builder.CreateTilesForScrolling(pattern_SmallBricks_2, patternBgBlack);

            #endregion


        }

        static void CreateTilesForPacific2()
        {
            IList<string> bg = new List<string>();
            IList<string> input = new List<string>();
            IList<string> input2 = new List<string>();
            IList<string> input3 = new List<string>();

            IList<string> bgColors = new List<string>();
            IList<string> inputColors = new List<string>();
            IList<string> input2Colors = new List<string>();
            IList<string> input3Colors = new List<string>();


            var builder = new TilesForVerticalScroll();



            #region sequence of 3 or more tiles (5 transitions)

            //Tiles.Pacific2.Bg_Sea.Load(out bg, out bgColors);
            //Tiles.Pacific2.Land_MidRight.Load(out input, out input2, out input3, out inputColors, out input2Colors, out input3Colors);

            //builder.CreateTilesForScrolling_Entering(input, bg);
            //builder.CreateTilesForScrolling_1(input2, input);
            //builder.CreateTilesForScrolling_1(input2, input2);
            //builder.CreateTilesForScrolling_1(input3, input2);
            //builder.CreateTilesForScrolling_Exiting(bg, input3);

            ////builder.CreateTilesForScrolling_Entering(inputColors, bgColors);
            ////builder.CreateTilesForScrolling_1(input2Colors, inputColors);
            ////builder.CreateTilesForScrolling_1(input2Colors, input2Colors);
            ////builder.CreateTilesForScrolling_1(input3Colors, input2Colors);
            ////builder.CreateTilesForScrolling_Exiting(bgColors, input3Colors);

            #endregion sequence of 3 or more tiles (5 transitions)


            #region single tile with repetition (3 transitions)

            //Tiles.Pacific2.Bg_Sea.Load(out bg, out bgColors);
            //Tiles.Pacific2.Land_Leftmost.Load(out input, out inputColors);

            //Tiles.Pacific2.Bg_Land.Load(out bg, out bgColors);
            //Tiles.Pacific2.Trees.Load(out input, out inputColors);

            //builder.CreateTilesForScrolling_Entering(input, bg);
            //builder.CreateTilesForScrolling_1(input, input);
            //builder.CreateTilesForScrolling_Exiting(bg, input);

            //builder.CreateTilesForScrolling_Entering(inputColors, bgColors);
            //builder.CreateTilesForScrolling_1(inputColors, inputColors);
            //builder.CreateTilesForScrolling_Exiting(bgColors, inputColors);

            #endregion single tile with repetition (3 transitions)


            #region single tile without repetition (2 transitions)

            Pacific2.Tiles.Bg_Land.Load(out bg, out bgColors);
            Pacific2.Tiles.Rocks.Load(out input, out inputColors);

            //builder.CreateTilesForScrolling_Entering(input, bg);
            //builder.CreateTilesForScrolling_Exiting(bg, input);

            builder.CreateTilesForScrolling_Entering(inputColors, bgColors);
            builder.CreateTilesForScrolling_Exiting(bgColors, inputColors);

            #endregion single tile without repetition (2 transitions)
        }

        static string MakeUnrolledCommands(string command, int number)
        {
            var output = "";
            for (int i = 0; i < number; i++)
            {
                output += command + " ";
            }

            return output;
        }

        static void PocMegaROM()
        {
            var text =  "\torg	8000h,0BFFFh	; page {0}" + "\n\r" +
                        "\tdb \"Text from segment {0}\",LF,CR,0" + "\n\r" +
                        "\tds PageSize -($ -8000h),255" + "\n\r";

            Console.WriteLine();

            for (int i = 9; i <= 255; i++)
            {
                Console.WriteLine(String.Format(text, i));
            }

            return;
        }



        // left side of road
        //From (111, 63) to (0, 90)
        //From (111, -2) to (0, ?)
        static void CalcPositionsPseudo3DEffect()
        {
            var xOrigin = 111;
            var yOrigin = 63;
            var xDest = 0;
            var yDest = 90;

            decimal step = (decimal)(xOrigin - xDest) / (decimal)(yDest - yOrigin + 0);
            decimal x = xOrigin;
            for (var y = yOrigin; y <= yDest; y++)
            {
                Console.WriteLine("db " + Math.Round(x) + "         ; x coord for line " + (y - yOrigin));
                x -= step;
            }
        }


        /// <summary>
        /// Read all names table and add an offset
        /// </summary>
        static void NameTableWithOffset()
        {
            var inputFilename = "screen1.nt2";
            var outputFilename = "screen joined.nt2";
            var offset = 76;

            var bytesInput = File.ReadAllBytes(inputFilename);

            var bytesOutput = new List<byte>();


            foreach (var byteInput in bytesInput)
            {
                bytesOutput.Add((byte)(byteInput + offset));
            }

            File.WriteAllBytes(outputFilename, bytesOutput.ToArray());

            Console.WriteLine("OK");
        }


        /// <summary>
        /// Read all color table separating each byte in two nibbles and converting color value from "x" to "y'
        /// </summary>
        static void ConvertColorTable() 
        {
            var inputFilename = "screen.ct2";
            var outputFilename = "screen converted.ct2";

            var bytesInput = File.ReadAllBytes(inputFilename);

            var bytesOutput = new List<byte>();

            foreach (var byteInput in bytesInput)
            {
                var highNibble = (byteInput & 0b11110000) >> 4;

                var outputHighNibble = ReplaceNibble(highNibble) << 4;

                var lowNibble = (byteInput & 0b00001111);

                var outputLowNibble = ReplaceNibble(lowNibble);

                var byteOutput = outputHighNibble | outputLowNibble;

                bytesOutput.Add((byte)byteOutput);
            }

            File.WriteAllBytes(outputFilename, bytesOutput.ToArray());

            Console.WriteLine("OK");
        }

        static int ReplaceNibble(int nibble)
        {
            switch (nibble)
            {
                // Frame #1
                //case 7:
                //    return 14;
                //case 13:
                //    return 15;


                // Frame #2
                case 15:
                    return 8;

                case 8:
                    return 15;

                case 2:
                    return 10;

                case 10:
                    return 2;

                case 7:
                    return 15;
                case 13:
                    return 14;


                default:
                    return nibble;
                    //break;
            }

        }
    }
}
