using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MSXUtilities.MsxSliver
{



    public class MsxSliver_Class
    {
        public static void CreatePrecalcData()
        {
            //const string preCalcDataFilePath = @"PreCalcData.s";

            /*
            
            QIII | QIY
            -----+-----
            QII  | QI

            Quadrant 	x-coordinate	y-coordinate
            I (QI)	    Positive (+)	Positive (+)
            II (QII)	Negative (-)	Positive (+)
            III (QIII)	Negative (-)	Negative (-)
            IV (QIV)	Positive (+)	Negative (-)

            */



            const int MAP_WIDTH = 64;
            //const int MAP_HEIGHT = 64;

            const int TILE_SIZE = 16;  // Size of each tile in units, in both width and height
            
            const double RAY_STEP = ((double)TILE_SIZE) / 1000;

            const int DISTANCE_PLAYER_TO_SCREEN = 16;

            const int MAX_TILES = 20;

            const double RAY_MAX_DISTANCE = 240; // max ray distance for angle = 44 (all angles max distance is 320)

            const double MAX_DISTANCE_ADJUSTED = (TILE_SIZE * DISTANCE_PLAYER_TO_SCREEN) / RAY_STEP;

            const int MEGAROM_PAGE_SIZE = 16 * 1024;
            int counter = 0;

            const int FIRST_MEGAROM_PAGE = 2;

            const int BLOCK_SIZE = 64; // 64 bytes per block

            const int ANGLE_STEP = 2; // Angle step in degrees

            double maxDistance = 0; // debug



            var sb = new StringBuilder();

            for (int y = 0; y < TILE_SIZE; y++)
            //int y = 0;
            {
                for (int x = 0; x < TILE_SIZE; x++)
                //int x = 0;
                {
                    for (double angle = 0; angle < 360; angle += ANGLE_STEP)
                    //double angle = 180 + 30;
                    //double angle = 180 - 30;
                    //double angle = 360 - 30;
                    //double angle = 44;
                    {
                        if ((counter % 256) == 0)
                        {
                            int megaromPageNumber = ((counter * BLOCK_SIZE) / MEGAROM_PAGE_SIZE) + FIRST_MEGAROM_PAGE;
                            sb.AppendLine("; ---------------------------- MegaROM Page " + megaromPageNumber);
                            sb.AppendLine("\torg	0x8000, 0xBFFF");
                            sb.AppendLine();
                            sb.AppendLine();
                        }
                        //Console.WriteLine("Counter: " + counter_1);
                        //Console.WriteLine("Is new megarom page: " + ((counter_1 % 256) == 0));
                        //Console.WriteLine("Megarom page: " + ((counter_1 * BLOCK_SIZE) / (MEGAROM_PAGE_SIZE)));


                        sb.AppendLine($"; ---- Data for position ({x}, {y}), angle {angle}");
                        sb.AppendLine();

                        double angle_rad = Helpers.MathHelpers.DegreesToRadians(angle);


                        double step_X = Math.Cos(angle_rad) * RAY_STEP;
                        double step_Y = Math.Sin(angle_rad) * RAY_STEP;


                        double ray_X = x;
                        double ray_Y = y;
                        
                        //while (ray_X >= 0 && ray_X < TILE_SIZE && ray_Y >= 0 && ray_Y < TILE_SIZE) // check tile bounds
                        //{
                        //    ray_X += step_X;
                        //    ray_Y += step_Y;
                        //}


                        //double side_X = ray_X - x;
                        //double side_Y = ray_Y - y;

                        //// calc hypotenuse
                        //double dist = Math.Sqrt(Math.Pow(side_X, 2) + Math.Pow(side_Y, 2));


                        ////// adjust (project wall into an screen in front of player)
                        ////dist = (TILE_SIZE * DISTANCE_PLAYER_TO_SCREEN) / dist;


                        //// normalize dist to range 0-59
                        //dist = (dist * 59) / RAY_MAX_DISTANCE;
                        ////dist = (dist * 59) / MAX_DISTANCE_ADJUSTED;

                        //string addr = $"FixFishEyeTable + ({Math.Round(dist)} * 64)";


                        //int converted = ConvertToFixedPoint_8_8(dist); // convert to fixed point 8.8

                        //string firstDistance = $"\tdw\t{addr}\t; distance to edge of first tile (fixed point 8.8), decimal value: {dist}";




                        //double rayOrigin_X = (x * ((double)TILE_SIZE / 1000));
                        //double rayOrigin_Y = (y * ((double)TILE_SIZE / 1000));


                        //// save position of ray at edge of tile
                        //double ray_edge_X = rayOrigin_X + ray_X;
                        //double ray_edge_Y = rayOrigin_Y + ray_Y;

                        // ------ first tile


                        // calc tile relative to tile origin
                        //int tile_X = (int)Math.Floor(ray_X / (TILE_SIZE));
                        //int tile_Y = (int)Math.Floor(ray_Y / (TILE_SIZE));

                        //int tileLinear = (tile_Y * MAP_WIDTH) + tile_X;

                        //Console.WriteLine($"\tdb\t{tileLinear}\t; tile X: {tile_X}, Y: {tile_Y}");

                        //if (tileLinear == 0)
                        //{
                        //    Console.WriteLine("[ERROR] first tile delta = 0");
                        //}

                        // ----------------------------------- 

                        IList<int> tilesTouched = new List<int>();
                        //tilesTouched.Add(tileLinear); // first tile
                        int tilesNumber = 0;

                        IList<double> distances = new List<double>();
                        IList<double> realDistances = new List<double>();
                        IList<double> distancesNormalized = new List<double>();

                        // calc tile linear of origin
                        int originTile_X = (int)Math.Floor((double)x / (TILE_SIZE));
                        int originTile_Y = (int)Math.Floor((double)y / (TILE_SIZE));

                        int originTileLinear = (originTile_Y * MAP_WIDTH) + originTile_X;

                        while (tilesNumber < MAX_TILES)
                        {
                            ray_X += step_X;
                            ray_Y += step_Y;

                            // calc tile relative to tile origin
                            int tile_X = (int)Math.Floor(ray_X / (TILE_SIZE));
                            int tile_Y = (int)Math.Floor(ray_Y / (TILE_SIZE));

                            int tileLinear = (tile_Y * MAP_WIDTH) + tile_X;

                            if ((tilesTouched.Count == 0 && tileLinear != originTileLinear ) || (tilesTouched.Count > 0 && tileLinear != tilesTouched.Last()))
                            {
                                // new tile, add it
                                tilesTouched.Add(tileLinear);
                                tilesNumber++;

                                double side_X = ray_X - x;
                                double side_Y = ray_Y - y;

                                // calc hypotenuse
                                double dist = Math.Sqrt(Math.Pow(side_X, 2) + Math.Pow(side_Y, 2));

                                realDistances.Add(dist);

                                distances.Add(dist);



                                if (tilesNumber == MAX_TILES)
                                {
                                    // max tile x and y
                                    sb.AppendLine($"\tdb\t{tile_X},\t{tile_Y} ; Max tiles: x: {tile_X}, y: {tile_Y}");
                                    sb.AppendLine();
                                }
                            }

                        }

                        IList<int> tilesTouchedDeltas = new List<int>();
                        tilesTouchedDeltas.Add(tilesTouched.First());
                        for (int i = 1; i < tilesTouched.Count; i++)
                        {
                            tilesTouchedDeltas.Add(tilesTouched[i] - tilesTouched[i - 1]);
                        }

                        int[] validValues = { -65, -64, -63, 1, 65, 64, 63, -1 };
                        foreach (var item in tilesTouchedDeltas)
                        {
                            if (!validValues.Contains(item))
                            {
                                Console.WriteLine("[ERROR] Invalid tile delta: " + item);
                            }
                        }

                        
                        
                        foreach (var d in distances)
                        {
                            if (d > maxDistance) maxDistance = d; // debug
                        }

                        // cap max distance and normalize it
                        for (int i = 0; i < MAX_TILES; i++)
                        {
                            if (distances[i] > RAY_MAX_DISTANCE)
                            {
                                tilesTouchedDeltas[i] = 0;
                                distances[i] = RAY_MAX_DISTANCE;
                            }


                            //// adjust (project wall into an screen in front of player)
                            //distances[i] = (TILE_SIZE * DISTANCE_PLAYER_TO_SCREEN) / distances[i];


                            // normalize dist to range 0-59
                            double distNormalized = (distances[i] * 59) / RAY_MAX_DISTANCE;
                            //double distNormalized = (distances[i] * 59) / MAX_DISTANCE_ADJUSTED;

                            distancesNormalized.Add(distNormalized);
                        }




                        sb.AppendLine($"\t; Tiles touched deltas");
                        sb.AppendLine($"\tdb\t" + String.Join(",\t", tilesTouchedDeltas));
                        sb.AppendLine();

                        sb.AppendLine($"\t; Distances:");

                        //sb.AppendLine(firstDistance);
                        //sb.AppendLine();




                        int distanceCounter = 0;
                        foreach (var d in distancesNormalized)
                        {
                            string addr_1 = $"FixFishEyeTable + ({Math.Round(d)} * 64)";

                            //int converted_1 = ConvertToFixedPoint_8_8(d); // convert to fixed point 8.8

                            sb.AppendLine($"\tdw\t{addr_1}\t; distance for tile #{distanceCounter}, fixed point 8.8, decimal value: {d}");
                            distanceCounter++;
                        }

                        sb.AppendLine();
                        sb.AppendLine($"\tdb\t0,\t0 ; not used (Data to fill 64 bytes)"); // Data to fill 64 bytes

                        sb.AppendLine();
                        sb.AppendLine();

                        // debug
                        if (distances[0] > distances[1])
                        {
                            var error = "[ERROR] Distance 0 > distance 1";
                            sb.AppendLine(error);
                            Console.WriteLine(error);
                        }


                        // TODO: which wall was hit (N/E, S/W), for different shading

                        // TODO: point of impact into the wall, for texture mapping


                        //// Not really necessary, as data fits exactly the 16 kb
                        //if ((counter % 256) == 0)
                        //{
                        //    sb.AppendLine("\tds PageSize - ($ - 0x8000), 255");
                        //}

                        counter++;
                    }
                }
            }

            Console.WriteLine("[debug] maxDistance: " + maxDistance); // debug


            //Console.Write(sb.ToString());

            var sbTemp = new StringBuilder();
            int outputFileCounter = 0;
            foreach (var line in sb.ToString().Split(Environment.NewLine))
            {
                if (line == "; ---------------------------- MegaROM Page 90")
                {
                    File.WriteAllText($"PreCalcData_part_{outputFileCounter}.s", sbTemp.ToString());
                    outputFileCounter++;
                    sbTemp.Clear();
                }
                
                sbTemp.AppendLine(line);
            }
            File.WriteAllText($"PreCalcData_part_{outputFileCounter}.s", sbTemp.ToString());



            Console.Write("Done.");
            Console.ReadLine();

        }

        public static void CreateTiles()
        {
            const string patternsFilePath = @"TilePatterns.s";
            const string colorsFilePath = @"TileColors.s";

            IList<IList<string>> tilePatterns = new List<IList<string>> {
                new List<string> {
                    "\tdb  10001000 b",
                    "\tdb  00100010 b",
                    "\tdb  10001000 b",
                    "\tdb  00100010 b",
                    "\tdb  10001000 b",
                    "\tdb  00100010 b",
                    "\tdb  10001000 b",
                    "\tdb  00100010 b",
                },
                new List<string> {
                    "\tdb  10101010 b",
                    "\tdb  01010101 b",
                    "\tdb  10101010 b",
                    "\tdb  01010101 b",
                    "\tdb  10101010 b",
                    "\tdb  01010101 b",
                    "\tdb  10101010 b",
                    "\tdb  01010101 b",
                },
                new List<string> {
                    "\tdb  01110111 b",
                    "\tdb  11011101 b",
                    "\tdb  01110111 b",
                    "\tdb  11011101 b",
                    "\tdb  01110111 b",
                    "\tdb  11011101 b",
                    "\tdb  01110111 b",
                    "\tdb  11011101 b",
                },
                new List<string> {
                    "\tdb  11111111 b",
                    "\tdb  11111111 b",
                    "\tdb  11111111 b",
                    "\tdb  11111111 b",
                    "\tdb  11111111 b",
                    "\tdb  11111111 b",
                    "\tdb  11111111 b",
                    "\tdb  11111111 b",
                },
            };

            IList<string> colors = new List<string> { "0xba", "0xcb", "0xdc", "0xed", "0xfe" };

            var sbPatterns = new StringBuilder();
            sbPatterns.AppendLine("Tile_Patterns:");

            var sbColors = new StringBuilder();
            sbColors.AppendLine("Tile_Colors:");
            
            int index = 0;

            var bgColor = "0x00"; // temp
            //var color = colors.First(); // temp
            foreach (var color in colors)
            {
                for (int tilePatternIndex = 0; tilePatternIndex < tilePatterns.Count; tilePatternIndex++)
                {
                    sbPatterns.AppendLine($"; ----------------------- Tile pattern #{tilePatternIndex}");
                    sbPatterns.AppendLine();

                    sbColors.AppendLine($"; ----------------------- Tile pattern #{tilePatternIndex}, color {color}");
                    sbColors.AppendLine();

                    for (int i = 1; i <= 8; i++)
                    {
                        sbPatterns.AppendLine($"; Tile pattern #{tilePatternIndex}, height: {i}, index: {index}");
                        sbColors.AppendLine($"; Tile pattern #{tilePatternIndex}, height: {i}, index: {index}");

                        for (int j = 0; j < 8; j++)
                        {
                            sbPatterns.AppendLine(tilePatterns[tilePatternIndex][j]);
                        }
                        for (int j = 8 - i; j > 0; j--)
                        {
                            //sbPatterns.AppendLine("\tdb  00000000 b");
                            sbColors.AppendLine("\tdb  " + bgColor);
                        }
                        for (int j = 8 - i; j < 8; j++)
                        {
                            //sbPatterns.AppendLine(tilePatterns[tilePatternIndex][j]);
                            sbColors.AppendLine("\tdb  " + color);
                        }

                        sbPatterns.AppendLine();
                        sbColors.AppendLine();

                        index++;
                    }
                }
            }

            sbPatterns.AppendLine(".size: equ $ - Tile_Patterns");
            sbColors.AppendLine(".size: equ $ - Tile_Colors");

            Console.Write(sbPatterns.ToString());
            Console.Write(sbColors.ToString());

            File.WriteAllText(patternsFilePath, sbPatterns.ToString());
            File.WriteAllText(colorsFilePath, sbColors.ToString());
        }

        public static void CreateColumns() 
        {
            // Considering only first part of screen (top)
            // min height: 4px
            // max height: 64x
            // 

            const int minHeight = 4;
            const int maxHeight = 64;
            const int tilesNumber = 160;
            const double step = (double)tilesNumber / (double)(maxHeight - minHeight);

            const byte bgTile = 255;

            // full tiles: 7, 15, 23 ...

            // column examples:
            // height       data
            //   4          255 255 255 255 255 255 255 3           // second part of the screen is the reverse (3  255 255 255...)
            //   5          255 255 255 255 255 255 255 4
            //   6          255 255 255 255 255 255 255 5
            //   7          255 255 255 255 255 255 255 6
            //   8          255 255 255 255 255 255 255 7

            //   9          255 255 255 255 255 255 0   7
            //   10         255 255 255 255 255 255 1   7
            //   11         255 255 255 255 255 255 2   7
            //   12         255 255 255 255 255 255 3   7
            //   13         255 255 255 255 255 255 4   7
            //   14         255 255 255 255 255 255 5   7
            //   15         255 255 255 255 255 255 6   7
            //   16         255 255 255 255 255 255 7   7

            double counter = 0;

            int baseFullTile = 7;
            int fullTile = 7;
            var sb = new StringBuilder();



            for (int height = minHeight + 1; height <= 8; height++)
            //for (int height = 8; height >= minHeight + 1; height--)
            {
                IList<string> bytes = new List<string>();

                sb.Append("\tdb\t");
                for (int i = 0; i < 7; i++)
                {
                    bytes.Add(bgTile.ToString()); // 255
                }
                bytes.Add(((fullTile - 7) + height - 1).ToString());

                ((List<string>)bytes).AddRange(bytes.Reverse());

                sb.AppendLine(String.Join(",\t", bytes) + $" ; column height: {(height) * 2} pixels");

                counter += step;

                fullTile = baseFullTile + (8 * ((int)Math.Floor(counter / 8)));
            }
            sb.AppendLine("; -------");



            for (int j = 0; j < 7; j++)
            //for (int j = 6; j >= 0; j--)
            //int j = 6;
            {
                for (int height = 1; height <= 8; height++)
                {
                    int colHeight = 16 + (((j * 8) + height) * 2); // column height in pixels

                    IList<string> bytes = new List<string>();

                    sb.Append("\tdb\t");
                    for (int i = 0; i < 6-j; i++)
                    {
                        bytes.Add(bgTile.ToString()); // 255
                    }
                    
                    bytes.Add(((fullTile - 7) + height - 1).ToString());

                    for (int i = 6-j; i < 7; i++)
                    {
                        //sb.Append(",\t" + fullTile);
                        bytes.Add(fullTile.ToString());
                    }

                    ((List<string>)bytes).AddRange(bytes.Reverse());

                    //sb.AppendLine();
                    sb.AppendLine(String.Join(",\t", bytes) + $" ; column height: {colHeight} pixels");

                    counter += step;

                    fullTile = baseFullTile + (8 * ((int)Math.Floor(counter / 8)));
                }
                sb.AppendLine("; -------");
            }

            var output = ReverseLinesInStringBuilder(sb);
            
            Console.WriteLine(output);
        }

        private static string ReverseLinesInStringBuilder(StringBuilder sb)
        {
            // Convert StringBuilder to string
            string content = sb.ToString();

            // Split the content into lines, handling different newlines
            string[] lines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            // Reverse the order of the lines
            var reversedLines = lines.Reverse();

            // Join the reversed lines back into a single string using the system's new line separator
            string result = string.Join(Environment.NewLine, reversedLines);

            // You can optionally update the original StringBuilder
            // sb.Clear();
            // sb.Append(result);

            return result;
        }

        private static int ConvertToFixedPoint_8_8(double value)
        {
            return (int)Math.Round(value * 256, 15);
        }

        public static void CreateFixFishEyeTable()
        {
            var sb = new StringBuilder();

            //int colCounter = 0; //debug
            for (int columnHeight = 5; columnHeight <= 64; columnHeight++)
            {
                sb.AppendLine($"; ----- Column original height: {columnHeight}");

                for (int angle = -32; angle < 32; angle += 2)
                {
                    int adjustedHeight = (int)Math.Round(columnHeight * Math.Cos(Helpers.MathHelpers.DegreesToRadians(angle)));

                    if (adjustedHeight < 5) adjustedHeight = 5;

                    string addr = $"Columns + ({adjustedHeight - 5} * 16)";

                    sb.AppendLine($"\tdw\t{addr}\t; original height: {columnHeight}, ajusted height: {adjustedHeight}, fixed for angle: {angle}");
                }

                sb.AppendLine();

                //colCounter++;
            }

            Console.Write(sb.ToString());
        }

        /// <summary>
        /// Cos table for angles -32 to 30, for fisheye effect fix
        /// </summary>
        public static void CreateCosTable_32()
        {
            var sb = new StringBuilder();

            for (int angle = -32; angle < 32; angle += 2)
            {
                double cos = Math.Cos(Helpers.MathHelpers.DegreesToRadians(angle));

                sb.AppendLine($"\tdw\t{ConvertToFixedPoint_8_8(cos)}\t; cosine for angle: {angle} = {cos}");
            }

            Console.Write(sb.ToString());
        }
    }
}

