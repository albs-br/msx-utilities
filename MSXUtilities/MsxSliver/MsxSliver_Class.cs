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
            /*

            Quadrant 	x-coordinate	y-coordinate
            I (QI)	    Positive (+)	Positive (+)
            II (QII)	Negative (-)	Positive (+)
            III (QIII)	Negative (-)	Negative (-)
            IV (QIV)	Positive (+)	Negative (-)

            */



            const int MAP_WIDTH = 64;
            //const int MAP_HEIGHT = 64;

            const int TILE_SIZE = 16;  // Size of each tile in units, in both width and height

            const int MAX_TILES = 20;

            //const int ANGLE_STEP = 2; // Angle step in degrees

            //for (int y = 0; y < TILE_SIZE; y++)
            int y = 8;
            {
                //for (int x = 0; x < TILE_SIZE; x++)
                int x = 8;
                {
                    //for (double angle = 0; angle < 360; angle += ANGLE_STEP)
                    double angle = 180 + 30;
                    {
                        Console.WriteLine($"; ---- Data for position ({x}, {y}), angle {angle}");
                        Console.WriteLine();

                        double angle_rad = Helpers.MathHelpers.DegreesToRadians(angle);


                        double step_X = Math.Cos(angle_rad) * (TILE_SIZE) / 1000;
                        double step_Y = Math.Sin(angle_rad) * (TILE_SIZE) / 1000;

                        double ray_X = x;
                        double ray_Y = y;
                        while (ray_X > 0 && ray_X < TILE_SIZE && ray_Y > 0 && ray_Y < TILE_SIZE) // check tile bounds
                        {
                            ray_X += step_X;
                            ray_Y += step_Y;
                        }


                        // calc hypotenuse
                        double dist = Math.Sqrt(Math.Pow(ray_X - x, 2) + Math.Pow(ray_Y - y, 2));


                        int converted = ConvertToFixedPoint_8_8(dist); // convert to fixed point 8.8

                        double rayOrigin_X = (x * (TILE_SIZE / 1000));
                        double rayOrigin_Y = (y * (TILE_SIZE / 1000));

                        string firstDistance = $"\tdw\t{converted}\t; distance to edge of first tile (fixed point 8.8), decimal value: {dist}";

                        // save position of ray at edge of tile
                        double ray_edge_X = rayOrigin_X + ray_X;
                        double ray_edge_Y = rayOrigin_Y + ray_Y;

                        // ------ first tile


                        // calc tile relative to tile origin
                        int tile_X = (int)Math.Floor(ray_edge_X / (TILE_SIZE));
                        int tile_Y = (int)Math.Floor(ray_edge_Y / (TILE_SIZE));

                        int tileLinear = (tile_Y * MAP_WIDTH) + tile_X;

                        //Console.WriteLine($"\tdb\t{tileLinear}\t; tile X: {tile_X}, Y: {tile_Y}");



                        // ----------------------------------- 

                        IList<int> tilesTouched = new List<int>();
                        tilesTouched.Add(tileLinear); // first tile
                        int tilesNumber = 1;

                        IList<double> distances = new List<double>();

                        while (tilesNumber < MAX_TILES)
                        {
                            ray_X += step_X;
                            ray_Y += step_Y;

                            // calc tile relative to tile origin
                            tile_X = (int)Math.Floor(rayOrigin_X + ray_X / (TILE_SIZE));
                            tile_Y = (int)Math.Floor(rayOrigin_Y + ray_Y / (TILE_SIZE));

                            tileLinear = (tile_Y * MAP_WIDTH) + tile_X;

                            if (tileLinear != tilesTouched.Last())
                            {
                                // new tile, add it
                                tilesTouched.Add(tileLinear);
                                tilesNumber++;

                                distances.Add(Math.Sqrt(Math.Pow(ray_X - rayOrigin_X, 2) + Math.Pow(ray_Y - rayOrigin_Y, 2)));

                                if (tilesNumber == MAX_TILES)
                                {
                                    // max tile x and y
                                    Console.WriteLine($"\tdb\t{tile_X},\t{tile_Y} ; Max tiles: x: {tile_X}, y: {tile_Y}");
                                    Console.WriteLine();
                                }
                            }

                        }

                        IList<int> tilesTouchedDelta = new List<int>();
                        tilesTouchedDelta.Add(tilesTouched.First());
                        for (int i = 1; i < tilesTouched.Count; i++)
                        {
                            tilesTouchedDelta.Add(tilesTouched[i] - tilesTouched[i - 1]);
                        }


                        Console.WriteLine($"\t; Tiles touched deltas");
                        Console.Write($"\tdb\t" + String.Join(",\t", tilesTouchedDelta)); //TODO: convert to fixed point 8.8
                        Console.WriteLine();
                        Console.WriteLine();

                        Console.WriteLine($"\t; Distances:");

                        Console.WriteLine(firstDistance);
                        Console.WriteLine();

                        int counter = 1;
                        foreach (var d in distances)
                        {
                            Console.WriteLine($"\tdw\t{ConvertToFixedPoint_8_8(d)}\t; distance for tile #{counter}, fixed point 8.8, decimal value: {d}");
                            counter++;
                        }
                        Console.WriteLine();
                        Console.WriteLine();

                        // TODO: which wall was hit (N/E, S/W), for different shading

                        // TODO: point of impact into the wall, for texture mapping
                    }
                }
            }

            Console.ReadLine();

            static int ConvertToFixedPoint_8_8(double value)
            {
                return (int)Math.Round(value * 256, 15);
            }
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

            const int bgTile = 255;

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
            for (int height = 4; height <= 8; height++)
            {
                IList<string> bytes = new List<string>();

                sb.Append("\tdb\t");
                for (int i = 0; i < 7; i++)
                {
                    //sb.Append("255,\t");
                    bytes.Add("255");
                }
                //sb.AppendLine(((fullTile - 7) + height - 1).ToString());
                bytes.Add(((fullTile - 7) + height - 1).ToString());

                ((List<string>)bytes).AddRange(bytes.Reverse());

                sb.AppendLine(String.Join(",\t", bytes));

                counter += step;

                fullTile = baseFullTile + (8 * ((int)Math.Floor(counter / 8)));
            }



            for (int j = 0; j < 7; j++)
            //int j = 6;
            {
                sb.AppendLine("; -------");
                for (int height = 1; height <= 8; height++)
                {
                    IList<string> bytes = new List<string>();

                    sb.Append("\tdb\t");
                    for (int i = 0; i < 6-j; i++)
                    {
                        //sb.Append("255,\t");
                        bytes.Add("255");
                    }
                    
                    //sb.Append(((fullTile - 7) + height - 1).ToString());
                    bytes.Add(((fullTile - 7) + height - 1).ToString());

                    for (int i = 6-j; i < 7; i++)
                    {
                        //sb.Append(",\t" + fullTile);
                        bytes.Add(fullTile.ToString());
                    }

                    ((List<string>)bytes).AddRange(bytes.Reverse());

                    //sb.AppendLine();
                    sb.AppendLine(String.Join(",\t", bytes));

                    counter += step;

                    fullTile = baseFullTile + (8 * ((int)Math.Floor(counter / 8)));
                }
            }

            Console.WriteLine(sb.ToString());
        }
    }
}

