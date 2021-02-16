using System;
using System.Collections.Generic;
using System.IO;

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

            //CreateTilesForPenguimPlatformer();
            CreateTileMapForPenguimPlatformer();

            Console.WriteLine("Done.");
            Console.ReadLine();
        }


        static void CreateTilesForPenguimPlatformer()
        {
            IList<string> bgPattern = new List<string>();
            IList<string> inputPattern_0 = new List<string>();
            IList<string> inputPattern_1 = new List<string>();

            IList<string> bgColor = new List<string>();
            IList<string> inputColor_0 = new List<string>();
            IList<string> inputColor_1 = new List<string>();

            var builder = new TilesForHorizontalScroll();



            Tiles.PenguimPlatformer.Bg_Black.Load(out bgPattern, out bgColor);
            Tiles.PenguimPlatformer.Bg_Bricks_Small.Load(out inputPattern_0, out inputColor_0, out inputPattern_1, out inputColor_1);

            Console.WriteLine(String.Format("; -------- Tile transitions from {0} to {1}", "Black", "Small brick - top"));
            builder.CreateTilesForScrolling(inputPattern_0, bgPattern);

            Console.WriteLine(String.Format("; -------- Tile transitions from {0} to {1}", "Small brick - top", "Small brick - top"));
            builder.CreateTilesForScrolling(inputPattern_1, inputPattern_0);

            Console.WriteLine(String.Format("; -------- Tile transitions from {0} to {1}", "Small brick - top", "Black"));
            builder.CreateTilesForScrolling(bgPattern, inputPattern_0);
        }

        static void CreateTileMapForPenguimPlatformer()
        {
            //var lastLine = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, }; // 32 chars
            
            // 128 chars
            //var lastLine = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, };

            // 128 chars
            var lastLine = new List<int> { 9, 9, 9, 9, 9, 17, 0, 1, 9, 9, 9, 9, 9, 9, 9, 9, 9, 17, 0, 0, 0, 1, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, };

            for (int i = 0; i < 8; i++)
            {
                Console.Write("\tdb\t");
                var first = true;
                foreach (var item in lastLine)
                {
                    if (!first)
                    {
                        Console.Write(", ");
                    }

                    if (item != 0)
                    {
                        Console.Write((item + i).ToString());
                    }
                    else
                    {
                        Console.Write((item).ToString());
                    }

                    first = false;
                }
                Console.WriteLine();
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
