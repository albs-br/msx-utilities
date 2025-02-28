using MSXUtilities.MsxWings;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

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
            //MsxDoom.Precalc_LUTs.CreateSinTable();
            //MsxDoom.Precalc_LUTs.CreateCosTable();
            //MsxDoom.Precalc_LUTs.CreatePowerOf2Table();
            //MsxDoom.Precalc_LUTs.CreateSquareRootTable();



            //SC11_Compressor.Method_2();

            var destinyFolder = @"C:\Users\albs_\source\repos\msx-tests\Images\tmp\";
            var mk = new MK.MK_Class(@"C:\Users\albs_\OneDrive\Desktop\MSX development\MK\scorpion-1.sc5");
            mk.Run(
                startX: 0, // x in bytes
                startY: 0, // y in pixels
                width: 28, // in bytes
                height: 104, // in pixels,
                megaROMpage: 1,
                name: destinyFolder + "scorpion_frame_0"
                );
            mk.Run(
                startX: 30, // x in bytes
                startY: 0, // y in pixels
                width: 28, // in bytes
                height: 104, // in pixels
                megaROMpage: 1,
                name: destinyFolder + "scorpion_frame_1"
                );
            mk.Run(
                startX: (130-12)/2, // x in bytes
                startY: 0, // y in pixels
                width: 28, // in bytes
                height: 104, // in pixels
                megaROMpage: 1,
                name: destinyFolder + "scorpion_frame_2"
                );
            mk.Run(
                startX: (188-12)/2, // x in bytes
                startY: 0, // y in pixels
                width: 28, // in bytes
                height: 104, // in pixels
                megaROMpage: 1,
                name: destinyFolder + "scorpion_frame_3"
                );
            mk.Run(
                startX: 0, // x in bytes
                startY: 110, // y in pixels
                width: 28, // in bytes
                height: 104, // in pixels
                megaROMpage: 1,
                name: destinyFolder + "scorpion_frame_4"
                );
            mk.Run(
                startX: (70-12)/2, // x in bytes
                startY: 110, // y in pixels
                width: 28, // in bytes
                height: 104, // in pixels
                megaROMpage: 1,
                name: destinyFolder + "scorpion_frame_5"
                );
            mk.Run(
                startX: (130 - 12) / 2, // x in bytes
                startY: 110, // y in pixels
                width: 28, // in bytes
                height: 104, // in pixels
                megaROMpage: 1,
                name: destinyFolder + "scorpion_frame_6"
                );


            //MSXUtilities.MsxWings.PlaneRotatingImg.SplitImage(0, 78, 0);
            //MSXUtilities.MsxWings.PlaneRotatingImg.SplitImage(85 - 12 + 1, 147, 13);
            //MSXUtilities.MsxWings.PlaneRotatingImg.SplitImage(172 - 20, 222, 27);
            //MSXUtilities.MsxWings.PlaneRotatingImg.SplitImage(228, 308, 39);
            //MSXUtilities.MsxWings.PlaneRotatingImg.List_PrepareSC5Image(0, 12);
            //MSXUtilities.MsxWings.PlaneRotatingImg.List_PrepareSC5Image(13, 26);
            //MSXUtilities.MsxWings.PlaneRotatingImg.List_PrepareSC5Image(27, 44);

            //// --------- MSX Wings "Stage clear" fonts for animation
            //string inputColors = @"
            //     db	0x05
            //     db	0x05
            //     db	0x0f
            //     db	0x0f
            //     db	0x09
            //     db	0x09
            //     db	0x0d
            //     db	0x0d
            //     db	0x04
            //     db	0x04
            //     db	0x0c
            //     db	0x0c
            //     db	0x08
            //     db	0x08
            //     db	0x0d
            //     db	0x0d
            //     ";
            ////char[] charArray = { 'S', 'T', 'A', 'G', 'E', 'C', 'L', 'R' }; // "STAGE CLEAR" string without repeated chars
            //char[] charArray = { 'M', 'O', 'V' }; // "GAME OVER" string without previous/repeated chars

            ////for (int factor = 2; factor <= 5; factor++)
            //int factor = 2;
            //{
            //    var outputColors = ExpandSprites_Class.ExpandSprites(null, inputColors, factor);
            //    var colors = outputColors.GetText_Colors();
            //    File.WriteAllText(String.Format("colors_factor_{0}.s", factor), colors);

            //    foreach (var item in charArray)
            //    {
            //        var inputPattern = MsxWings.FontsLarge.MsxWings_FontsLarge.GetCharPatternByNumber(item);

            //        var output = ExpandSprites_Class.ExpandSprites(inputPattern, inputColors, factor);

            //        var patterns = output.GetText_Pattern(item, factor);
            //        File.WriteAllText(String.Format("patterns_{0}_factor_{1}.s", item, factor), patterns);
            //    }
            //}
            //// ------------------------------------------------------

            // ------- SPRATR table for msx-wings 'STAGE CLEAR' animation
            //MsxWings.FontsLarge.MsxWings_FontsLarge.Create_SPRATR_Table();




            // ------------------------------------------------------

            //var fontImageFilePath = @"C:\Users\XDAD\OneDrive - PETROBRAS\Desktop\ATARIPL.png";
            //ConvertFontPngImageToAsmSource.Execute(fontImageFilePath);


            // ------------------------------------------------------


            //var command = "OUTI";
            //var number = 128;
            //var unrolledCommands = MakeUnrolledCommands(command, number);
            //return;

            // ------------------------------------------------------

            //PocMegaROM();

            //ConvertColorTable();

            //NameTableWithOffset();

            //CalcPositionsPseudo3DEffect();

            //CreateTilesForPacific2()




            //CreateTilesForGoPenguin();

            // Create Tilemap for Go Penguin
            //var tileMap_16x16_Static = MSXUtilities.GoPenguin.TileMaps.TestLevel_1.TileMap_TestLevel_1.LoadTileMap();
            //var tileMap_16x16_Static = MSXUtilities.GoPenguin.TileMaps.TestLevel_2.TileMap_TestLevel_2.LoadTileMap();
            //GoPenguin.TileMaps.CreateTileMap.Execute(tileMap_16x16_Static);

            // Import tilemap from Tiled Map Editor
            //var Tiled_TileMapFilePath = @"C:\Users\albs_\OneDrive\Desktop\MSX development\GoPenguin\tile map test..csv";
            //var baseLabel = "BgObjectsInitialState_TestLevel_2";
            //var tilemapConverted = GoPenguin.TileMaps.ImportTileMapFromTiled.Execute(Tiled_TileMapFilePath, baseLabel);
            //GoPenguin.TileMaps.CreateTileMap.Execute(tilemapConverted);



            //// Take two images (msx wings title and a base for palette cycling) and merge them
            //CreateGradientImage.Convert2ColorImageIntoImageForPaletteCycling();


            ////Convert bmp image into smaller images that will later be 16kb chunks to be used as background scroll on MSX 2
            ////var image = @"C:\Users\albs_\OneDrive\Desktop\MSX development\Aero Fighters 3 screen tests\AeroFighters2-Stage8-Tikal,Mexico.png";
            //var image = @"C:\Users\albs_\OneDrive\Desktop\MSX development\Aero Fighters 3 screen tests\msx-wings title screen 1.png";
            //var heightSc5 = (16 * 1024) / 128;  // 128 bytes per line (e.g. screen 5)
            ////var heightSc8 = (16 * 1024) / 256;  // 256 bytes per line (e.g. screen 8/11)
            ////SplitImageIn16KbChunks(image, heightSc8, "level8");
            //SplitImageIn16KbChunks(image, heightSc5, "title-screen");

            //// Remove 7 byte header from file and keep only 16kb
            ////var baseFileName = @"C:\Users\albs_\source\repos\msx-utilities\MSXUtilities\bin\Debug\netcoreapp3.1\level8_{0}.sra";
            //var baseFileName = @"C:\Users\albs_\source\repos\msx-utilities\MSXUtilities\bin\Debug\netcoreapp3.1\title-screen_{0}.sc5";
            //RemoveHeaderAndKeep16kbOfFiles(baseFileName, 7);

            //// Create .asm code for INCBIN the files
            //var levelNumber = "8";
            //var lastPage = 23;
            //var text =
            //            "\torg	0x8000, 0xBFFF" + Environment.NewLine +
            //            "\tINCBIN \"Graphics/Bitmaps/Level_" + levelNumber + "/level" + levelNumber + "_{0}.sra.new\"" + Environment.NewLine +
            //            "\tds PAGE_SIZE - ($ - 0x8000), 255" + Environment.NewLine + Environment.NewLine;
            //var output = RepeatText(text, lastPage);



            //ConvertSc5ImageToSprites_Jobs.PlayerAndEnemyPlanes();
            //ConvertSc5ImageToSprites_Jobs.EnemyChopper();



            //var filename = @"Msxmas21\conveyor belt 1.txt";
            //ConvertTinySpriteBkpToSc5Format(filename);

            //var filename = @"Msxmas21\window snow frame 1.txt";
            //ConvertTinySpriteBkpToSc5Format(filename);
            //filename = @"Msxmas21\window snow frame 2.txt";
            //ConvertTinySpriteBkpToSc5Format(filename);
            //filename = @"Msxmas21\window snow frame 3.txt";
            //ConvertTinySpriteBkpToSc5Format(filename);
            //filename = @"Msxmas21\window snow frame 4.txt";
            //ConvertTinySpriteBkpToSc5Format(filename);



            //var fileName = @"MsxWings\sonic wings font neo geo.png";
            //IList<ConvertNeoGeoSpritesToMsx2Sprites.ConversionParams> list = new List<ConvertNeoGeoSpritesToMsx2Sprites.ConversionParams>();

            //// 8x8 font
            //list.Add(new ConvertNeoGeoSpritesToMsx2Sprites.ConversionParams 
            //{
            //    xStart = 410,
            //    yStart = 90,
            //    numberOfChars = 18
            //});
            //list.Add(new ConvertNeoGeoSpritesToMsx2Sprites.ConversionParams
            //{
            //    xStart = 10,
            //    yStart = 174,
            //    numberOfChars = 23
            //});
            //list.Add(new ConvertNeoGeoSpritesToMsx2Sprites.ConversionParams
            //{
            //    xStart = 10,
            //    yStart = 258,
            //    numberOfChars = 23
            //});
            //list.Add(new ConvertNeoGeoSpritesToMsx2Sprites.ConversionParams
            //{
            //    xStart = 10,
            //    yStart = 342,
            //    numberOfChars = 23
            //});
            //list.Add(new ConvertNeoGeoSpritesToMsx2Sprites.ConversionParams
            //{
            //    xStart = 10,
            //    yStart = 426,
            //    numberOfChars = 12
            //});
            //ConvertNeoGeoSpritesToMsx2Sprites.BatchConversion(fileName, list);

            //// 8x16 font
            //list.Add(new ConvertNeoGeoSpritesToMsx2Sprites.ConversionParams
            //{
            //    xStart = 1770,
            //    yStart = 510,
            //    numberOfChars = 1
            //});
            //list.Add(new ConvertNeoGeoSpritesToMsx2Sprites.ConversionParams
            //{
            //    xStart = 10,
            //    yStart = 594,
            //    numberOfChars = 23
            //});
            //list.Add(new ConvertNeoGeoSpritesToMsx2Sprites.ConversionParams
            //{
            //    xStart = 10,
            //    yStart = 678,
            //    numberOfChars = 23
            //});
            //ConvertNeoGeoSpritesToMsx2Sprites.BatchConversion(fileName, list);

            //// 16x16 font
            //list.Add(new ConvertNeoGeoSpritesToMsx2Sprites.ConversionParams
            //{
            //    xStart = 10,
            //    yStart = 762,
            //    numberOfChars = 23
            //});
            //list.Add(new ConvertNeoGeoSpritesToMsx2Sprites.ConversionParams
            //{
            //    xStart = 10,
            //    yStart = 846,
            //    numberOfChars = 23
            //});
            //ConvertNeoGeoSpritesToMsx2Sprites.BatchConversion(fileName, list);



            //var spritePattern =
            //    //"$03,$5B,$AE,$7F,$FF,$6F,$FF,$BF," +
            //    //"$7F,$BF,$FF,$77,$BF,$77,$09,$56," +
            //    //"$5C,$FD,$F6,$FF,$DE,$FF,$FB,$FE," +
            //    //"$FF,$FF,$F6,$FF,$BA,$FE,$D4,$A0,";
            //    "$03,$0A,$2E,$3F,$5F,$6F,$FF,$BF," +
            //    "$7F,$BF,$FF,$37,$9F,$57,$09,$14," +
            //    "$54,$F8,$F4,$FA,$DC,$FE,$FB,$FE," +
            //    "$FF,$FE,$F6,$FC,$B2,$E8,$D0,$A0,";
            //ConvertTinySpriteToSc11CustomFormat(spritePattern);




            // // ---- Go Penguin
            //// Convert tiny sprite bkp data to tiles (4x4, 8x8, 16x16 or 32x32 size pixel)
            //var filename = @"GoPenguin\Bkps from TinySprite\Tiles - Penguin - Enemies.txt";
            ////const int SPRITE_ON_SLOT_0 = 3;
            //const int SPRITE_ON_SLOT_1 = 20;
            ////ConvertTinySpriteBkpToTiles_4x4(filename, SPRITE_ON_SLOT_1);
            ////ConvertTinySpriteBkpToTiles_8x8(filename, SPRITE_ON_SLOT_1);
            ////ConvertTinySpriteBkpToTiles_16x16(filename, SPRITE_ON_SLOT_1);
            //ConvertTinySpriteBkpToTiles_32x32(filename, SPRITE_ON_SLOT_1);



            //// -------------- Create look up table for circle sprites movement

            ////Point centerPoint = new Point(128, 96);
            ////double angle = 359; // angle in degrees (0-359); 0 is eastmost point
            ////int distance = 80;
            ////var result = PointInCircumference(centerPoint, angle, distance);

            ////Point centerPoint = new Point(128, 96 - 8); // middle of a 256x192 screen (subtract 8 from height to center a 16x16 sprite)
            //Point centerPoint = new Point(128 - ((7 * 16)/2) + 32, 96 - 8); // string 'LEVEL n' (7 chars long)

            //IList<Point> list = new List<Point>();

            //const int RADIUS_1 = 96 - 8 + 16;        // adjust for first position be -16 making the sprite sho progressively from top of screen
            //const int STEP_IN_DEGREES_1 = 5;
            //const int RADIUS_2 = 64;
            //const int STEP_IN_DEGREES_2 = 7;
            //const int RADIUS_3 = 32;
            //const int STEP_IN_DEGREES_3 = 10;

            //decimal radius = RADIUS_1;
            //for (int angle = 270; angle > 180; angle -= STEP_IN_DEGREES_1)
            //{
            //    list.Add(PointInCircumference(centerPoint, angle, (int)Math.Round(radius)));
            //    decimal radiusStep = (decimal)(RADIUS_1 - RADIUS_2) / ((270 - 180) / STEP_IN_DEGREES_1);
            //    radius -= radiusStep;
            //}
            //radius = RADIUS_2;
            //for (int angle = 180; angle > 0; angle -= STEP_IN_DEGREES_2)
            //{
            //    list.Add(PointInCircumference(centerPoint, angle, (int)Math.Round(radius)));
            //    //radius -= (RADIUS_2 - RADIUS_3) / ((180 - 0) / STEP_IN_DEGREES);
            //    decimal radiusStep = (decimal)(RADIUS_2 - RADIUS_3) / ((180 - 0) / STEP_IN_DEGREES_2);
            //    radius -= radiusStep;
            //}
            //radius = RADIUS_3;
            //for (int angle = 359; angle > 180; angle -= STEP_IN_DEGREES_3)
            //{
            //    list.Add(PointInCircumference(centerPoint, angle, (int)Math.Round(radius)));
            //}

            //foreach (var item in list)
            //{
            //    //Console.WriteLine(String.Format("Center: ({0}, {1}); Point: ({2}, {3})", centerPoint.X, centerPoint.Y, item.X, item.Y));
            //    Console.WriteLine(String.Format("\tdb\t {1}, {0}", item.X, item.Y));
            //}



            //var fileNameSrc = @"test.bmp";
            //var fileNameDest = @"test_new.bmp";
            //int lineToBeDuplicated = 4;
            //DuplicateLine_Class.DuplicateLine(fileNameSrc, lineToBeDuplicated, fileNameDest);



            Console.WriteLine("Done.");
            Console.ReadLine();
        }

        public static Point PointInCircumference(Point centerPoint, double angle, int radius)
        {
            var result = new Point(0, 0);
            result.Y = centerPoint.Y + (int)Math.Round(radius * Math.Sin(angle * (Math.PI / 180)));
            result.X = centerPoint.X + (int)Math.Round(radius * Math.Cos(angle * (Math.PI / 180)));

            //if (result.Y < 0) result.Y = 0;
            //if (result.X < 0) result.X = 0;

            return result;
        }

        private static void ConvertTinySpriteBkpToTiles_4x4(string filename, int startLine)
        {
            IList<string> lines = GetSpriteLines(filename, startLine);

            //string[] fileLines = File.ReadAllLines(filename);

            //IList<string> lines = new List<string>();

            //// get the sprite pattern required
            //int counter = 0;
            //const string COLOR_TO_REPLACE_TRANPARENT = "1";
            //foreach (string line in fileLines)
            //{
            //    if (counter >= startLine && counter < (startLine+16))
            //    {
            //        lines.Add(line.Replace(".", COLOR_TO_REPLACE_TRANPARENT));
            //    }

            //    counter++;
            //}

            //if (lines.Count != 16) throw new ArgumentException("Sprite should have 16 lines.");

            List<string> patternTable = new List<string>();
            List<string> colorTable = new List<string>();

            int counter = 0;
            for (int lineNumber = 0; lineNumber < 16; lineNumber += 2)
            {
                for (int i = 0; i < 16; i += 2)
                {
                    colorTable.Add("; --------- colors for pattern #" + (counter + 1));

                    var frontColorUp = lines[lineNumber][i];
                    var backColorUp = lines[lineNumber][i + 1];

                    // 4x
                    colorTable.Add("\tdb\t 0x" + frontColorUp + backColorUp);
                    colorTable.Add("\tdb\t 0x" + frontColorUp + backColorUp);
                    colorTable.Add("\tdb\t 0x" + frontColorUp + backColorUp);
                    colorTable.Add("\tdb\t 0x" + frontColorUp + backColorUp);


                    var frontColorDown = lines[lineNumber + 1][i];
                    var backColorDown = lines[lineNumber + 1][i + 1];

                    // 4x
                    colorTable.Add("\tdb\t 0x" + frontColorDown + backColorDown);
                    colorTable.Add("\tdb\t 0x" + frontColorDown + backColorDown);
                    colorTable.Add("\tdb\t 0x" + frontColorDown + backColorDown);
                    colorTable.Add("\tdb\t 0x" + frontColorDown + backColorDown);

                    colorTable.Add(""); // empty line



                    patternTable.Add("; --------- pattern #" + (counter + 1));

                    // 8x
                    patternTable.Add("\tdb\t 1111 0000 b");
                    patternTable.Add("\tdb\t 1111 0000 b");
                    patternTable.Add("\tdb\t 1111 0000 b");
                    patternTable.Add("\tdb\t 1111 0000 b");
                    patternTable.Add("\tdb\t 1111 0000 b");
                    patternTable.Add("\tdb\t 1111 0000 b");
                    patternTable.Add("\tdb\t 1111 0000 b");
                    patternTable.Add("\tdb\t 1111 0000 b");

                    patternTable.Add(""); // empty line

                    counter++;
                }
            }

            // TODO: remove duplicates

            File.WriteAllLines("color_table.s", colorTable);

            File.WriteAllLines("pattern_table.s", patternTable);


            // ------------- Names table
            var namesTable = new StringBuilder("\tdb\t");
            const int NUMBER_OF_TILES = 8 * 8;
            for (int i = 1; i <= NUMBER_OF_TILES; i++)
            {
                if(i > 1) namesTable.Append(", ");

                namesTable.Append(i);
            }
            File.WriteAllText("names_table.s", namesTable.ToString());
        }
        
        private static void ConvertTinySpriteBkpToTiles_8x8(string filename, int startLine)
        {
            IList<string> lines = GetSpriteLines(filename, startLine);

            Create_15_Tiles_Square_Filled();

            // ------------- Names table
            var namesTable = new StringBuilder("\tdb\t");
            int counter = 0;
            for (int lineNumber = 0; lineNumber < 16; lineNumber++)
            {
                for (int i = 0; i < 16; i++)
                {
                    if (counter > 0) namesTable.Append(", ");

                    string hexValue = lines[lineNumber][i].ToString();
                    namesTable.Append(int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber));

                    counter++;
                }
            }
            File.WriteAllText("names_table.s", namesTable.ToString());
        }

        private static void ConvertTinySpriteBkpToTiles_16x16(string filename, int startLine)
        {
            IList<string> lines = GetSpriteLines(filename, startLine);

            Create_15_Tiles_Square_Filled();

            // ------------- Names table
            var namesTable = new StringBuilder("\tdb\t");
            int counter = 0;
            for (int lineNumber = 2; lineNumber < 14; lineNumber++) // only the 12 central lines of the sprite will be used (lines 2 to 13)
            {
                IList<string> tempLine = new List<string>();

                if (counter > 0) namesTable.Append(", ");

                // loop through line
                for (int i = 0; i < 16; i++)
                {
                    string hexValue = lines[lineNumber][i].ToString();

                    // x2
                    tempLine.Add(" " + int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber).ToString());
                    tempLine.Add(" " + int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber).ToString());

                    counter++;
                }

                // x2
                var tempLineConcat = tempLine.Concat(tempLine);

                namesTable.Append(String.Join(",", tempLineConcat.ToArray()));
            }
            File.WriteAllText("names_table.s", namesTable.ToString());
        }

        private static void ConvertTinySpriteBkpToTiles_32x32(string filename, int startLine)
        {
            IList<string> lines = GetSpriteLines(filename, startLine);

            Create_15_Tiles_Square_Filled();

            // ------------- Names table
            var namesTable = new StringBuilder("\tdb\t");
            int counter = 0;
            for (int lineNumber = 5; lineNumber < 11; lineNumber++) // only the 6 central lines of the sprite will be used (lines 5 to 10)
            {
                IList<string> tempLine = new List<string>();

                if (counter > 0) namesTable.Append(", ");

                // loop through line
                for (int i = 4; i < 12; i++) // only the 8 central columns of the sprite will be used (columns 4 to 11)
                {
                    string hexValue = lines[lineNumber][i].ToString();

                    // x4
                    tempLine.Add(" " + int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber).ToString());
                    tempLine.Add(" " + int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber).ToString());
                    tempLine.Add(" " + int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber).ToString());
                    tempLine.Add(" " + int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber).ToString());

                    counter++;
                }

                // x4
                var tempLineConcat = tempLine.Concat(tempLine);
                tempLineConcat = tempLineConcat.Concat(tempLine);
                tempLineConcat = tempLineConcat.Concat(tempLine);

                //if (tempLineConcat.Count() != 768) throw new Exception("Names table should have 768 values.");

                namesTable.Append(String.Join(",", tempLineConcat.ToArray()));
            }
            File.WriteAllText("names_table.s", namesTable.ToString());
        }

        private static void Create_15_Tiles_Square_Filled()
        {
            // Create the 15 patterns required (one full 8x8 square for each color)
            List<string> patternTable = new List<string>();
            List<string> colorTable = new List<string>();
            for (int color = 1; color < 16; color++)
            {
                patternTable.Add("; --------- pattern #" + color);

                colorTable.Add("; --------- colors for pattern #" + color);

                for (int i = 0; i < 8; i++)
                {
                    patternTable.Add("\tdb\t 1111 1111 b");

                    colorTable.Add("\tdb\t 0x" + color.ToString("X") + color.ToString("X"));
                }

                patternTable.Add(""); // empty line

                colorTable.Add(""); // empty line
            }
            File.WriteAllLines("pattern_table.s", patternTable);
            File.WriteAllLines("color_table.s", colorTable);
        }

        private static IList<string> GetSpriteLines(string filename, int startLine)
        {
            string[] fileLines = File.ReadAllLines(filename);

            IList<string> lines = new List<string>();

            // get the sprite pattern required
            int currentLine = 0;
            const string COLOR_TO_REPLACE_TRANPARENT = "1";
            foreach (string line in fileLines)
            {
                if (currentLine >= startLine && currentLine < (startLine + 16))
                {
                    lines.Add(line.Replace(".", COLOR_TO_REPLACE_TRANPARENT));
                }

                currentLine++;
            }

            if (lines.Count != 16) throw new ArgumentException("Sprite should have 16 lines.");

            return lines;
        }

        private static void ConvertTinySpriteToSc11CustomFormat(string input)
        {
            const string PIXEL_COLOR = "6"; // "1";
            var bytes = input.Split(',');
            IList<string> output = new List<string>();
            for (int i = 0; i < 16; i++)
            {
                var tempHex = bytes[i].Replace("$", "");
                var binaryString = Convert.ToString(Convert.ToInt32(tempHex, 16), 2).PadLeft(8, '0');
                for (int j = 0; j < 8; j++)
                {
                    if (binaryString.Substring(j, 1) == "0")
                    {
                        output.Add("0");
                    }
                    else
                    {
                        output.Add("0x" + PIXEL_COLOR + "8");
                    }
                }
                tempHex = bytes[i + 16].Replace("$", "");
                binaryString = Convert.ToString(Convert.ToInt32(tempHex, 16), 2).PadLeft(8, '0');
                for (int j = 0; j < 8; j++)
                {
                    if (binaryString.Substring(j, 1) == "0")
                    {
                        output.Add("0");
                    }
                    else
                    {
                        output.Add("0x" + PIXEL_COLOR + "8");
                    }
                }
            }
            
            Console.WriteLine("\tdb\t" + String.Join(", ", output));
        }

        private static string RepeatText(string text, int max)
        {
            var output = "";
            for (int i = 0; i <= max; i++)
            {
                output += String.Format(text, i);
            }

            return output;
        }


        private static void ConvertTinySpriteBkpToSc5Format(string filename)
        {
            var file = File.ReadAllText(filename);

            var lines = file.Split(Environment.NewLine).ToList();
            var bytesOutput = new List<byte>();

            foreach (var line in lines)
            {
                for (int i = 0; i < line.Length; i+=2)
                {
                    var inputCharEven = (line[i] == '.') ? '0' : line[i];
                    var inputCharOdd = (line[i+1] == '.') ? '0' : line[i+1];

                    var byteInputHighNibble = Byte.Parse(inputCharEven.ToString(), NumberStyles.HexNumber);
                    var byteInputLowNibble = Byte.Parse(inputCharOdd.ToString(), NumberStyles.HexNumber);

                    var byteOutput = Convert.ToByte((byteInputHighNibble << 4) + byteInputLowNibble);
                    bytesOutput.Add(byteOutput);
                }
            }

            var outputFilename = filename + ".bin";

            File.WriteAllBytes(outputFilename, bytesOutput.ToArray());
        }

        /// <summary>
        /// Remove the 7-byte header of file and keep only
        /// the following 16 kb, in order to fit one MegaROM page
        /// </summary>
        /// <param name="baseFileName"></param>
        /// <param name="skipBytes"></param>
        private static void RemoveHeaderAndKeep16kbOfFiles(string baseFileName, int skipBytes)
        {
            const int HEADER_SIZE = 7; // 7 bytes header
            for (int i = 0; i < 100; i++)
            {
                var fileName = String.Format(baseFileName, i);
                if (!File.Exists(fileName)) throw new Exception("File " + fileName + " not found.");

                using (var input = File.OpenRead(fileName))
                using (var reader = new BinaryReader(input))
                using (var output = File.Create(fileName + ".new"))
                {
                    for (int j = 0; j < HEADER_SIZE; j++)
                    {
                        reader.ReadByte();
                    }

                    var buffer = new byte[4096 * 4]; // 16 kb page

                    // only one page
                    var actual = reader.Read(buffer, 0, buffer.Length);
                    output.Write(buffer, 0, actual);
                }
            }
        }


        /// <summary>
        /// Split image file in BMP format in smaller images that, when converted to MSX formats (.sc5, .sc8)
        /// will give 16 kb chunks to fit a Mega ROM page
        /// </summary>
        /// <param name="imageSourcePath"></param>
        /// <param name="height"></param>
        /// <param name="destinyBaseFilename"></param>
        private static void SplitImageIn16KbChunks(string imageSourcePath, int height, string destinyBaseFilename)
        {
            using (Bitmap bmpSource = new Bitmap(imageSourcePath))
            {
                if (bmpSource.Width < 256)
                {
                    throw new InvalidDataException("Source image should be at least 256 pixels wide");
                }

                int? lastImageNumber = null;
                Bitmap bmpDestiny = null;
                int yDest = 0;
                for (int y = 0; y < bmpSource.Height; y++)
                {
                    var currentImageNumber = ((int)(y / height));

                    if (lastImageNumber == null || lastImageNumber != currentImageNumber)
                    {
                        if (bmpDestiny != null)
                        {
                            bmpDestiny.Save(destinyBaseFilename + "_" + lastImageNumber + ".bmp", ImageFormat.Bmp);
                        }

                        lastImageNumber = currentImageNumber;
                        yDest = 0;

                        //height = ((bmpSource.Height - (currentImageNumber * height)) < height) ? (bmpSource.Height - (currentImageNumber * height)) : height;

                        bmpDestiny = new Bitmap(256, height);
                    }

                    for (int x = 0; x < 256; x++)
                    {
                        bmpDestiny.SetPixel(x, yDest, bmpSource.GetPixel(x, y));
                    }

                    yDest++;
                }
                bmpDestiny.Save(destinyBaseFilename + "_" + lastImageNumber + ".bmp", ImageFormat.Bmp);
            }
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



            // ------ Enemy Ladybug
            IList<string> pattern_EnemyLadybug_Left_0 = new List<string>();
            IList<string> pattern_EnemyLadybug_Left_1 = new List<string>();
            IList<string> pattern_EnemyLadybug_Left_2 = new List<string>();
            IList<string> pattern_EnemyLadybug_Left_3 = new List<string>();

            IList<string> pattern_EnemyLadybug_Right_0 = new List<string>();
            IList<string> pattern_EnemyLadybug_Right_1 = new List<string>();
            IList<string> pattern_EnemyLadybug_Right_2 = new List<string>();
            IList<string> pattern_EnemyLadybug_Right_3 = new List<string>();

            // Colors are the same for both left and right positions of the character
            IList<string> color_EnemyLadybug_0 = new List<string>();
            IList<string> color_EnemyLadybug_1 = new List<string>();
            IList<string> color_EnemyLadybug_2 = new List<string>();
            IList<string> color_EnemyLadybug_3 = new List<string>();



            // ------ Enemy Snail
            IList<string> pattern_EnemySnail_Left_0 = new List<string>();
            IList<string> pattern_EnemySnail_Left_1 = new List<string>();
            IList<string> pattern_EnemySnail_Left_2 = new List<string>();
            IList<string> pattern_EnemySnail_Left_3 = new List<string>();

            IList<string> pattern_EnemySnail_Right_0 = new List<string>();
            IList<string> pattern_EnemySnail_Right_1 = new List<string>();
            IList<string> pattern_EnemySnail_Right_2 = new List<string>();
            IList<string> pattern_EnemySnail_Right_3 = new List<string>();

            // Colors are the same for both left and right positions of the character
            IList<string> color_EnemySnail_0 = new List<string>();
            IList<string> color_EnemySnail_1 = new List<string>();
            IList<string> color_EnemySnail_2 = new List<string>();
            IList<string> color_EnemySnail_3 = new List<string>();


            // old code:
            GoPenguin.Tiles.Bg_Black.Load(out patternBgBlack, out colorBgBlack);
            GoPenguin.Tiles.Bg_Bricks_Small.Load(out pattern_SmallBricks_0, out color_SmallBricks_0, out pattern_SmallBricks_1, out color_SmallBricks_1, out pattern_SmallBricks_2, out color_SmallBricks_2, out pattern_SmallBricks_3, out color_SmallBricks_3);

            GoPenguin.Tiles.Bg_Bricks_Big.LoadFromTinySpriteBackup(out pattern_BigBricks_0, out color_BigBricks_0, out pattern_BigBricks_1, out color_BigBricks_1, out pattern_BigBricks_2, out color_BigBricks_2, out pattern_BigBricks_3, out color_BigBricks_3);
            GoPenguin.Tiles.Bg_Grass.LoadFromTinySpriteBackup(out pattern_Grass_0, out color_Grass_0, out pattern_Grass_1, out color_Grass_1, out pattern_Grass_2, out color_Grass_2, out pattern_Grass_3, out color_Grass_3);
            GoPenguin.Tiles.Bg_Rocks.LoadFromTinySpriteBackup(out pattern_Rocks_0, out color_Rocks_0, out pattern_Rocks_1, out color_Rocks_1, out pattern_Rocks_2, out color_Rocks_2, out pattern_Rocks_3, out color_Rocks_3);
            GoPenguin.Tiles.Bg_Diamond.LoadFromTinySpriteBackup(out pattern_Diamond_0, out color_Diamond_0, out pattern_Diamond_1, out color_Diamond_1, out pattern_Diamond_2, out color_Diamond_2, out pattern_Diamond_3, out color_Diamond_3);
            GoPenguin.Tiles.Bg_TestSquare.LoadFromTinySpriteBackup(out pattern_TestSquare_0, out color_TestSquare_0, out pattern_TestSquare_1, out color_TestSquare_1, out pattern_TestSquare_2, out color_TestSquare_2, out pattern_TestSquare_3, out color_TestSquare_3);

            GoPenguin.Tiles.Enemy_Ladybug_Left.LoadFromTinySpriteBackup(out pattern_EnemyLadybug_Left_0, out color_EnemyLadybug_0, out pattern_EnemyLadybug_Left_1, out color_EnemyLadybug_1, out pattern_EnemyLadybug_Left_2, out color_EnemyLadybug_2, out pattern_EnemyLadybug_Left_3, out color_EnemyLadybug_3);
            GoPenguin.Tiles.Enemy_Ladybug_Right.LoadFromTinySpriteBackup(out pattern_EnemyLadybug_Right_0, out color_EnemyLadybug_0, out pattern_EnemyLadybug_Right_1, out color_EnemyLadybug_1, out pattern_EnemyLadybug_Right_2, out color_EnemyLadybug_2, out pattern_EnemyLadybug_Right_3, out color_EnemyLadybug_3);

            GoPenguin.Tiles.Enemy_Snail_Left.LoadFromTinySpriteBackup(out pattern_EnemySnail_Left_0, out color_EnemySnail_0, out pattern_EnemySnail_Left_1, out color_EnemySnail_1, out pattern_EnemySnail_Left_2, out color_EnemySnail_2, out pattern_EnemySnail_Left_3, out color_EnemySnail_3);
            GoPenguin.Tiles.Enemy_Snail_Right.LoadFromTinySpriteBackup(out pattern_EnemySnail_Right_0, out color_EnemySnail_0, out pattern_EnemySnail_Right_1, out color_EnemySnail_1, out pattern_EnemySnail_Right_2, out color_EnemySnail_2, out pattern_EnemySnail_Right_3, out color_EnemySnail_3);



            // Save a .png file for each tile (will be used on Tiled map editor)
            SaveTilePngImage("Bricks small", pattern_SmallBricks_0, color_SmallBricks_0, pattern_SmallBricks_1, color_SmallBricks_1, pattern_SmallBricks_2, color_SmallBricks_2, pattern_SmallBricks_3, color_SmallBricks_3);
            SaveTilePngImage("Bricks big", pattern_BigBricks_0, color_BigBricks_0, pattern_BigBricks_1, color_BigBricks_1, pattern_BigBricks_2, color_BigBricks_2, pattern_BigBricks_3, color_BigBricks_3);
            SaveTilePngImage("Grass", pattern_Grass_0, color_Grass_0, pattern_Grass_1, color_Grass_1, pattern_Grass_2, color_Grass_2, pattern_Grass_3, color_Grass_3);
            SaveTilePngImage("Rocks", pattern_Rocks_0, color_Rocks_0, pattern_Rocks_1, color_Rocks_1, pattern_Rocks_2, color_Rocks_2, pattern_Rocks_3, color_Rocks_3);
            SaveTilePngImage("Diamond", pattern_Diamond_0, color_Diamond_0, pattern_Diamond_1, color_Diamond_1, pattern_Diamond_2, color_Diamond_2, pattern_Diamond_3, color_Diamond_3);


            // TODO: Fix these numbers, all of them are wrong, as the small bricks now are 24 tiles, not 48

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
                pattern_EnemyLadybug_Left_0, pattern_EnemyLadybug_Left_1, pattern_EnemyLadybug_Left_2, pattern_EnemyLadybug_Left_3,
                color_EnemyLadybug_0, color_EnemyLadybug_1, color_EnemyLadybug_2, color_EnemyLadybug_3,
                "EnemyLadybug_Left");

            builder.CreateCompleteSetOfTilesForScrolling_Enemy(patternBgBlack,
                pattern_EnemyLadybug_Right_0, pattern_EnemyLadybug_Right_1, pattern_EnemyLadybug_Right_2, pattern_EnemyLadybug_Right_3,
                color_EnemyLadybug_0, color_EnemyLadybug_1, color_EnemyLadybug_2, color_EnemyLadybug_3,
                "EnemyLadybug_Right");

            builder.CreateCompleteSetOfTilesForScrolling_Enemy(patternBgBlack,
                pattern_EnemySnail_Left_0, pattern_EnemySnail_Left_1, pattern_EnemySnail_Left_2, pattern_EnemySnail_Left_3,
                color_EnemySnail_0, color_EnemySnail_1, color_EnemySnail_2, color_EnemySnail_3,
                "EnemySnail_Left");

            builder.CreateCompleteSetOfTilesForScrolling_Enemy(patternBgBlack,
                pattern_EnemySnail_Right_0, pattern_EnemySnail_Right_1, pattern_EnemySnail_Right_2, pattern_EnemySnail_Right_3,
                color_EnemySnail_0, color_EnemySnail_1, color_EnemySnail_2, color_EnemySnail_3,
                "EnemySnail_Right");

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

        private static void SaveTilePngImage(string fileName,
            IList<string> pattern_0, IList<string> color_0, IList<string> pattern_1, IList<string> color_1,
            IList<string> pattern_2, IList<string> color_2, IList<string> pattern_3, IList<string> color_3)
        {
            using (Bitmap bmp = new Bitmap(16, 16))
            {
                using (Graphics grp = Graphics.FromImage(bmp))
                {
                    grp.Clear(Color.Black);
                }

                for (int y = 0; y < 16; y++)
                {
                    var patternLine = "";
                    var colorLine = "";
                    if (y < 8)
                    {
                        patternLine = pattern_0[y] + pattern_1[y];
                        colorLine = color_0[y];
                    }
                    else
                    {
                        patternLine = pattern_2[y - 8] + pattern_3[y - 8];
                        colorLine = color_2[y - 8];
                    }
                    colorLine = colorLine.Replace("0x", "");

                    for (int x = 0; x < 16; x++)
                    {
                        var bit = patternLine.Substring(x, 1);
                        var hexStr = "";
                        if (bit == "1") // foreground color
                        {
                            hexStr = colorLine.Substring(0, 1);
                        }
                        else // background color
                        {
                            hexStr = colorLine.Substring(1, 1);
                        }

                        var colorValue = Convert.ToInt32(hexStr, 16);
                        var color = ConvertMsxColorValueToColor(colorValue);
                        bmp.SetPixel(x, y, color);
                    }
                }

                bmp.Save(fileName + ".png", ImageFormat.Png);
            }
        }

        static Color ConvertMsxColorValueToColor(int colorValue)
        {
            switch (colorValue)
            {
                case 0:
                    return Color.FromArgb(0, 0, 0);
                case 1:
                    return Color.FromArgb(1, 1, 1);
                case 2:
                    return Color.FromArgb(62, 184, 73);
                case 3:
                    return Color.FromArgb(116, 208, 125);
                case 4:
                    return Color.FromArgb(89, 85, 224);
                case 5:
                    return Color.FromArgb(128, 118, 241);
                case 6:
                    return Color.FromArgb(185, 94, 81);
                case 7:
                    return Color.FromArgb(101, 219, 239);
                case 8:
                    return Color.FromArgb(219, 101, 89);
                case 9:
                    return Color.FromArgb(255, 137, 125);
                case 10:
                    return Color.FromArgb(204, 195, 94);
                case 11:
                    return Color.FromArgb(222, 208, 135);
                case 12:
                    return Color.FromArgb(58, 162, 65);
                case 13:
                    return Color.FromArgb(183, 102, 181);
                case 14:
                    return Color.FromArgb(204, 204, 204);
                case 15:
                    return Color.FromArgb(255, 255, 255);
                default:
                    return Color.Black;
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
            var text = "\torg	8000h,0BFFFh	; page {0}" + "\n\r" +
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
