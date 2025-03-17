using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MSXUtilities.MK
{
    public class MK_Main
    {
        string inputFile;
        string destinyFolder;
        byte[] file;
        IList<byte> outputDataBytesOptimized;
        IList<byte> outputDataBytesAll;
        int outputListTotalSize;

        public MK_Main(string _inputFile, string _destinyFolder)
        {
            this.inputFile = _inputFile;
            this.destinyFolder = _destinyFolder;


            this.outputDataBytesOptimized = new List<byte>();
            this.outputDataBytesAll = new List<byte>();
            this.outputListTotalSize = 0;

            this.file = File.ReadAllBytes(inputFile);

            /*
                Byte: #FE (Type of file)
                Word: Begin address of file
                Word: End address of file
                Word: Start address of file (Only important when ",R" parameter is defined with BLOAD)             
            */

            // remove the 7 bytes header, if present
            if (file[0] == 0xfe)
            {
                var temp = file.ToList<byte>();
                for (int i = 0; i < 7; i++)
                {
                    temp.RemoveAt(0);
                }

                this.file = temp.ToArray<byte>();
            }

        }

        public void Run(
            int startX,
            int startY,
            int width,
            int height,
            string megaROMpage,
            string name,
            bool mirror
            )
        {
            Console.WriteLine("Starting frame " + name);

            int endX = startX + width;
            int endY = startY + height;

            if (mirror)
            {
                for (int y = startY; y < endY; y++)
                {
                    IList<byte> lineReversed = new List<byte>();
                    for (int x = startX; x < endX; x++)
                    {
                        //get byte, invert nibbles and add to the start of the new line
                        int current = (y * 128) + x;
                        var b = file[current];

                        var hiNibble = b & 0b11110000;
                        var lowNibble = b & 0b00001111;

                        b = (byte)((lowNibble << 4) | (hiNibble >> 4));

                        lineReversed.Insert(0, b);
                    }

                    // replace old line with the new one
                    var index = 0;
                    for (int x = startX; x < endX; x++)
                    {
                        int current = (y * 128) + x;
                        file[current] = lineReversed[index];
                        index++;
                    }

                }
            }

            const string DATA_BASE_ADDRESS = "0x8000";
            int currentPosition;
            int currentIncrement = 0;
            int lastPosition = 0;
            //int dataAddress = 0;
            int dataAddress = outputDataBytesAll.Count;
            bool newSlice = true;
            bool firstListEntry = true;

            StringBuilder outputHeader = new StringBuilder();
            StringBuilder outputList = new StringBuilder();
            //StringBuilder outputData = new StringBuilder();

            IList<int> colorsUsed = new List<int>();
            int slicesCount = 0;
            int totalSliceData = 0;


            IList<byte> currentSlice = new List<byte>();

            for (int y = startY; y < endY; y++)
            {
                for (int x = startX; x < endX; x++)
                {
                    currentPosition = (y * 128) + x;
                    var b = file[currentPosition];

                    if (b != 0x88) // 0x88 = double transparent pixels
                    {
                        // convert hi or low nibbles from transparent (color index 8)
                        // to the color index 0
                        var hiNibble = b & 0b11110000;
                        var lowNibble = b & 0b00001111;
                        
                        var hiNibbleAdjusted = hiNibble >> 4;
                        //var lowNibbleAdjusted = lowNibble << 4;

                        if (hiNibble == 0x80)
                        {
                            b = (byte)(0x00 | lowNibble);
                        }
                        else if (lowNibble == 0x08)
                        {
                            b = (byte)(hiNibble | 0x00);
                        }

                        if (!colorsUsed.Contains(hiNibbleAdjusted)) colorsUsed.Add(hiNibbleAdjusted);
                        if (!colorsUsed.Contains(lowNibble)) colorsUsed.Add(lowNibble);

                        currentSlice.Add(b);

                        if (newSlice)
                        {
                            currentIncrement = currentPosition - lastPosition;
                            lastPosition = currentPosition;
                            newSlice = false;
                        }
                        //Console.WriteLine(file[i]);
                    }
                    else
                    {
                        if (!newSlice)
                        {
                            // --------- set increment, length, and address

                            if (firstListEntry)
                            {
                                int blankLines = (currentIncrement - (startY * 128)) / 128;
                                int yOffset = blankLines * 128;

                                var tempArray = name.Split('_');
                                var frameNumber = tempArray[tempArray.Length - 1];

                                //dw yOffset; db width; db height; db MegaROM page number; dw List Address
                                outputHeader.AppendLine(";\t\tyOffset\twidth\theight\tmegaROM page\tList Address");
                                outputHeader.AppendLine(String.Format("\tdw\t{0}\tdb\t{1},\t{2},\t{3}\tdw\t{4}",
                                    yOffset,
                                    width * 2, // width in pixels
                                    height,
                                    megaROMpage,
                                    "Subzero_Stance_Right_Frame_" + frameNumber + ".List"
                                    )
                                );

                                // get only 7 lower bits
                                currentIncrement = 0b01111111 & currentIncrement;
                            }
                            else
                            {
                                // get only 8 lower bits
                                currentIncrement = 0b11111111 & currentIncrement;
                            }

                            if (firstListEntry)
                            {
                                currentIncrement -= startX;
                                firstListEntry = false;
                            }

                            outputList.AppendLine(String.Format("\tdb\t{0},\t{1}\tdw\t{2}",
                                currentIncrement,
                                currentSlice.Count,
                                DATA_BASE_ADDRESS + " + "  + dataAddress));

                            slicesCount++;
                            totalSliceData += currentSlice.Count;

                            outputListTotalSize += 4; //size of each list entry

                            //outputData.Append("\tdb");
                            //var first = true;
                            foreach (var item in currentSlice)
                            {
                                //if (!first) outputData.Append(",");
                                //first = false;
                                //outputData.Append(String.Format("\t{0}",
                                //    item));

                                outputDataBytesAll.Add(item);
                            }
                            //outputData.AppendLine();

                            // check if current slice is already in the data
                            bool found = false;
                            for (int i = 0; i < outputDataBytesOptimized.Count - currentSlice.Count; i++)
                            {
                                for (int j = 0; j < currentSlice.Count; j++)
                                {
                                    if (currentSlice[j] == outputDataBytesOptimized[i + j])
                                    {
                                        found = true;
                                    }
                                    else
                                    {
                                        found = false;
                                        break;
                                    }
                                }
                            }
                            if (!found)
                            {
                                //Console.Write("-");
                                for (int j = 0; j < currentSlice.Count; j++)
                                {
                                    outputDataBytesOptimized.Add(currentSlice[j]);
                                }
                            }
                            else
                            {
                                //Console.Write("V");
                            }

                            newSlice = true;
                            dataAddress += currentSlice.Count;
                            currentSlice = new List<byte>();
                        }
                    }
                }
            }

            outputList.AppendLine("\tdb  0 ; end of frame");

            File.WriteAllText(destinyFolder + name + "_header.s", outputHeader.ToString());
            File.WriteAllText(destinyFolder + name + "_list.s", outputList.ToString());

            //outputData.Append("\tdb");
            //var first = true;
            //foreach (byte item in outputDataBytesAll)
            //{
            //    if (!first) outputData.Append(",");
            //    first = false;
            //    outputData.Append(String.Format("\t{0}",
            //        item));
            //}
            //outputData.AppendLine();
            //File.WriteAllText(destinyFolder + name + "_data.s", outputData.ToString());

            Console.WriteLine("--- Stats for this frame:");
            Console.Write("Colors used:");
            foreach (var color in colorsUsed.OrderBy(x => x)) Console.Write(" " + color);
            Console.WriteLine();

            Console.WriteLine("Slices count: " + slicesCount);
            Console.WriteLine("Average slice size: " + ((double)totalSliceData / (double)slicesCount));

            Console.WriteLine("--- Stats for all frames so far:");
            Console.WriteLine("Data optimized size: " + outputDataBytesOptimized.Count + " bytes");
            Console.WriteLine("Data all size: " + outputDataBytesAll.Count + " bytes");

            Console.WriteLine("Total list size: " + outputListTotalSize + " bytes");
            Console.WriteLine("Space remeining on a MegaROM page: " + (16384 - (outputDataBytesAll.Count + outputListTotalSize)) + " bytes");

            if ((outputDataBytesAll.Count + outputListTotalSize) > 16384)
            {
                throw new Exception("Data + list will not fit into a MegaROM page.");
            }


            Console.WriteLine(name +  " done.");
            Console.WriteLine();
        }

        public void SaveDataFile(string name)
        {
            StringBuilder outputData = new StringBuilder();

            outputData.Append("\tdb");
            var first = true;
            foreach (byte item in outputDataBytesAll)
            {
                if (!first) outputData.Append(",");
                first = false;
                outputData.Append(String.Format("\t{0}",
                    item));
            }
            outputData.AppendLine();
            File.WriteAllText(destinyFolder + name + "_data.s", outputData.ToString());
        }
    }
}
