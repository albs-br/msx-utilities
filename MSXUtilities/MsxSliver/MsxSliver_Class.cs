using System;
using System.Collections.Generic;
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
            };

            var sbPatterns = new StringBuilder();
            var sbColors = new StringBuilder();
            int index = 0;

            for (int tilePatternIndex = 0; tilePatternIndex < tilePatterns.Count; tilePatternIndex++)
            {
                sbPatterns.AppendLine($"; ----------------------- Tile pattern #{tilePatternIndex}");
                sbPatterns.AppendLine();

                for (int i = 1; i <= 8; i++)
                {
                    sbPatterns.AppendLine($"; Tile pattern #{tilePatternIndex}, height: {i}, index: {index}");
                    for (int j = 8 - i; j > 0; j--)
                    {
                        sbPatterns.AppendLine("\tdb  00000000 b");
                    }
                    for (int j = 8 - i; j < 8; j++)
                    {
                        sbPatterns.AppendLine(tilePatterns[tilePatternIndex][j]);
                    }

                    sbPatterns.AppendLine();

                    index++;
                }

            }

            Console.Write(sbPatterns.ToString());
        }
    }
}

