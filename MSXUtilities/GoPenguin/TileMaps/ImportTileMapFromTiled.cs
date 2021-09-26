using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace MSXUtilities.GoPenguin.TileMaps
{
    public static class ImportTileMapFromTiled
    {
        public static List<List<int>> Execute(string tileMapFilePath, string baseLabel)
        {
            var output = new List<List<int>>();
            var file = File.ReadAllText(tileMapFilePath);

            var startLabel = baseLabel + "_Start";
            var bgObjectsText = startLabel + ":" + Environment.NewLine + Environment.NewLine;

            var lines = file.Split(Environment.NewLine).ToList();
            lines = lines.Where(x => x.Trim() != "").ToList();
            if (lines.Count() != 12) throw new InvalidDataException("Tilemap file must have exactly 12 lines");

            int x = 0, y = 0;
            var arrayBgObjectsScreens = new string[16];
            var arrayBgObjectsObjectsperScreen = new int[16];
            foreach (var line in lines)
            {
                var outputLine = new List<int>();

                var arrayLine = line.Split(",");
                if (arrayLine.Count() != 256) throw new InvalidDataException("Each line of the tilemap file must have exactly 256 tiles");

                const string BASE_STRUCT_BGOBJECTS =         "\tdb      {0},     {1},          {2} * 2 * 8,      1,  0,  {3},    0,  0,  0,  0,  0,  0,  0,  0,  0,  0";
                const string BASE_STRUCT_BGOBJECTS_ENEMY_B = "\tdb      {0},     {1},          {2} * 2 * 8,      1,  0,  {3},    ({2} * 2 * 8) {4},  ({2} * 2 * 8) {5},  0,  0,  0,  0,  0,  0,  0,  0";

                x = 0;
                foreach (var item in arrayLine)
                {
                    int currentScreen = x / 16;

                    var intValueConverted = 0;
                    switch (int.Parse(item))
                    {
                        case -1:
                            intValueConverted = 0;
                            break;

                        case 0:
                            // Big Bricks
                            intValueConverted = 2;
                            break;

                        case 1:
                            // Small Bricks
                            intValueConverted = 1;
                            break;

                        case 2:
                            // Diamond
                            arrayBgObjectsScreens[currentScreen] +=
                                String.Format(BASE_STRUCT_BGOBJECTS + Environment.NewLine,
                                x,
                                "DIAMOND_FIRST_TILE",
                                y,
                                0
                                );
                            arrayBgObjectsObjectsperScreen[currentScreen]++;
                            break;

                        case 3:
                            // Grass
                            intValueConverted = 3;
                            break;

                        case 4:
                            // Rocks
                            intValueConverted = 4;
                            break;

                        case 6:
                            // Ladybug
                            arrayBgObjectsScreens[currentScreen] +=
                                String.Format(BASE_STRUCT_BGOBJECTS + Environment.NewLine,
                                x,
                                "ENEMY_TYPE_A",
                                y,
                                "ENEMY_TYPE_LADYBUG_LEFT"
                                );
                            arrayBgObjectsObjectsperScreen[currentScreen]++;
                            break;

                        case 7:
                            // Snail
                            arrayBgObjectsScreens[currentScreen] +=
                                String.Format(BASE_STRUCT_BGOBJECTS + Environment.NewLine,
                                x,
                                "ENEMY_TYPE_A",
                                y,
                                "ENEMY_TYPE_SNAIL_LEFT"
                                );
                            arrayBgObjectsObjectsperScreen[currentScreen]++;
                            break;

                        case 8:
                            // Armadillo
                            arrayBgObjectsScreens[currentScreen] +=
                                String.Format(BASE_STRUCT_BGOBJECTS_ENEMY_B + Environment.NewLine,
                                x,
                                "ENEMY_TYPE_B",
                                y,
                                "ENEMY_TYPE_ARMADILLO_LEFT",
                                "+ 9",
                                "- 7"
                                );
                            arrayBgObjectsObjectsperScreen[currentScreen]++;
                            break;

                        case 9:
                            // Dino
                            arrayBgObjectsScreens[currentScreen] +=
                                String.Format(BASE_STRUCT_BGOBJECTS_ENEMY_B + Environment.NewLine,
                                x,
                                "ENEMY_TYPE_B",
                                y,
                                "ENEMY_TYPE_DINO_LEFT",
                                "+ 14",
                                "- 5"
                                );
                            arrayBgObjectsObjectsperScreen[currentScreen]++;
                            break;

                        case 10:
                            // Elephant
                            arrayBgObjectsScreens[currentScreen] +=
                                String.Format(BASE_STRUCT_BGOBJECTS_ENEMY_B + Environment.NewLine,
                                x,
                                "ENEMY_TYPE_B",
                                y,
                                "ENEMY_TYPE_ELEPHANT_LEFT",
                                "+ 15",
                                "- 5"
                                );
                            arrayBgObjectsObjectsperScreen[currentScreen]++;
                            break;

                        case 11:
                            // Centipede
                            arrayBgObjectsScreens[currentScreen] +=
                                String.Format(BASE_STRUCT_BGOBJECTS_ENEMY_B + Environment.NewLine,
                                x,
                                "ENEMY_TYPE_B",
                                y,
                                "ENEMY_TYPE_CENTIPEDE_LEFT",
                                "+ 15",
                                "- 4"
                                );
                            arrayBgObjectsObjectsperScreen[currentScreen]++;
                            break;


                        default:
                            //intValueConverted = 0;
                            break;
                    }
                    
                    if (arrayBgObjectsObjectsperScreen[currentScreen] > 16)
                    {
                        throw new InvalidDataException(String.Format("Screen number {0} has more than 16 dynamic objects (e.g. Diamonds, enemies, etc)", currentScreen));
                    }

                    int? previousItem = null;
                    foreach (var outputItem in outputLine)
                    {
                        if (previousItem != null && outputItem != 0 && previousItem != 0 && outputItem != previousItem) 
                        {
                            throw new InvalidDataException(
                                String.Format(
                                    "It's not allowed two different blocks together. Column: {0}, Line: {1}.",
                                    x, 
                                    y
                                ));
                        }

                        previousItem = outputItem;
                    }

                    outputLine.Add(intValueConverted);

                    x++;
                }

                output.Add(outputLine);

                y++;
            }

            // Save BgObjects file
            int index = 0;
            foreach (var item in arrayBgObjectsScreens)
            {
                bgObjectsText += "; Screen #" + index + Environment.NewLine;
                bgObjectsText += item;
                bgObjectsText += String.Format(
                    "\tds     256 - ($ - ({0} + {1})), 0                 ; fill with 0s until end of block" + Environment.NewLine + Environment.NewLine,
                    startLabel,
                    index * 256
                    );
                index++;
            }
            bgObjectsText += baseLabel + "_End:" + Environment.NewLine;
            File.WriteAllText("BgObjects.s", bgObjectsText);

            return output;
        }

    }
}
