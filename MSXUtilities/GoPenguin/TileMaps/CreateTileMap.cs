using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MSXUtilities.GoPenguin.TileMaps
{
    public static class CreateTileMap
    {
        public static void Execute(List<List<int>> tileMap_16x16_Static)
        {
            Console.WriteLine("Creating tilemap for Go Penguin");

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

            // ---- Conversion logic from tilemap 16x16 static to 8x8 animated

            // 1st step: Loop all tilemap16x16 testing < 256 and filling with zeroes to make = 256
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

            // 2nd step: Loop all tilemap16x16 converting to 8x8 static
            //      ex. input:  { 0, 1, 0 }
            //          output: { 0, 0, 9, 9, 0, 0 }
            //                  { 0, 0,33,33, 0, 0 }
            var tileMap_8x8_Animated = new List<List<int>>();
            for (int line = 0; line < tileMap_16x16_Static.Count; line++)
            {
                tileMap_8x8_Animated.Add(new List<int>());
                tileMap_8x8_Animated.Add(new List<int>());

                for (int column = 0; column < (TILEMAP_SIZE_IN_8X8_COLUMNS / 2); column++)
                {
                    // Empty (black)
                    if (tileMap_16x16_Static[line][column] == 0)
                    {
                        tileMap_8x8_Animated[line * 2].Add(0);
                        tileMap_8x8_Animated[line * 2].Add(0);
                        tileMap_8x8_Animated[(line * 2) + 1].Add(0);
                        tileMap_8x8_Animated[(line * 2) + 1].Add(0);
                    }
                    // Small Bricks
                    else if (tileMap_16x16_Static[line][column] == 1)
                    {
                        tileMap_8x8_Animated[line * 2].Add(9);
                        tileMap_8x8_Animated[line * 2].Add(9);
                        tileMap_8x8_Animated[(line * 2) + 1].Add(9);
                        tileMap_8x8_Animated[(line * 2) + 1].Add(9);
                    }
                    // Big Bricks
                    else if (tileMap_16x16_Static[line][column] == 2)
                    {
                        tileMap_8x8_Animated[line * 2].Add(57 - 24);
                        tileMap_8x8_Animated[line * 2].Add(65 - 24);
                        tileMap_8x8_Animated[(line * 2) + 1].Add(89 - 24);
                        tileMap_8x8_Animated[(line * 2) + 1].Add(97 - 24);
                    }
                    // Grass
                    else if (tileMap_16x16_Static[line][column] == 3)
                    {
                        tileMap_8x8_Animated[line * 2].Add(57 + 64 - 24);
                        tileMap_8x8_Animated[line * 2].Add(65 + 64 - 24);
                        tileMap_8x8_Animated[(line * 2) + 1].Add(89 + 64 - 24);
                        tileMap_8x8_Animated[(line * 2) + 1].Add(97 + 64 - 24);
                    }
                    // Rocks
                    else if (tileMap_16x16_Static[line][column] == 4)
                    {
                        tileMap_8x8_Animated[line * 2].Add(57 + 128 - 24);
                        tileMap_8x8_Animated[line * 2].Add(65 + 128 - 24);
                        tileMap_8x8_Animated[(line * 2) + 1].Add(89 + 128 - 24);
                        tileMap_8x8_Animated[(line * 2) + 1].Add(97 + 128 - 24);
                    }
                }
            }

            // 3rd step: loop all tilemap8x8 static converting to 8x8 animated
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
                        tileMap_8x8_Animated[line][column] = 1;
                        tileMap_8x8_Animated[line + 1][column] = 1;
                    }
                    else if (tileMap_8x8_Animated[line][column] == 9 && tileMap_8x8_Animated[line][column + 1] == 0)
                    {
                        tileMap_8x8_Animated[line][column] = 17;
                        tileMap_8x8_Animated[line + 1][column] = 17;
                    }

                    // Big bricks
                    if (tileMap_8x8_Animated[line][column] == 0 && tileMap_8x8_Animated[line][column + 1] == 57 - 24)
                    {
                        tileMap_8x8_Animated[line][column] = 49 - 24;
                        tileMap_8x8_Animated[line][column + 1] = 57 - 24;
                        tileMap_8x8_Animated[line + 1][column] = 81 - 24;
                        tileMap_8x8_Animated[line + 1][column + 1] = 89 - 24;
                    }
                    else if (tileMap_8x8_Animated[line][column] == 65 - 24 && tileMap_8x8_Animated[line][column + 1] == 57 - 24)
                    {
                        tileMap_8x8_Animated[line][column] = 73 - 24;
                        tileMap_8x8_Animated[line + 1][column] = 105 - 24;
                    }

                    // Grass
                    if (tileMap_8x8_Animated[line][column] == 0 && tileMap_8x8_Animated[line][column + 1] == 121 - 24)
                    {
                        tileMap_8x8_Animated[line][column] = 113 - 24;
                        tileMap_8x8_Animated[line][column + 1] = 121 - 24;
                        tileMap_8x8_Animated[line + 1][column] = 145 - 24;
                        tileMap_8x8_Animated[line + 1][column + 1] = 153 - 24;
                    }
                    else if (tileMap_8x8_Animated[line][column] == 129 - 24 && tileMap_8x8_Animated[line][column + 1] == 121 - 24)
                    {
                        tileMap_8x8_Animated[line][column] = 137 - 24;
                        tileMap_8x8_Animated[line + 1][column] = 169 - 24;
                    }

                    // Rocks
                    if (tileMap_8x8_Animated[line][column] == 0 && tileMap_8x8_Animated[line][column + 1] == 121 + 64 - 24)
                    {
                        tileMap_8x8_Animated[line][column] = 113 + 64 - 24;
                        tileMap_8x8_Animated[line][column + 1] = 121 + 64 - 24;
                        tileMap_8x8_Animated[line + 1][column] = 145 + 64 - 24;
                        tileMap_8x8_Animated[line + 1][column + 1] = 153 + 64 - 24;
                    }
                    else if (tileMap_8x8_Animated[line][column] == 129 + 64 - 24 && tileMap_8x8_Animated[line][column + 1] == 121 + 64 - 24)
                    {
                        tileMap_8x8_Animated[line][column] = 137 + 64 - 24;
                        tileMap_8x8_Animated[line + 1][column] = 169 + 64 - 24;
                    }
                }
            }

            // Save output file
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
    }
}
