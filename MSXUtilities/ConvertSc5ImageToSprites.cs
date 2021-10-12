using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MSXUtilities
{
    public static class ConvertSc5ImageToSprites
    {
        const int HEADER_SIZE = 7; // 7 bytes header

        public static void Execute(string fileName,
            int sprite0_offsetX, int sprite0_offsetY, int sprite0_width,
            int sprite1_offsetX, int sprite1_offsetY, int sprite0_height
            )
        {
            List<List<int>> palette = GetPaletteFromFile(fileName);

            using (var input = File.OpenRead(fileName))
            using (var reader = new BinaryReader(input))
            //using (var output = File.Create(fileName + ".new"))
            {
                const int SCREEN_WIDTH_IN_PIXELS = 256;
                const int SCREEN_WIDTH_IN_BYTES = SCREEN_WIDTH_IN_PIXELS / 2;
                const int SPRITE_WIDTH = 16;
                const int SPRITE_HEIGHT = 16;

                var counter = 0;
                int x = 0, y = 0;
                
                int totalLastX = sprite0_offsetX + SPRITE_WIDTH - 1;
                int totalLastY = sprite0_offsetY + SPRITE_HEIGHT - 1;

                int usefulLastX = sprite0_offsetX + sprite0_width - 1;
                int usefulLastY = sprite0_offsetY + sprite0_height - 1;

                IList<int> colorsList = new List<int>();
                //var pixelsList = new List<int>();
                //var foundCounter = 0;
                //var notFoundCounter = 0;
                string pattern_0 = "", pattern_1 = "";

                for (int j = 0; j < input.Length; j++)
                {
                    var byteRead = reader.ReadByte();

                    if (j < HEADER_SIZE) continue; // skip header

                    counter = j - HEADER_SIZE;
                    y = counter / (SCREEN_WIDTH_IN_BYTES);
                    x = (counter - (SCREEN_WIDTH_IN_BYTES * y)) * 2; // each byte represents 2 pixels on SC5
                    
                    // Left pixel
                    GetNibbleOfByte(sprite0_offsetX, sprite0_offsetY, x, y, totalLastX, totalLastY, usefulLastX, usefulLastY, ref pattern_0, ref pattern_1, byteRead, true);

                    // Right pixel
                    GetNibbleOfByte(sprite0_offsetX, sprite0_offsetY, x, y, totalLastX, totalLastY, usefulLastX, usefulLastY, ref pattern_0, ref pattern_1, byteRead, false);
                }

                //var buffer = new byte[4096 * 4]; // 16 kb page

                //// only one page
                //var actual = reader.Read(buffer, 0, buffer.Length);
                //output.Write(buffer, 0, actual);
            }
        }

        private static void GetNibbleOfByte(
            int sprite0_offsetX, int sprite0_offsetY, int x, int y, int totalLastX, int totalLastY, int usefulLastX, int usefulLastY, 
            ref string pattern_0, ref string pattern_1, byte byteRead, bool leftPixel)
        {
            if (!leftPixel)
            {
                x++;
            }

            if (x >= sprite0_offsetX && x <= totalLastX         // check if current (x, y) is inside the 16x16 sprite area
                && y >= sprite0_offsetY && y <= totalLastX)
            {
                if (x >= sprite0_offsetX && x <= usefulLastX    // check if current (x, y) is inside the width x height useful sprite area
                    && y >= sprite0_offsetY && y <= usefulLastY)
                {
                    var highNibble = byteRead & 0b11110000;
                    var lowNibble = byteRead & 0b00001111;

                    var leftPixelColor = highNibble >> 4;
                    var rightPixelColor = lowNibble;

                    //if (leftPixelColor != 0) colorsList.Add(leftPixelColor);
                    //if (rightPixelColor != 0) colorsList.Add(rightPixelColor);

                    if (leftPixel)
                    {
                        if (leftPixelColor == 0)
                        {
                            pattern_0 += "0";
                            pattern_1 += "0";
                        }
                        else
                        {
                            pattern_0 += "1";
                            pattern_1 += "1";
                        }
                    }
                    else
                    {
                        if (rightPixelColor == 0)
                        {
                            pattern_0 += "0";
                            pattern_1 += "0";
                        }
                        else
                        {
                            pattern_0 += "1";
                            pattern_1 += "1";
                        }
                    }
                }
                else
                {
                    pattern_0 += "0";
                    pattern_1 += "0";
                }

                if (x == totalLastX && y <= totalLastY)
                {
                    pattern_0 += Environment.NewLine;
                    pattern_1 += Environment.NewLine;
                }

            }
        }

        /// <summary>
        /// Convert a .sc5 image file generated by BMP2MSX
        /// into a series of 16x16 sprites with 3 colors per line (OR-color)
        /// </summary>
        /// <param name="fileName"></param>
        public static void Execute_old(string fileName)
        {
            List<List<int>> palette = GetPaletteFromFile(fileName);


            // create a replacement color for each color of the palette (color most similar)
            var paletteReplacement = new List<int>();
            paletteReplacement.Add(-1); // first index is transparent color
            const int FIRST_COLOR = 1; // skip palette first entry (transparent)
            for (int i = FIRST_COLOR; i < 16; i++)
            {
                // calculate distance from this color to each other
                var distances = new List<int>();
                var smallestDistance = (7 + 7 + 7) + 1;
                var smallestDistanceIndex = 0;
                for (int j = FIRST_COLOR; j < 16; j++)
                {
                    if (i != j)
                    {
                        const int RED = 0;
                        const int BLUE = 1;
                        const int GREEN = 2;
                        var currentDistance =
                            Math.Abs(palette[i][RED] - palette[j][RED]) +
                            Math.Abs(palette[i][BLUE] - palette[j][BLUE]) +
                            Math.Abs(palette[i][GREEN] - palette[j][GREEN]);
                        distances.Add(currentDistance);

                        if (currentDistance < smallestDistance)
                        {
                            smallestDistance = currentDistance;
                            smallestDistanceIndex = j;
                        }
                        else if (currentDistance == smallestDistance)
                        {
                            // if two RGB distances are equal, the one with smaller individual distances should prevail
                            // Ex.: RGB distances (0, 2, 0) vs (1, 1, 0), the second one is the winner

                            var distancesFromIToJ = new int[] {
                                Math.Abs(palette[i][RED] - palette[j][RED]),
                                Math.Abs(palette[i][BLUE] - palette[j][BLUE]),
                                Math.Abs(palette[i][GREEN] - palette[j][GREEN])
                            };

                            var distancesFromIToSmallest = new int[] {
                                Math.Abs(palette[i][RED] - palette[smallestDistanceIndex][RED]),
                                Math.Abs(palette[i][BLUE] - palette[smallestDistanceIndex][BLUE]),
                                Math.Abs(palette[i][GREEN] - palette[smallestDistanceIndex][GREEN])
                            };

                            if (Enumerable.Max(distancesFromIToJ) < Enumerable.Max(distancesFromIToSmallest))
                            {
                                smallestDistanceIndex = j;
                            }
                        }
                    }
                }

                // put the color most similar in the replacement array
                paletteReplacement.Add(smallestDistanceIndex);
            }

            using (var input = File.OpenRead(fileName))
            using (var reader = new BinaryReader(input))
            using (var output = File.Create(fileName + ".new"))
            {
                var counter = 0;
                IList<int> colorsList = new List<int>();
                //var pixelsList = new List<int>();
                var foundCounter = 0;
                var notFoundCounter = 0;
                for (int j = 0; j < input.Length; j++)
                {
                    var byteRead = reader.ReadByte();
                    if (j >= HEADER_SIZE) // skip header
                    {
                        var highNibble = byteRead & 0xf0;
                        var lowNibble = byteRead & 0b00001111;

                        var leftPixelColor = highNibble >> 4;
                        var rightPixelColor = lowNibble;

                        //if (leftPixelColor != 0 && !colorsList.Contains(leftPixelColor)) colorsList.Add(leftPixelColor);
                        //if (rightPixelColor != 0 && !colorsList.Contains(rightPixelColor)) colorsList.Add(rightPixelColor);

                        if (leftPixelColor != 0) colorsList.Add(leftPixelColor);
                        if (rightPixelColor != 0) colorsList.Add(rightPixelColor);

                        counter++;
                        if (counter == 8) // 8 bytes = 16 pixels on screen 5
                        {
                            var colorsGrouped = colorsList
                                .GroupBy(x => x)
                                .Select(n => new { ColorNumber = n.Key, Count = n.Count() })
                                .OrderByDescending(x => x.Count)
                                .ToList();

                            if (colorsGrouped.Count == 3)
                            {
                                //var color1 = 0;
                                //var color2 = 0;
                                //var orColor = 0;
                                bool found = false;

                                found = CheckIfOrColorIsPossible(
                                    colorsGrouped[0].ColorNumber,
                                    colorsGrouped[1].ColorNumber,
                                    colorsGrouped[2].ColorNumber
                                    );

                                if (!found)
                                {
                                    // try again, replacing each color by a replacement candidate
                                    found = CheckIfOrColorIsPossible(
                                        paletteReplacement[colorsGrouped[0].ColorNumber],
                                        colorsGrouped[1].ColorNumber,
                                        colorsGrouped[2].ColorNumber
                                        );

                                    if (!found)
                                    {
                                        found = CheckIfOrColorIsPossible(
                                            colorsGrouped[0].ColorNumber,
                                            paletteReplacement[colorsGrouped[1].ColorNumber],
                                            colorsGrouped[2].ColorNumber
                                            );
                                    }

                                    if (!found)
                                    {
                                        found = CheckIfOrColorIsPossible(
                                            colorsGrouped[0].ColorNumber,
                                            colorsGrouped[1].ColorNumber,
                                            paletteReplacement[colorsGrouped[2].ColorNumber]
                                            );
                                    }

                                }

                                if (!found)
                                {
                                    notFoundCounter++;
                                }
                                else
                                {
                                    foundCounter++;
                                }
                            }
                            else if (colorsGrouped.Count > 3)
                            {
                                // loop from color with less repetitions upwards
                                // replacing by the others until find a combination
                                // valid to form 3 colors with OR-color
                                foreach (var item in colorsGrouped)
                                {

                                }
                            }

                            colorsList.Clear();
                            counter = 0;
                        }
                    }
                }

                //var buffer = new byte[4096 * 4]; // 16 kb page

                //// only one page
                //var actual = reader.Read(buffer, 0, buffer.Length);
                //output.Write(buffer, 0, actual);
            }
        }

        private static List<List<int>> GetPaletteFromFile(string fileName)
        {
            // get palette from file (last 32 bytes)
            byte[] paletteBytes = new byte[32];
            using (var input = File.OpenRead(fileName))
            using (BinaryReader reader = new BinaryReader(input))
            {
                reader.BaseStream.Seek(input.Length - 32, SeekOrigin.Begin);
                reader.Read(paletteBytes, 0, 32);
            }
            var palette = new List<List<int>>();
            for (int i = 0; i < 32; i += 2)
            {
                int red = (paletteBytes[i] & 0b11110000) >> 4;
                int blue = (paletteBytes[i] & 0b00001111);
                int green = paletteBytes[i + 1];

                palette.Add(new List<int> { red, blue, green });
            }

            return palette;
        }

        private static bool CheckIfOrColorIsPossible(int color0, int color1, int color2)
        {
            var or1 = color0 | color1;
            if (or1 == color2)
            {
                //color1 = colorsGrouped[0].ColorNumber;
                //color2 = colorsGrouped[1].ColorNumber;
                //orColor = colorsGrouped[2].ColorNumber;
                return true;
            }

            var or2 = color0 | color2;
            if (or2 == color1)
            {
                //color1 = colorsGrouped[0].ColorNumber;
                //color2 = colorsGrouped[1].ColorNumber;
                //orColor = colorsGrouped[2].ColorNumber;
                return true;
            }

            var or3 = color1 | color2;
            if (or3 == color0)
            {
                //color1 = colorsGrouped[0].ColorNumber;
                //color2 = colorsGrouped[1].ColorNumber;
                //orColor = colorsGrouped[2].ColorNumber;
                return true;
            }

            return false;
        }

    }
}
