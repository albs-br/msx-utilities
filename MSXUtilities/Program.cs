using MSXUtilities.Tiles.PenguimPlatformer.TileMaps;
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

            CreateTilesForPenguimPlatformer();
            //CreateTileMapForPenguimPlatformer();

            Console.WriteLine("Done.");
            Console.ReadLine();
        }


        static void CreateTilesForPenguimPlatformer()
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


            Tiles.PenguimPlatformer.Bg_Black.Load(out patternBgBlack, out colorBgBlack);
            Tiles.PenguimPlatformer.Bg_Bricks_Small.Load(out pattern_SmallBricks_0, out color_SmallBricks_0, out pattern_SmallBricks_1, out color_SmallBricks_1, out pattern_SmallBricks_2, out color_SmallBricks_2, out pattern_SmallBricks_3, out color_SmallBricks_3);
            Tiles.PenguimPlatformer.Bg_Bricks_Big.LoadFromTinySpriteBackup(out pattern_BigBricks_0, out color_BigBricks_0, out pattern_BigBricks_1, out color_BigBricks_1, out pattern_BigBricks_2, out color_BigBricks_2, out pattern_BigBricks_3, out color_BigBricks_3);
            Tiles.PenguimPlatformer.Bg_Grass.LoadFromTinySpriteBackup(out pattern_Grass_0, out color_Grass_0, out pattern_Grass_1, out color_Grass_1, out pattern_Grass_2, out color_Grass_2, out pattern_Grass_3, out color_Grass_3);

            // Tile pattern # 49    bg              --> top left
            // Tile pattern # 57    top left        --> top right
            // Tile pattern # 65    top right       --> bg
            // Tile pattern # 73    top right       --> top left
            // Tile pattern # 81    bg              --> bottom left
            // Tile pattern # 89    bottom left     --> bottom right
            // Tile pattern # 97    bottom right    --> bg
            // Tile pattern # 105   bottom right    --> bottom left
            builder.CreateCompleteSetOfTilesForScrolling(patternBgBlack,
                pattern_BigBricks_0, pattern_BigBricks_1, pattern_BigBricks_2, pattern_BigBricks_3,
                color_BigBricks_0, color_BigBricks_1, color_BigBricks_2, color_BigBricks_3,
                "BigBricks");

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

        static void CreateTileMapForPenguimPlatformer()
        {
            var fileName = "TileMap_Page_{0}.s";

            for (int i = 1; i <= 6; i++)
            {
                var fileToBeDeleted = String.Format(fileName, i);
                if (File.Exists(fileToBeDeleted))
                {
                    File.Delete(fileToBeDeleted);
                }
            }

            const int TILEMAP_SIZE_IN_8X8_COLUMNS = 512;

            var tileMap_16x16_Static = TileMap_Level_Test.LoadTileMap();

            // ---- Conversion logic from tilemap 16x16 static to 8x8 animated

            // Loop all tilemap16x16 testing < 256 and filling with zeroes to make = 256
            for (int line = 0; line < tileMap_16x16_Static.Count; line++)
            {
                if (tileMap_16x16_Static[line].Count > TILEMAP_SIZE_IN_8X8_COLUMNS / 2)
                {
                    throw new Exception(String.Format("Line {0} with number of columns greater than the maximum allowed ({1})", line, (TILEMAP_SIZE_IN_8X8_COLUMNS / 2)));
                }
                var initialSize = tileMap_16x16_Static[line].Count;
                for (int i = 0; i < (TILEMAP_SIZE_IN_8X8_COLUMNS / 2) - initialSize; i++)
                {
                    tileMap_16x16_Static[line].Add(0);
                }
            }

            // Loop all tilemap16x16 converting to 8x8 static
            //      ex. input:  { 0, 1, 0}
            //          output: { 0, 0, 9, 9, 0, 0}
            //                  { 0, 0,33,33, 0, 0}
            var tileMap_8x8_Animated = new List<List<int>>();
            for (int line = 0; line < tileMap_16x16_Static.Count; line++)
            {
                tileMap_8x8_Animated.Add(new List<int>());
                tileMap_8x8_Animated.Add(new List<int>());

                for (int column = 0; column < (TILEMAP_SIZE_IN_8X8_COLUMNS / 2); column++)
                {
                    if (tileMap_16x16_Static[line][column] == 0)
                    {
                        tileMap_8x8_Animated[line * 2].Add(0);
                        tileMap_8x8_Animated[line * 2].Add(0);
                        tileMap_8x8_Animated[(line * 2) + 1].Add(0);
                        tileMap_8x8_Animated[(line * 2) + 1].Add(0);
                    }
                    // small bricks
                    else if (tileMap_16x16_Static[line][column] == 1)
                    {
                        tileMap_8x8_Animated[line * 2].Add(9);
                        tileMap_8x8_Animated[line * 2].Add(9);
                        tileMap_8x8_Animated[(line * 2) + 1].Add(33);
                        tileMap_8x8_Animated[(line * 2) + 1].Add(33);
                    }
                    // big bricks
                    else if (tileMap_16x16_Static[line][column] == 2)
                    {
                        tileMap_8x8_Animated[line * 2].Add(57);
                        tileMap_8x8_Animated[line * 2].Add(65);
                        tileMap_8x8_Animated[(line * 2) + 1].Add(89);
                        tileMap_8x8_Animated[(line * 2) + 1].Add(97);
                    }
                }
            }

            // loop all tilemap8x8 static converting to 8x8 animated
            //      ex. input:  { 0, 0, 9, 9, 0, 0}
            //                  { 0, 0,33,33, 0, 0}
            //      ex. output: { 0, 1, 9,17, 0, 0}
            //                  { 0,25,33,41, 0, 0}
            for (int line = 0; line < tileMap_8x8_Animated.Count - 1; line += 2)
            {
                for (int column = 0; column < TILEMAP_SIZE_IN_8X8_COLUMNS - 1; column++)
                {
                    // Small bricks
                    if (tileMap_8x8_Animated[line][column] == 0 && tileMap_8x8_Animated[line][column + 1] == 9)
                    {
                        tileMap_8x8_Animated[line][column] =     1;
                        tileMap_8x8_Animated[line + 1][column] = 25;
                    }
                    else if (tileMap_8x8_Animated[line][column] == 9 && tileMap_8x8_Animated[line][column + 1] == 0)
                    {
                        tileMap_8x8_Animated[line][column] = 17;
                        tileMap_8x8_Animated[line + 1][column] = 41;
                    }

                    // Big bricks
                    if (tileMap_8x8_Animated[line][column] == 0 && tileMap_8x8_Animated[line][column + 1] == 57)
                    {
                        tileMap_8x8_Animated[line][column]          = 49;
                        tileMap_8x8_Animated[line][column + 1]      = 57;
                        tileMap_8x8_Animated[line + 1][column]      = 81;
                        tileMap_8x8_Animated[line + 1][column + 1]  = 89;
                    }
                    else if (tileMap_8x8_Animated[line][column] == 65 && tileMap_8x8_Animated[line][column + 1] == 57)
                    {
                        tileMap_8x8_Animated[line][column] = 73;
                        tileMap_8x8_Animated[line + 1][column] = 105;
                    }
                }
            }

            for (int line = 0; line < tileMap_8x8_Animated.Count; line++)
            {
                if (tileMap_8x8_Animated[line].Count != TILEMAP_SIZE_IN_8X8_COLUMNS)
                {
                    throw new Exception("; Line # " + line + ", count:" + tileMap_8x8_Animated[line].Count);
                }

                using (StreamWriter sw = new StreamWriter(String.Format(fileName, ((line / 4) + 1)), true))
                {
                    sw.WriteLine("; ---------------");
                    sw.WriteLine("; Line # " + line);

                    for (int frame = 0; frame < 8; frame++)
                    {
                        sw.Write("\tdb\t");
                        var first = true;
                        foreach (var item in tileMap_8x8_Animated[line])
                        {
                            if (!first)
                            {
                                sw.Write(", ");
                            }

                            if (item != 0)
                            {
                                sw.Write((item + frame).ToString());
                            }
                            else
                            {
                                sw.Write((item).ToString());
                            }

                            first = false;
                        }
                        sw.WriteLine();
                    }
                }
            }
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

            Tiles.Pacific2.Bg_Land.Load(out bg, out bgColors);
            Tiles.Pacific2.Rocks.Load(out input, out inputColors);

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
