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
            int sprite0_offsetX, int sprite0_offsetY, int sprite0_width, int sprite0_height,
            int sprite1_offsetX, int sprite1_offsetY)
        {
            //List<List<int>> palette = GetPaletteFromFile_ToRgb(fileName);
            byte[] originalPalette = GetPaletteFromFile_ToBytes(fileName);
            var outputFileBaseName = "output";

            using (var input = File.OpenRead(fileName))
            using (var reader = new BinaryReader(input))
            using (var paletteFile = File.Create(outputFileBaseName + ".pal"))
            using (var patternsFile = File.Create(outputFileBaseName + ".pat"))
            using (var colorsFile = File.Create(outputFileBaseName + ".col"))
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

                IList<IList<int>> colorsList = new List<IList<int>>();
                IList<IList<int>> pixelsList = new List<IList<int>>();
                //string pattern_0 = "", pattern_1 = "";

                for (int j = 0; j < input.Length; j++)
                {
                    var byteRead = reader.ReadByte();

                    if (j < HEADER_SIZE) continue; // skip header

                    counter = j - HEADER_SIZE;
                    y = counter / (SCREEN_WIDTH_IN_BYTES);
                    x = (counter - (SCREEN_WIDTH_IN_BYTES * y)) * 2; // each byte represents 2 pixels on SC5
                    
                    // Left pixel
                    GetNibbleOfByte(sprite0_offsetX, sprite0_offsetY, x, y, totalLastX, totalLastY, usefulLastX, usefulLastY,
                        pixelsList, colorsList, byteRead, true);

                    // Right pixel
                    GetNibbleOfByte(sprite0_offsetX, sprite0_offsetY, x, y, totalLastX, totalLastY, usefulLastX, usefulLastY,
                        pixelsList, colorsList, byteRead, false);
                }

                // show pattern 0
                //Console.WriteLine(pattern_0);


                var listOf3Colors = new List<List<int>>();

                // count colors per line
                var lineNumber = 0;
                foreach (var line in colorsList)
                {
                    Console.Write("Line #" + lineNumber.ToString().PadLeft(2, ' ') + ": ");
                    //Console.Write(line.Count + " colors: ");
                    if (line.Count != 16) throw new InvalidDataException("Line #" + lineNumber + " has " + line.Count + " pixels. Should be 16.");

                    foreach (var color in line)
                    {
                        Console.Write(color.ToString().PadLeft(2, ' ') + ", ");
                    }

                    // get the distinct colors different than transparent on this line
                    var distinctColorsOnLine = line.Where(x => x != 0).Distinct().ToList();

                    Console.Write("; Distinct colors: " + distinctColorsOnLine.Count());

                    // check if OR-color is possible on the default palette
                    if (distinctColorsOnLine.Count() == 3)
                    {
                        listOf3Colors.Add(new List<int> { distinctColorsOnLine[0], distinctColorsOnLine[1], distinctColorsOnLine[2] });

                        if (
                               ((distinctColorsOnLine[0] | distinctColorsOnLine[1]) == distinctColorsOnLine[2])
                            || ((distinctColorsOnLine[0] | distinctColorsOnLine[2]) == distinctColorsOnLine[1])
                            || ((distinctColorsOnLine[1] | distinctColorsOnLine[2]) == distinctColorsOnLine[0])
                            )
                        {
                            Console.Write("; OR-color possible");
                        }
                        else
                        {
                            Console.Write("; OR-color impossible");
                        }
                    }

                    Console.WriteLine();

                    lineNumber++;
                }

                var distinctColors1 = colorsList.SelectMany(x => x).Where(x => x != 0).Distinct();
                Console.WriteLine();
                Console.WriteLine("Total distinct colors: " + distinctColors1.Count());

                Console.WriteLine();
                Console.WriteLine("3 colors combinations:");
                var listOf3ColorsNoRepeat = new List<List<int>>();
                foreach (var item in listOf3Colors)
                {
                    var colors = item.OrderBy(x => x).ToList();
                    foreach (var color in colors)
                    {
                        Console.Write(color + ", ");
                    }

                    // new list without duplicated combinations
                    if (!listOf3ColorsNoRepeat.Any(c => c.SequenceEqual(colors)))
                    {
                        listOf3ColorsNoRepeat.Add(colors);
                    }

                    Console.WriteLine();
                }

                var orColorCount = new Dictionary<int, int>();
                Console.WriteLine();
                Console.WriteLine("3 colors combinations without repetitions:");
                foreach (var colors in listOf3ColorsNoRepeat)
                {
                    foreach (var color in colors)
                    {
                        Console.Write(color + ", ");

                        // count most common colors
                        if (!orColorCount.ContainsKey(color))
                        {
                            orColorCount.Add(color, 0);
                        }
                        orColorCount[color]++;
                    }

                    Console.WriteLine();
                }


                Console.WriteLine();
                Console.WriteLine("Most common colors (only on 3 color lines):");
                var listOfBestOrColors = new List<int> { 
                    15,                 // 15 combinations
                    14, 13, 11, 7,      // 6  combinations
                    12, 10, 9, 6, 5, 3  // 1 combination
                };
                var index = 0;
                foreach (var item in orColorCount.OrderByDescending(x => x.Value))
                {
                    Console.Write("Color " + item.Key + ": " + item.Value + " times");
                    
                    Console.WriteLine();
                    
                    index++;
                }

                // Brute force to find a palette
                Console.WriteLine();
                Console.WriteLine("Brute force to find a palette");
                var newListOf3ColorsNoRepeat = new List<List<int>>();
                var newPalette = new List<int>();
                Random rnd = new Random();
                for (UInt64 i = 0; i < UInt64.MaxValue; i++)
                {
                    newListOf3ColorsNoRepeat.Clear();
                    newPalette.Clear();

                    // sort list of 15 random colors
                    for (int j = 1; j <= 15; j++)
                    {
                        bool found = false;
                        do
                        {
                            int randomNumber = rnd.Next(1, 16);
                            if (!newPalette.Contains(randomNumber))
                            {
                                newPalette.Add(randomNumber);
                                found = true;
                            }
                        }
                        while (!found);
                    }


                    // to substitute on list of 3 colors for this sprite
                    foreach (var colors in listOf3ColorsNoRepeat)
                    {
                        newListOf3ColorsNoRepeat.Add(new List<int>
                        {
                            newPalette[colors[0]],
                            newPalette[colors[1]],
                            newPalette[colors[2]]
                        });
                    }

                    // insert first color (transparent)
                    newPalette.Insert(0, 0);

                    // Check if this palette is valid for this sprite
                    var isValid = CheckIfPaletteIsValidForThisSprite(newListOf3ColorsNoRepeat);
                    //Console.WriteLine();
                    //Console.WriteLine("Palette #" + i + " is valid: " + isValid);
                    if (isValid)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Valid palette found");
                        foreach (var item in newPalette)
                        {
                            Console.Write(item + ", ");
                        }

                        Console.WriteLine();
                        Console.WriteLine();
                        foreach (var colors in newListOf3ColorsNoRepeat)
                        {
                            foreach (var color in colors)
                            {
                                Console.Write(color + ", ");
                            }
                            Console.WriteLine();
                        }

                        break;
                    }
                }

                // organize list (OR color should be the third of each line)
                foreach (var colors in newListOf3ColorsNoRepeat)
                {
                    var color0 = colors[0];
                    var color1 = colors[1];
                    var color2 = colors[2];

                    if ((colors[0] | colors[1]) == colors[2])
                    {
                        colors.Clear();
                        colors.Add(color0);
                        colors.Add(color1);
                        colors.Add(color2);
                    }
                    else if ((colors[2] | colors[1]) == colors[0])
                    {
                        colors.Clear();
                        colors.Add(color2);
                        colors.Add(color1);
                        colors.Add(color0);
                    }
                    else if ((colors[0] | colors[2]) == colors[1])
                    {
                        colors.Clear();
                        colors.Add(color0);
                        colors.Add(color2);
                        colors.Add(color1);
                    }
                }


                // generate patterns and colors for sprite
                //string pattern_0 = "", pattern_1 = "";
                string color_0 = "", color_1 = "";
                IList<string> pattern_0 = new List<string>(), pattern_1 = new List<string>();
                for (int i = 0; i < 32; i++)
                {
                    pattern_0.Add("");
                    pattern_1.Add("");
                }
                lineNumber = 0;
                foreach (var line in pixelsList)
                {
                    var colorsInThisLine = pixelsList[lineNumber].Where(x => x != 0).Distinct().ToList();

                    var colNumber = 0;
                    foreach (var pixel in line)
                    {
                        var lineIndex = (int)Math.Floor((decimal)lineNumber / 8);
                        var colIndex = (int)Math.Floor((decimal)colNumber / 8);
                        var spriteIndex = (colIndex * 2) + lineIndex;
                        var patternIndex = (spriteIndex * 8) + (lineNumber % 8);

                        // patterns
                        if (pixel == 0)
                        {
                            pattern_0[patternIndex] += "0";
                            pattern_1[patternIndex] += "0";
                        }
                        else
                        {
                            if (pixel == colorsInThisLine[0])
                            {
                                pattern_0[patternIndex] += "1";
                                pattern_1[patternIndex] += "0";
                            }
                            else if (pixel == colorsInThisLine[1])
                            {
                                pattern_0[patternIndex] += "0";
                                pattern_1[patternIndex] += "1";
                            }
                            else if (pixel == colorsInThisLine[2]) // OR-color
                            {
                                pattern_0[patternIndex] += "1";
                                pattern_1[patternIndex] += "1";
                            }
                        }

                        colNumber++;
                    }



                    // colors
                    if(colorsInThisLine.Count > 0) color_0 += colorsInThisLine[0];
                    if (colorsInThisLine.Count <= 1)
                    {
                        color_1 += "0";
                    }
                    else if (colorsInThisLine.Count == 2)
                    {
                        color_1 += colorsInThisLine[1];
                    }
                    else if (colorsInThisLine.Count == 3) 
                    { 
                        color_1 += (colorsInThisLine[1] + 64); 
                    }

                    color_0 += Environment.NewLine;
                    color_1 += Environment.NewLine;

                    lineNumber++;
                }

                Console.WriteLine();
                Console.WriteLine("; pattern 0:");
                foreach (var line in pattern_0)
                {
                    Console.WriteLine(line);
                }
                Console.WriteLine("; pattern 1:");
                foreach (var line in pattern_1)
                {
                    Console.WriteLine(line);
                }

                Console.WriteLine("; color 0:");
                Console.WriteLine(color_0);
                Console.WriteLine("; color 1:");
                Console.WriteLine(color_1);


                // Convert input palette to palette found and save to file
                var paletteBytes = new byte[32];
                for (int i = 0; i < 16; i++)
                {
                    // newPalette // 16-bytes palette index

                    paletteBytes[i * 2] = originalPalette[newPalette[i] * 2];
                    paletteBytes[(i * 2) + 1] = originalPalette[(newPalette[i] * 2) + 1];
                }
                paletteFile.Write(paletteBytes, 0, paletteBytes.Length);


                // save patterns file
                var patternBytes = new byte[64];
                index = 0;
                foreach (var line in pattern_0.Concat(pattern_1))
                {
                    patternBytes[index] = Convert.ToByte(line, 2);

                    index++;
                }
                patternsFile.Write(patternBytes, 0, patternBytes.Length);


                // save colors file
                var colorsBytes = new byte[32];
                index = 0;
                foreach (var line in (color_0 + color_1).Split(Environment.NewLine))
                {
                    if (line != "")
                    {
                        colorsBytes[index] = Convert.ToByte(line);

                        index++;
                    }
                }
                colorsFile.Write(colorsBytes, 0, colorsBytes.Length);
            }
        }

        private static bool CheckIfPaletteIsValidForThisSprite(List<List<int>> listOf3ColorsNoRepeat)
        {
            foreach (var colors in listOf3ColorsNoRepeat)
            {
                if (
                    ((colors[0] | colors[1]) != colors[2]) &&
                    ((colors[2] | colors[1]) != colors[0]) &&
                    ((colors[2] | colors[0]) != colors[1])
                  )
                {
                    return false;
                }
            }

            return true;
        }

        private static void GetNibbleOfByte(
            int sprite0_offsetX, int sprite0_offsetY, int x, int y, int totalLastX, int totalLastY, int usefulLastX, int usefulLastY,
            IList<IList<int>> pixelsList, IList<IList<int>> colorsList, byte byteRead, bool leftPixel)
        {
            if (!leftPixel)
            {
                x++;
            }

            var xSprite = x - sprite0_offsetX;
            var ySprite = y - sprite0_offsetY;

            if (x >= sprite0_offsetX && x <= totalLastX         // check if current (x, y) is inside the 16x16 sprite area
                && y >= sprite0_offsetY && y <= totalLastY)
            {
                if (x >= sprite0_offsetX && x <= usefulLastX    // check if current (x, y) is inside the width x height useful sprite area
                    && y >= sprite0_offsetY && y <= usefulLastY)
                {
                    if (xSprite == 0)
                    {
                        colorsList.Add(new List<int>());
                        pixelsList.Add(new List<int>());
                    }

                    var highNibble = byteRead & 0b11110000;
                    var lowNibble = byteRead & 0b00001111;

                    var leftPixelColor = highNibble >> 4;
                    var rightPixelColor = lowNibble;

                    //if (leftPixelColor != 0) colorsList.Add(leftPixelColor);
                    //if (rightPixelColor != 0) colorsList.Add(rightPixelColor);

                    if (leftPixel)
                    {
                        colorsList[ySprite].Add(leftPixelColor);
                        pixelsList[ySprite].Add(leftPixelColor);

                        //if (leftPixelColor == 0)
                        //{
                        //    pattern_0 += "0";
                        //    pattern_1 += "0";
                        //}
                        //else
                        //{
                        //    pattern_0 += "1";
                        //    pattern_1 += "1";
                        //}
                    }
                    else
                    {
                        colorsList[ySprite].Add(rightPixelColor);
                        pixelsList[ySprite].Add(rightPixelColor);

                        //if (rightPixelColor == 0)
                        //{
                        //    pattern_0 += "0";
                        //    pattern_1 += "0";
                        //}
                        //else
                        //{
                        //    pattern_0 += "1";
                        //    pattern_1 += "1";
                        //}
                    }
                }
                //else
                //{
                //    pattern_0 += "0";
                //    pattern_1 += "0";
                //}

                //if (x == totalLastX && y <= totalLastY)
                //{
                //    pattern_0 += Environment.NewLine;
                //    pattern_1 += Environment.NewLine;
                //}

            }
        }

        /// <summary>
        /// Convert a .sc5 image file generated by BMP2MSX
        /// into a series of 16x16 sprites with 3 colors per line (OR-color)
        /// </summary>
        /// <param name="fileName"></param>
        public static void Execute_old(string fileName)
        {
            List<List<int>> palette = GetPaletteFromFile_ToRgb(fileName);


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

        private static List<List<int>> GetPaletteFromFile_ToRgb(string fileName)
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

        private static byte[] GetPaletteFromFile_ToBytes(string fileName)
        {
            // get palette from file (last 32 bytes)
            byte[] paletteBytes = new byte[32];
            using (var input = File.OpenRead(fileName))
            using (BinaryReader reader = new BinaryReader(input))
            {
                reader.BaseStream.Seek(input.Length - 32, SeekOrigin.Begin);
                reader.Read(paletteBytes, 0, 32);
            }
            
            return paletteBytes;
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
