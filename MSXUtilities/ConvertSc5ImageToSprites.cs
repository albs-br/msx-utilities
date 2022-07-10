using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSXUtilities
{
    public static class ConvertSc5ImageToSprites
    {
        const int HEADER_SIZE = 7; // 7 bytes header

        public static void Execute(string fileName,
            int sprite0_offsetX, int sprite0_offsetY, int sprite0_width, int sprite0_height,
            int sprite1_offsetX, int sprite1_offsetY,
            string outputFileBaseName,
            bool bruteForcePalette = true)
        {
            Console.WriteLine("Converting sprite: " + outputFileBaseName);

            var paletteBytes = new byte[32];
            var patternBytes = new byte[64];
            var colorsBytes = new byte[32];

            using (var input = File.OpenRead(fileName))
            using (var reader = new BinaryReader(input))
            {
                DoConversion_2_Sprites(
                    sprite0_offsetX, sprite0_offsetY, 
                    sprite1_offsetX, sprite1_offsetY,
                    sprite0_width, sprite0_height, 
                    paletteBytes, patternBytes, colorsBytes, 
                    input, reader, bruteForcePalette);
            }

            // save output files
            using (var paletteFile = File.Create(outputFileBaseName + ".pal"))
            using (var patternsFile = File.Create(outputFileBaseName + ".pat"))
            using (var colorsFile = File.Create(outputFileBaseName + ".col"))
            {
                paletteFile.Write(paletteBytes, 0, paletteBytes.Length);
                patternsFile.Write(patternBytes, 0, patternBytes.Length);
                colorsFile.Write(colorsBytes, 0, colorsBytes.Length);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sprite0_offsetX">Offset X from top left of the source bmp</param>
        /// <param name="sprite0_offsetY">Offset Y from top left of the source bmp</param>
        /// <param name="sprite1_offsetX">Offset X from top left of the sprite 0</param>
        /// <param name="sprite1_offsetY">Offset Y from top left of the sprite 0</param>
        /// <param name="sprite0_width">Normally 16 pixels (untested with other values)</param>
        /// <param name="sprite0_height">Normally 16 pixels (untested with other values)</param>
        /// <param name="paletteBytes"></param>
        /// <param name="patternBytes"></param>
        /// <param name="colorsBytes"></param>
        /// <param name="input"></param>
        /// <param name="reader"></param>
        /// <param name="bruteForcePalette"></param>
        public static void DoConversion_2_Sprites(
            int sprite0_offsetX, int sprite0_offsetY,
            int sprite1_offsetX, int sprite1_offsetY, 
            int sprite0_width, int sprite0_height, 
            byte[] paletteBytes, byte[] patternBytes, byte[] colorsBytes, 
            FileStream input, BinaryReader reader,
            bool bruteForcePalette = true)
        {
            List<List<int>> paletteRGB = GetPaletteFromFile_ToRgb(input, reader);
            byte[] originalPalette = GetPaletteFromFile_ToBytes(input, reader);


            const int SCREEN_WIDTH_IN_PIXELS = 256;
            const int SCREEN_WIDTH_IN_BYTES = SCREEN_WIDTH_IN_PIXELS / 2;
            const int SPRITE_WIDTH = 16;
            const int SPRITE_HEIGHT = 16;

            var counter = 0;
            int x = 0, y = 0;

            int totalLastX = sprite0_offsetX + SPRITE_WIDTH + sprite1_offsetX - 1;
            int totalLastY = sprite0_offsetY + SPRITE_HEIGHT + sprite1_offsetY - 1;

            int usefulLastX = sprite0_offsetX + sprite0_width + sprite1_offsetX - 1;
            int usefulLastY = sprite0_offsetY + sprite0_height + sprite1_offsetY - 1;

            IList<IList<int>> pixelsList = new List<IList<int>>();

            // return to start of file
            reader.BaseStream.Seek(0, SeekOrigin.Begin);

            for (int j = 0; j < input.Length; j++)
            {
                var byteRead = reader.ReadByte();

                if (j < HEADER_SIZE) continue; // skip header

                counter = j - HEADER_SIZE;
                y = counter / (SCREEN_WIDTH_IN_BYTES);
                x = (counter - (SCREEN_WIDTH_IN_BYTES * y)) * 2; // each byte represents 2 pixels on SC5

                // Left pixel
                GetNibbleOfByte(sprite0_offsetX, sprite0_offsetY, x, y, totalLastX, totalLastY, usefulLastX, usefulLastY,
                    pixelsList, byteRead, true);

                // Right pixel
                GetNibbleOfByte(sprite0_offsetX, sprite0_offsetY, x, y, totalLastX, totalLastY, usefulLastX, usefulLastY,
                    pixelsList, byteRead, false);
            }


            var listOf3Colors = new List<List<int>>();

            // count colors per line
            Console.WriteLine();
            Console.WriteLine("Original sprite:");
            var isPaletteValid = ShowSprite(pixelsList, listOf3Colors);

            // reduce the color count to max 3 colors per line
            if (!isPaletteValid)
            {
                foreach (var line in pixelsList)
                {
                    var distinctColorsInLine = line.Where(x => x != 0).Distinct();
                    //while (distinctColorsInLine.Count() > 2)
                    while (distinctColorsInLine.Count() > 3)
                    {
                        ReduceColorCountInLine(paletteRGB, line);
                        distinctColorsInLine = line.Where(x => x != 0).Distinct();
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Sprite with color count reduced:");
            isPaletteValid = ShowSprite(pixelsList, listOf3Colors);

            //TODO
            // solve the lines with 3 colors and or-color impossible

            // create list of all OR-color combinations possible
            var allOrColor = new List<List<int>>();
            for (int color1 = 1; color1 <= 15; color1++)
            {
                for (int color2 = 1; color2 <= 15; color2++)
                {
                    var orColor = color1 | color2;
                    if (color1 != color2 && color1 != orColor && color2 != orColor)
                    {
                        // add all possible combinations
                        allOrColor.Add(new List<int> { color1, color2, orColor });
                        allOrColor.Add(new List<int> { color1, orColor, color2 });
                        allOrColor.Add(new List<int> { color2, color1, orColor });
                        allOrColor.Add(new List<int> { color2, orColor, color1 });
                        allOrColor.Add(new List<int> { orColor, color1, color2 });
                        allOrColor.Add(new List<int> { orColor, color2, color1 });
                    }
                }
            }

            for (int i = 0; i < pixelsList.Count; i++)
            {
                var line = pixelsList[i];

                var distinctColorsOnLine = line.Where(x => x != 0).Distinct().ToList();
                if (distinctColorsOnLine.Count() == 3)
                {
                    if (!CheckIfOrColorIsPossible(distinctColorsOnLine))
                    {
                        Console.WriteLine("-----------------------------");
                        Console.WriteLine(String.Format("Searching best or-color combination for ({0}, {1}, {2})", distinctColorsOnLine[0], distinctColorsOnLine[1], distinctColorsOnLine[2]));

                        // compare this or-color combination with all possible or-color combinations
                        // and get the combination with smallest distance from original
                        double bestDistanceSum = double.MaxValue;
                        var bestOrColorCombination = new List<int>();
                        int bestDifferentColorComponents = int.MaxValue;

                        foreach (var orColorList in allOrColor)
                        {
                            double distanceSum = 0;
                            int differentColorComponents = 0; // number of RGB components different between the 2 colors
                            for (int j = 0; j <= 2; j++)
                            {
                                var color1 = paletteRGB[distinctColorsOnLine[j]];
                                var color2 = paletteRGB[orColorList[j]];
                                distanceSum += ColourDistance(color1, color2);

                                differentColorComponents += Math.Abs(color1[0] - color2[0]); // red
                                differentColorComponents += Math.Abs(color1[1] - color2[1]); // green
                                differentColorComponents += Math.Abs(color1[2] - color2[2]); // blue
                            }

                            //Console.Write(String.Format("distanceSum:{0}, differentColorComponents:{1} ({2}, {3}, {4})", 
                            //    distanceSum, differentColorComponents, orColorList[0], orColorList[1], orColorList[2]));
                            if ((distanceSum <= bestDistanceSum) && (differentColorComponents <= bestDifferentColorComponents))
                            {
                                bestDistanceSum = distanceSum;
                                bestOrColorCombination = orColorList;
                                bestDifferentColorComponents = differentColorComponents;
                                //Console.Write(" (*)");
                            }
                            //Console.WriteLine();
                        }

                        // replace pixels in line by the new colors
                        for (int pixel = 0; pixel < line.Count; pixel++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                if (line[pixel] == distinctColorsOnLine[j])
                                {
                                    line[pixel] = bestOrColorCombination[j];
                                }
                            }
                        }
                    }
                }
            }

            // save 16x16 bmp image after conversion
            if (sprite1_offsetX == 0 && sprite1_offsetY == 0) // TODO: fix it
            {
                var bmp = new Bitmap(16, 16);
                var yBmp = 0;
                foreach (var line in pixelsList)
                {
                    var xBmp = 0;
                    foreach (var pixel in line)
                    {
                        var red = paletteRGB[pixel][0] * 36; // 36 = 255 / 7, to convert rgb level from 0-7 to 0-255
                        var green = paletteRGB[pixel][1] * 36;
                        var blue = paletteRGB[pixel][2] * 36;
                        bmp.SetPixel(xBmp, yBmp, Color.FromArgb(red, green, blue));
                        xBmp++;
                    }
                    yBmp++;
                }
                bmp.Save("temp.bmp", ImageFormat.Bmp);
            }

            Console.WriteLine();
            Console.WriteLine("Sprite after or-colors replaced:");
            isPaletteValid = ShowSprite(pixelsList, listOf3Colors);

            var totalDistinctColors = pixelsList.SelectMany(x => x).Where(x => x != 0).Distinct();
            Console.WriteLine();
            Console.WriteLine("Total distinct colors: " + totalDistinctColors.Count());

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
            //var listOfBestOrColors = new List<int> {
            //    15,                 // 15 combinations
            //    14, 13, 11, 7,      // 6  combinations
            //    12, 10, 9, 6, 5, 3  // 1 combination
            //};
            var index = 0;
            foreach (var item in orColorCount.OrderByDescending(x => x.Value))
            {
                Console.Write("Color " + item.Key + ": " + item.Value + " times");

                Console.WriteLine();

                index++;
            }
            Console.WriteLine();


            IList<IList<int>> newPixelsList = new List<IList<int>>();
            IList<int> newPaletteFound = null;
            if (!isPaletteValid && bruteForcePalette)
            {
                // Brute force to find a palette
                Console.WriteLine();
                Console.WriteLine("Brute force to find a palette");

                // Brute force sequentially (not working: 90 millions combinations tried and no match):
                //var basePalette = new List<int>();
                //for (int j = 1; j <= 15; j++)
                //{
                //    basePalette.Add(j);
                //}
                //var allPalettes = GetPermutations(basePalette, 15);
                //var cont = 0;
                //foreach (var item in allPalettes)
                //{
                //    //Console.Write(item.Count());
                //    foreach (var item1 in item)
                //    {
                //        Console.Write(item1 + ", ");
                //    }
                //    Console.WriteLine();
                //    if (cont++ > 100) break;
                //}

                Parallel.For(0, int.MaxValue, (i, state) =>
                {
                    if ((i % 100000) == 0) Console.Write(".");

                    Random rnd = new Random();

                    var newListOf3ColorsNoRepeat = new List<List<int>>();
                    IList<int> newPalette = new List<int>();

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

                    // add transparent color
                    // insert first color (transparent)
                    newPalette.Insert(0, 0);

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
                        Console.WriteLine("List of 3 colors before/after substitution");
                        var index1 = 0;
                        foreach (var colors in newListOf3ColorsNoRepeat)
                        {
                            for (int k = 0; k < listOf3ColorsNoRepeat[index1].Count; k++)
                            {
                                Console.Write(listOf3ColorsNoRepeat[index1][k] + ", ");
                            }

                            Console.Write(" ==> ");

                            foreach (var color in colors)
                            {
                                Console.Write(color + ", ");
                            }
                            Console.WriteLine();

                            index1++;
                        }

                        //state.Break();
                        state.Stop();

                        newPaletteFound = newPalette;
                    }
                });
                Console.WriteLine();

                // transform sprite on original palette to new palette
                foreach (var line in pixelsList)
                {
                    //[debug]
                    //var colorsInThisLine = line.Where(x => x != 0).Distinct().ToList();
                    //if (colorsInThisLine.Count == 3)
                    //{
                    //}


                    IList<int> newLine = new List<int>();
                    foreach (var pixel in line)
                    {
                        // find this pixel color on new palette
                        //var newIndex = 0;
                        //for (int i = 0; i < newPalette.Count; i++)
                        //{
                        //    if (pixel == newPalette[i])
                        //    {
                        //        newIndex = i;
                        //        break;
                        //    }
                        //}
                        //newLine.Add(newIndex);

                        newLine.Add(newPaletteFound[pixel]);
                    }
                    newPixelsList.Add(newLine);
                }

                var dummyList = new List<List<int>>();
                Console.WriteLine();
                Console.WriteLine("Sprite after substitutions:");
                ShowSprite(newPixelsList, dummyList);
            }
            else
            {
                newPixelsList = pixelsList;
                newPaletteFound = new List<int>();
                for (int i = 0; i < originalPalette.Length; i++)
                {
                    newPaletteFound.Add(originalPalette[i]);
                }
            }


            // generate patterns and colors for sprite
            //string pattern_0 = "", pattern_1 = "";
            string color_line_pattern_0 = "", color_line_pattern_1 = "";
            IList<string> pattern_0 = new List<string>(), pattern_1 = new List<string>();
            for (int i = 0; i < 32; i++)
            {
                pattern_0.Add("");
                pattern_1.Add("");
            }




            var errorCount = 0;
            var lineNumber = 0;
            int pattern_0_Index = 0, pattern_1_Index = 0;
            foreach (var line in newPixelsList)
            {
                try
                {
                    bool isFirstSpriteLine = false, isSecondSpriteLine = false;

                    Console.Write("image line " + (lineNumber + sprite0_offsetY) + ", sprite line " + lineNumber);

                    List<int> colorsInThisLine = line.Where(x => x != 0).Distinct().ToList();
                    int color0 = -1, color1 = -1, orColor = -1;

                    // for sprite0_height = 16 and sprite1_offsetY = 8
                    // first sprite:        lines 0-15
                    // second sprite:       lines 8-23
                    // only first sprite:   lines 0-7
                    // both sprites:        lines 8-15
                    // only second sprite:  lines 16-23
                    if ((lineNumber < sprite1_offsetY))
                    {
                        // only first sprite
                        Console.WriteLine(", only first sprite");

                        isFirstSpriteLine = true;
                        color0 = colorsInThisLine[0];

                        if (colorsInThisLine.Count > 1)
                        {
                            throw new Exception("Only one color possible on this line (line #" + lineNumber + ")");
                        }
                    }
                    else if (lineNumber >= sprite0_height)
                    {
                        // only second sprite
                        Console.WriteLine(", only second sprite");

                        isSecondSpriteLine = true;
                        color1 = colorsInThisLine[0];

                        if (colorsInThisLine.Count > 1)
                        {
                            throw new Exception("Only one color possible on this line (line #" + lineNumber + ")");
                        }
                    }
                    else
                    {
                        // both sprites
                        Console.WriteLine(", both sprites");

                        isFirstSpriteLine = true;
                        isSecondSpriteLine = true;
                        ExtractColorsFromLine(line, lineNumber, sprite0_width, sprite1_offsetX, out colorsInThisLine, out color0, out color1, out orColor);

                        if (colorsInThisLine.Count > 3) throw new Exception("Only 3 colors possible on this line (line #" + lineNumber + ")");
                    }

                    var colNumber = 0;
                    foreach (var pixel in line)
                    {
                        // check if this pixel belongs to sprite 0 and 1 
                        var isFirstSpriteCol = false;
                        var isSecondSpriteCol = false;
                        if (colNumber < sprite0_width) isFirstSpriteCol = true;
                        if (colNumber >= sprite1_offsetX) isSecondSpriteCol = true;

                        if (isFirstSpriteCol)
                        {
                            //int patternIndex = GetSpriteNumber(lineNumber, pattern_0_Index, colNumber, sprite1_offsetX);

                            var lineIndex = (int)Math.Floor((decimal)pattern_0_Index / 8);
                            var colIndex = (int)Math.Floor((decimal)colNumber / 8);
                            var spriteIndex = (colIndex * 2) + lineIndex;
                            var patternIndex = (spriteIndex * 8) + (pattern_0_Index % 8);

                            // patterns
                            var bitPattern_0 = "";
                            if (pixel == 0)
                            {
                                bitPattern_0 = "0";
                            }
                            else if (pixel == color0)
                            {
                                bitPattern_0 = "1";
                            }
                            else if (pixel == color1)
                            {
                                bitPattern_0 = "0";
                            }
                            else if (pixel == orColor)
                            {
                                bitPattern_0 = "1";
                            }
                            else
                            {
                                throw new InvalidDataException();
                            }

                            if (isFirstSpriteLine) pattern_0[patternIndex] += bitPattern_0;
                        }

                        // both sprites
                        if (isFirstSpriteCol && isSecondSpriteCol)
                        {
                            //int patternIndex = GetSpriteNumber(lineNumber, pattern_1_Index, colNumber, sprite1_offsetX);

                            // logic to calc which of the four 8x8 sprites this pixel belongs
                            var lineIndex = (int)Math.Floor((decimal)pattern_1_Index / 8);
                            var colIndex = (int)Math.Floor(((decimal)(colNumber - sprite1_offsetX)) / 8);
                            var spriteIndex = (colIndex * 2) + lineIndex;
                            var patternIndex = (spriteIndex * 8) + (pattern_1_Index % 8);

                            // patterns
                            var bitPattern_1 = "";
                            if (pixel == 0)
                            {
                                bitPattern_1 = "0";
                            }
                            else if (pixel == color0)
                            {
                                bitPattern_1 = "0";
                            }
                            else if (pixel == color1)
                            {
                                bitPattern_1 = "1";
                            }
                            else if (pixel == orColor)
                            {
                                bitPattern_1 = "1";
                            }
                            else
                            {
                                throw new InvalidDataException();
                            }

                            if (isSecondSpriteLine) pattern_1[patternIndex] += bitPattern_1;
                        }

                        // only second sprite
                        if (!isFirstSpriteCol && isSecondSpriteCol)
                        {
                            //int patternIndex = GetSpriteNumber(lineNumber, pattern_1_Index, colNumber, sprite1_offsetX);

                            // logic to calc which of the four 8x8 sprites this pixel belongs
                            var lineIndex = (int)Math.Floor((decimal)pattern_1_Index / 8);
                            var colIndex = (int)Math.Floor(((decimal)(colNumber - sprite1_offsetX)) / 8);
                            var spriteIndex = (colIndex * 2) + lineIndex;
                            var patternIndex = (spriteIndex * 8) + (pattern_1_Index % 8);

                            // patterns
                            var bitPattern_1 = "";
                            if (pixel == 0)
                            {
                                bitPattern_1 = "0";
                            }
                            else if (pixel == color0)
                            {
                                bitPattern_1 = "1";
                            }
                            else if (pixel == color1)
                            {
                                bitPattern_1 = "1";
                            }
                            else if (pixel == orColor)
                            {
                                bitPattern_1 = "1";
                            }
                            else
                            {
                                throw new InvalidDataException();
                            }

                            if (isSecondSpriteLine) pattern_1[patternIndex] += bitPattern_1;
                        }

                        colNumber++;
                    }



                    // colors
                    if (colorsInThisLine.Count == 0)
                    {
                        if (isFirstSpriteLine) color_line_pattern_0 += "0";
                        if (isSecondSpriteLine) color_line_pattern_1 += "0";
                    }
                    else if (colorsInThisLine.Count == 1)
                    {
                        if (isSecondSpriteLine && isFirstSpriteLine)
                        {
                            color_line_pattern_0 += color0;
                            color_line_pattern_1 += "0";
                        }
                        else if (isFirstSpriteLine)
                        {
                            color_line_pattern_0 += color0;
                        }
                        else if (isSecondSpriteLine)
                        {
                            color_line_pattern_1 += color1;
                        }
                    }
                    else if (colorsInThisLine.Count == 2)
                    {
                        if (isSecondSpriteLine && isFirstSpriteLine)
                        {
                            color_line_pattern_0 += color0;
                            color_line_pattern_1 += color1;
                        }
                        else if (isFirstSpriteLine)
                        {
                            throw new InvalidDataException("More than one color for this sprite line. Line #" + lineNumber);
                        }
                        else if (isSecondSpriteLine)
                        {
                            throw new InvalidDataException("More than one color for this sprite line. Line #" + lineNumber);
                        }
                    }
                    else if (colorsInThisLine.Count == 3)
                    {
                        if (isSecondSpriteLine && isFirstSpriteLine)
                        {
                            color_line_pattern_0 += color0;
                            color_line_pattern_1 += (color1 + 64); // or-color
                        }
                        else if (isFirstSpriteLine)
                        {
                            throw new InvalidDataException("More than one color for this sprite line. Line #" + lineNumber);
                        }
                        else if (isSecondSpriteLine)
                        {
                            throw new InvalidDataException("More than one color for this sprite line. Line #" + lineNumber);
                        }
                    }
                    else
                    {
                        throw new InvalidDataException("More than 3 colors for this sprite line. Line #" + lineNumber);
                    }

                    //if (colorsInThisLine.Count > 0 && isFirstSpriteLine) color_line_pattern_0 += color0;
                    //if (colorsInThisLine.Count == 1 && isSecondSpriteLine && isFirstSpriteLine)
                    //{
                    //    color_line_pattern_1 += "0";
                    //}
                    //else if (colorsInThisLine.Count == 3 && isFirstSpriteLine && isSecondSpriteLine)
                    //{
                    //    color_line_pattern_1 += (color1 + 64); // or-color
                    //}
                    //else if (isSecondSpriteLine && colorsInThisLine.Count != 0)
                    //{
                    //    color_line_pattern_1 += color1;
                    //}

                    if (isFirstSpriteLine) color_line_pattern_0 += Environment.NewLine;
                    if (isSecondSpriteLine) color_line_pattern_1 += Environment.NewLine;


                    //


                    if (isFirstSpriteLine) pattern_0_Index++;
                    if (isSecondSpriteLine) pattern_1_Index++;

                    lineNumber++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    errorCount++;
                }
            }

            if (errorCount > 0)
            {
                Console.WriteLine("ATTENTION: there were one or more errors. Stopping processing here.");

                return;
            }


            Console.WriteLine();
            Console.WriteLine("Palette, patterns and colors are also saved on .pal, .pat and .col files on output folder");
            Console.WriteLine("; pattern 0:");
            lineNumber = 0;
            foreach (var line in pattern_0)
            {
                Console.WriteLine(line + "\t; line #" + lineNumber++);
            }
            Console.WriteLine("; pattern 1:");
            lineNumber = 0;
            foreach (var line in pattern_1)
            {
                Console.WriteLine(line + "\t; line #" + lineNumber++);
            }

            Console.WriteLine("; color 0:");
            Console.WriteLine(color_line_pattern_0);
            Console.WriteLine("; color 1:");
            Console.WriteLine(color_line_pattern_1);


            // Show original palette RGB values
            Console.WriteLine();
            Console.WriteLine("Original palette RGB values:");
            for (int i = 0; i < originalPalette.Length; i += 2)
            {
                int red = (originalPalette[i] & 0b11110000) >> 4;
                int blue = (originalPalette[i] & 0b00001111);
                int green = originalPalette[i + 1];

                Console.WriteLine((i / 2) + ": " + red + ", " + green + ", " + blue);
            }


            if (!isPaletteValid)
            {
                // Convert input palette to palette found and save to file
                for (int i = 0; i < 16; i++)
                {
                    var indexOriginalPalette = i * 2;
                    var indexNewPalette = newPaletteFound[i] * 2;

                    paletteBytes[indexNewPalette] = originalPalette[indexOriginalPalette];
                    paletteBytes[indexNewPalette + 1] = originalPalette[indexOriginalPalette + 1];
                }


                // Show converted palette RGB values
                Console.WriteLine();
                Console.WriteLine("Converted palette RGB values:");
                for (int i = 0; i < paletteBytes.Length; i += 2)
                {
                    int red = (paletteBytes[i] & 0b11110000) >> 4;
                    int blue = (paletteBytes[i] & 0b00001111);
                    int green = paletteBytes[i + 1];

                    Console.WriteLine((i / 2) + ": " + red + ", " + green + ", " + blue);
                }
            }


            // create patterns output file
            index = 0;
            foreach (var line in pattern_0.Concat(pattern_1))
            {
                patternBytes[index] = Convert.ToByte(line, 2);

                index++;
            }


            // create colors output file
            index = 0;
            foreach (var line in (color_line_pattern_0 + color_line_pattern_1).Split(Environment.NewLine))
            {
                if (line != "")
                {
                    colorsBytes[index] = Convert.ToByte(line);

                    index++;
                }
            }
        }

        private static bool CheckIfOrColorIsPossible(IList<int> colors)
        {
            return ((colors[0] | colors[1]) == colors[2])
                || ((colors[0] | colors[2]) == colors[1])
                || ((colors[1] | colors[2]) == colors[0]);
        }

        private static void ReduceColorCountInLine(List<List<int>> paletteRGB, IList<int> line)
        {
            var colorCount = new Dictionary<int, int>();
            {
                // count how many repetitions of each color
                foreach (var pixel in line)
                {
                    if (pixel != 0)
                    {
                        if (!colorCount.Keys.Contains(pixel))
                        {
                            colorCount.Add(pixel, 0);
                        }

                        colorCount[pixel]++;
                    }
                }

                // get the least common color
                var oldColor = colorCount.First(x => x.Value == colorCount.Values.OrderBy(x => x).First()).Key;

                // get the most similar color in the line
                var oldColorRGB = paletteRGB[oldColor];
                double minimumDistance = Double.MaxValue;
                int newColor = -1;
                foreach (var colorIndex in colorCount.Keys.Where(x => x != oldColor))
                {
                    var colorRGB = paletteRGB[colorIndex];

                    var colorDistance = ColourDistance(colorRGB, oldColorRGB);
                    if (colorDistance < minimumDistance)
                    {
                        minimumDistance = colorDistance;
                        newColor = colorIndex;
                    }
                }

                if (newColor == -1) throw new InvalidDataException("Error while trying to reduce color count at line " + line);

                // replace old color by new color in the line
                for (int i = 0; i < line.Count; i++)
                {
                    if (line[i] == oldColor) line[i] = newColor;
                }
            }
        }

        //public static int GetSpriteNumber(int lineNumber, int pattern_0_Index, int colNumber, int sprite1_offsetX)
        //{
        //    // logic to calc which of the four 8x8 sprites this pixel belongs
        //    var lineIndex = (int)Math.Floor((decimal)pattern_0_Index / 8);
        //    var colIndex = (int)Math.Floor(((decimal)(colNumber - sprite1_offsetX)) / 8);
        //    //var colIndex = (int)Math.Floor((decimal)colNumber / 8);
        //    var spriteIndex = (colIndex * 2) + lineIndex;
        //    var patternIndex = (spriteIndex * 8) + (lineNumber % 8);
        //    return patternIndex;
        //}

        public static double ColourDistance(IList<int> c1, IList<int> c2)
        {
            // code taken from stackoverflow (with adaptations)

            long rmean = ((long)c1[0] + (long)c2[0]) / 2; ;
            long r = (long)c1[0] - (long)c2[0];
            long g = (long)c1[1] - (long)c2[1];
            long b = (long)c1[2] - (long)c2[2];

            return Math.Sqrt((((512 + rmean) * r * r) >> 8) + (4 * g * g) + (((767 - rmean) * b * b) >> 8));
        }

        private static void ExtractColorsFromLine(IList<int> line, int lineNumber, int sprite0_width, int sprite1_offsetX, out List<int> colorsInThisLine, out int color0, out int color1, out int orColor)
        {
            colorsInThisLine = line.Where(x => x != 0).Distinct().ToList();
            color0 = -1;
            color1 = -1;
            orColor = -1;
            switch (colorsInThisLine.Count)
            {
                case 1:
                    color0 = colorsInThisLine[0];
                    break;

                case 2:
                    color0 = colorsInThisLine[0];
                    color1 = colorsInThisLine[1];
                    break;

                // TODO: OR-color should be validated only where there is 2 sprites overlap, 
                // not on the pixels that belongs to only one sprite (when there is X offset fot second sprite)
                case 3:
                    var colorsInTheAreaOnlyFirstSprite = new List<int>();
                    var colorOfFirstSpriteIsFixed = false;
                    for (int i = 0; i < sprite1_offsetX; i++)
                    {
                        if (line[i] != 0) colorsInTheAreaOnlyFirstSprite.Add(line[i]);
                    }
                    colorsInTheAreaOnlyFirstSprite = colorsInTheAreaOnlyFirstSprite.Distinct().ToList();
                    if (colorsInTheAreaOnlyFirstSprite.Count == 1)
                    {
                        color0 = colorsInTheAreaOnlyFirstSprite[0];
                        colorOfFirstSpriteIsFixed = true;
                    }
                    if (colorsInTheAreaOnlyFirstSprite.Count > 1) throw new InvalidDataException("More than one color on first sprite only part if line #" + lineNumber);


                    var colorsInTheAreaOnlySecondSprite = new List<int>();
                    var colorOfSecondSpriteIsFixed = false;
                    for (int i = sprite0_width; i < line.Count; i++)
                    {
                        if (line[i] != 0) colorsInTheAreaOnlySecondSprite.Add(line[i]);
                    }
                    colorsInTheAreaOnlySecondSprite = colorsInTheAreaOnlySecondSprite.Distinct().ToList();
                    if (colorsInTheAreaOnlySecondSprite.Count == 1)
                    {
                        color1 = colorsInTheAreaOnlySecondSprite[0];
                        colorOfSecondSpriteIsFixed = true;
                    }
                    if (colorsInTheAreaOnlySecondSprite.Count > 1) throw new InvalidDataException("More than one color on second sprite only part if line #" + lineNumber);


                    var colorsInTheIntersection = new List<int>();
                    for (int i = 0; i < line.Count; i++)
                    {
                        if (line[i] != 0 && i >= sprite1_offsetX && i < sprite0_width) colorsInTheIntersection.Add(line[i]);
                    }
                    colorsInTheIntersection = colorsInTheIntersection.Distinct().ToList();

                    if (color0 == -1) color0 = colorsInTheIntersection[0];
                    if (color1 == -1) color1 = colorsInTheIntersection[1];

                    foreach (var color in colorsInTheIntersection)
                    {
                        if (color != color0 && color != color1) orColor = color;
                    }

                    //if (colorsInTheIntersection.Count == 1 && colorsInTheIntersection[0] != color0 && )
                    //{
                    //    color0 = colorsInTheIntersection[0];
                    //    break;
                    //}
                    //if (colorsInTheIntersection.Count == 2)
                    //{
                    //    // TODO: serious problem here 


                    //    color0 = colorsInTheIntersection[0];
                    //    color1 = colorsInTheIntersection[1];
                    //    break;
                    //}

                    // 3 colors:
                    if (colorsInTheIntersection.Count == 3 && !colorOfFirstSpriteIsFixed && !colorOfSecondSpriteIsFixed)
                    {
                        if ((colorsInTheIntersection[0] | colorsInTheIntersection[1]) == colorsInTheIntersection[2])
                        {
                            color0 = colorsInTheIntersection[0];
                            color1 = colorsInTheIntersection[1];
                            orColor = colorsInTheIntersection[2];
                        }
                        else if ((colorsInTheIntersection[2] | colorsInTheIntersection[1]) == colorsInTheIntersection[0])
                        {
                            color0 = colorsInTheIntersection[2];
                            color1 = colorsInTheIntersection[1];
                            orColor = colorsInTheIntersection[0];
                        }
                        else if ((colorsInTheIntersection[0] | colorsInTheIntersection[2]) == colorsInTheIntersection[1])
                        {
                            color0 = colorsInTheIntersection[0];
                            color1 = colorsInTheIntersection[2];
                            orColor = colorsInTheIntersection[1];
                        }
                        else
                        {
                            throw new InvalidDataException("OR-Color impossible on line #" + lineNumber);
                        }
                    }

                    break;

                default:
                    break;
            }
        }

        private static bool ShowSprite(IList<IList<int>> pixelsList, List<List<int>> listOf3Colors)
        {
            listOf3Colors.Clear();
            var valid = true;
            var moreThan16PixelsPerLine = false;
            var lineNumber = 0;
            foreach (var line in pixelsList)
            {
                Console.Write("Line #" + lineNumber.ToString().PadLeft(2, ' ') + ": ");
                //Console.Write(line.Count + " colors: ");
                if (line.Count != 16)
                {
                    //throw new InvalidDataException("Line #" + lineNumber + " has " + line.Count + " pixels. Should be 16.");
                    Console.Write("[" + line.Count + " pixels] ");
                    moreThan16PixelsPerLine = true;
                }

                foreach (var color in line)
                {
                    //if (color == 0)
                    //{
                    //    Console.BackgroundColor = ConsoleColor.Black;
                    //}
                    //else
                    //{
                    //    Console.BackgroundColor = ConsoleColor.Red;
                    //}
                    Console.BackgroundColor = (ConsoleColor)color;

                    Console.Write(color.ToString().PadLeft(2, ' ') + ",");
                }
                Console.BackgroundColor = ConsoleColor.Black;

                // get the distinct colors different than transparent on this line
                var distinctColorsOnLine = line.Where(x => x != 0).Distinct().ToList();

                Console.Write("; Distinct colors: " + distinctColorsOnLine.Count());

                if (distinctColorsOnLine.Count() > 3)
                {
                    valid = false;
                    //throw new InvalidDataException("Line #" + lineNumber + " has more than 3 colors.");
                }

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
                        valid = false;
                        Console.Write("; OR-color impossible");
                    }
                }

                Console.WriteLine();

                lineNumber++;
            }

            if (moreThan16PixelsPerLine) return true; // somewhat ugly code, but more readable

            return valid;
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
            IList<IList<int>> pixelsList, byte byteRead, bool leftPixel)
        {
            if (!leftPixel)
            {
                x++;
            }

            var xSprite = x - sprite0_offsetX;
            var ySprite = y - sprite0_offsetY;

            //if (x >= sprite0_offsetX && x <= totalLastX         // check if current (x, y) is inside the 16x16 sprite area
            //    && y >= sprite0_offsetY && y <= totalLastY)
            {
                if (x >= sprite0_offsetX && x <= usefulLastX    // check if current (x, y) is inside the width x height useful sprite area
                    && y >= sprite0_offsetY && y <= usefulLastY)
                {
                    // new line
                    if (xSprite == 0)
                    {
                        pixelsList.Add(new List<int>());
                    }

                    var highNibble = byteRead & 0b11110000;
                    var lowNibble = byteRead & 0b00001111;

                    var leftPixelColor = highNibble >> 4;
                    var rightPixelColor = lowNibble;

                    if (leftPixel)
                    {
                        pixelsList[ySprite].Add(leftPixelColor);
                    }
                    else
                    {
                        pixelsList[ySprite].Add(rightPixelColor);
                    }
                }
            }
        }

        /// <summary>
        /// Convert a .sc5 image file generated by BMP2MSX
        /// into a series of 16x16 sprites with 3 colors per line (OR-color)
        /// </summary>
        /// <param name="fileName"></param>
        //public static void Execute_old(string fileName)
        //{
        //    List<List<int>> palette = GetPaletteFromFile_ToRgb(fileName);


        //    // create a replacement color for each color of the palette (color most similar)
        //    var paletteReplacement = new List<int>();
        //    paletteReplacement.Add(-1); // first index is transparent color
        //    const int FIRST_COLOR = 1; // skip palette first entry (transparent)
        //    for (int i = FIRST_COLOR; i < 16; i++)
        //    {
        //        // calculate distance from this color to each other
        //        var distances = new List<int>();
        //        var smallestDistance = (7 + 7 + 7) + 1;
        //        var smallestDistanceIndex = 0;
        //        for (int j = FIRST_COLOR; j < 16; j++)
        //        {
        //            if (i != j)
        //            {
        //                const int RED = 0;
        //                const int BLUE = 1;
        //                const int GREEN = 2;
        //                var currentDistance =
        //                    Math.Abs(palette[i][RED] - palette[j][RED]) +
        //                    Math.Abs(palette[i][BLUE] - palette[j][BLUE]) +
        //                    Math.Abs(palette[i][GREEN] - palette[j][GREEN]);
        //                distances.Add(currentDistance);

        //                if (currentDistance < smallestDistance)
        //                {
        //                    smallestDistance = currentDistance;
        //                    smallestDistanceIndex = j;
        //                }
        //                else if (currentDistance == smallestDistance)
        //                {
        //                    // if two RGB distances are equal, the one with smaller individual distances should prevail
        //                    // Ex.: RGB distances (0, 2, 0) vs (1, 1, 0), the second one is the winner

        //                    var distancesFromIToJ = new int[] {
        //                        Math.Abs(palette[i][RED] - palette[j][RED]),
        //                        Math.Abs(palette[i][BLUE] - palette[j][BLUE]),
        //                        Math.Abs(palette[i][GREEN] - palette[j][GREEN])
        //                    };

        //                    var distancesFromIToSmallest = new int[] {
        //                        Math.Abs(palette[i][RED] - palette[smallestDistanceIndex][RED]),
        //                        Math.Abs(palette[i][BLUE] - palette[smallestDistanceIndex][BLUE]),
        //                        Math.Abs(palette[i][GREEN] - palette[smallestDistanceIndex][GREEN])
        //                    };

        //                    if (Enumerable.Max(distancesFromIToJ) < Enumerable.Max(distancesFromIToSmallest))
        //                    {
        //                        smallestDistanceIndex = j;
        //                    }
        //                }
        //            }
        //        }

        //        // put the color most similar in the replacement array
        //        paletteReplacement.Add(smallestDistanceIndex);
        //    }

        //    using (var input = File.OpenRead(fileName))
        //    using (var reader = new BinaryReader(input))
        //    using (var output = File.Create(fileName + ".new"))
        //    {
        //        var counter = 0;
        //        IList<int> colorsList = new List<int>();
        //        //var pixelsList = new List<int>();
        //        var foundCounter = 0;
        //        var notFoundCounter = 0;
        //        for (int j = 0; j < input.Length; j++)
        //        {
        //            var byteRead = reader.ReadByte();
        //            if (j >= HEADER_SIZE) // skip header
        //            {
        //                var highNibble = byteRead & 0xf0;
        //                var lowNibble = byteRead & 0b00001111;

        //                var leftPixelColor = highNibble >> 4;
        //                var rightPixelColor = lowNibble;

        //                //if (leftPixelColor != 0 && !colorsList.Contains(leftPixelColor)) colorsList.Add(leftPixelColor);
        //                //if (rightPixelColor != 0 && !colorsList.Contains(rightPixelColor)) colorsList.Add(rightPixelColor);

        //                if (leftPixelColor != 0) colorsList.Add(leftPixelColor);
        //                if (rightPixelColor != 0) colorsList.Add(rightPixelColor);

        //                counter++;
        //                if (counter == 8) // 8 bytes = 16 pixels on screen 5
        //                {
        //                    var colorsGrouped = colorsList
        //                        .GroupBy(x => x)
        //                        .Select(n => new { ColorNumber = n.Key, Count = n.Count() })
        //                        .OrderByDescending(x => x.Count)
        //                        .ToList();

        //                    if (colorsGrouped.Count == 3)
        //                    {
        //                        //var color1 = 0;
        //                        //var color2 = 0;
        //                        //var orColor = 0;
        //                        bool found = false;

        //                        found = CheckIfOrColorIsPossible(
        //                            colorsGrouped[0].ColorNumber,
        //                            colorsGrouped[1].ColorNumber,
        //                            colorsGrouped[2].ColorNumber
        //                            );

        //                        if (!found)
        //                        {
        //                            // try again, replacing each color by a replacement candidate
        //                            found = CheckIfOrColorIsPossible(
        //                                paletteReplacement[colorsGrouped[0].ColorNumber],
        //                                colorsGrouped[1].ColorNumber,
        //                                colorsGrouped[2].ColorNumber
        //                                );

        //                            if (!found)
        //                            {
        //                                found = CheckIfOrColorIsPossible(
        //                                    colorsGrouped[0].ColorNumber,
        //                                    paletteReplacement[colorsGrouped[1].ColorNumber],
        //                                    colorsGrouped[2].ColorNumber
        //                                    );
        //                            }

        //                            if (!found)
        //                            {
        //                                found = CheckIfOrColorIsPossible(
        //                                    colorsGrouped[0].ColorNumber,
        //                                    colorsGrouped[1].ColorNumber,
        //                                    paletteReplacement[colorsGrouped[2].ColorNumber]
        //                                    );
        //                            }

        //                        }

        //                        if (!found)
        //                        {
        //                            notFoundCounter++;
        //                        }
        //                        else
        //                        {
        //                            foundCounter++;
        //                        }
        //                    }
        //                    else if (colorsGrouped.Count > 3)
        //                    {
        //                        // loop from color with less repetitions upwards
        //                        // replacing by the others until find a combination
        //                        // valid to form 3 colors with OR-color
        //                        foreach (var item in colorsGrouped)
        //                        {

        //                        }
        //                    }

        //                    colorsList.Clear();
        //                    counter = 0;
        //                }
        //            }
        //        }

        //        //var buffer = new byte[4096 * 4]; // 16 kb page

        //        //// only one page
        //        //var actual = reader.Read(buffer, 0, buffer.Length);
        //        //output.Write(buffer, 0, actual);
        //    }
        //}

        private static List<List<int>> GetPaletteFromFile_ToRgb(FileStream input, BinaryReader reader)
        {
            // get palette from file (last 32 bytes)
            byte[] paletteBytes = new byte[32];
            //using (var input = File.OpenRead(fileName))
            //using (BinaryReader reader = new BinaryReader(input))
            //{
                reader.BaseStream.Seek(input.Length - 32, SeekOrigin.Begin);
                reader.Read(paletteBytes, 0, 32);
            //}
            var palette = new List<List<int>>();
            for (int i = 0; i < 32; i += 2)
            {
                int red = (paletteBytes[i] & 0b11110000) >> 4;
                int blue = (paletteBytes[i] & 0b00001111);
                int green = paletteBytes[i + 1];

                palette.Add(new List<int> { red, green, blue });
            }

            return palette;
        }

        private static byte[] GetPaletteFromFile_ToBytes(FileStream input, BinaryReader reader)
        {
            // get palette from file (last 32 bytes)
            byte[] paletteBytes = new byte[32];
            //using (var input = File.OpenRead(fileName))
            //using (BinaryReader reader = new BinaryReader(input))
            //{
                reader.BaseStream.Seek(input.Length - 32, SeekOrigin.Begin);
                reader.Read(paletteBytes, 0, 32);
            //}
            
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

        // https://stackoverflow.com/questions/12249051/unique-combinations-of-list
        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> items, int count)
        {
            int i = 0;
            foreach (var item in items)
            {
                if (count == 1)
                    yield return new T[] { item };
                else
                {
                    //foreach (var result in GetPermutations(items.Skip(i + 1), count - 1))
                    foreach (var result in GetPermutations(items.Except(new[] { item }), count - 1))
                            yield return new T[] { item }.Concat(result);
                }

                ++i;
            }
        }
    }
}
