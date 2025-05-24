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
        //IList<byte> outputDataBytesOptimized = new List<byte>();
        IList<byte> outputDataBytesAll = new List<byte>();
        int outputListTotalSize = 0;
        int? dataOffset_AddFile = null; // used together with AddFile method
        int dataAddress = 0;

        #region constructors

        public MK_Main(string _inputFile, string _destinyFolder)
        {
            this.inputFile = _inputFile;

            this.file = File.ReadAllBytes(inputFile);

            this.file = RemoveFileHeader(this.file);

            this.destinyFolder = _destinyFolder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_inputFiles">Images must be 256x256</param>
        /// <param name="_destinyFolder"></param>
        public MK_Main(string[] _inputFiles, string _destinyFolder)
        {
            // Warning: Images must be 256x256

            List<byte> tempList = new List<byte>();

            foreach (var item in _inputFiles)
            {
                var temp = File.ReadAllBytes(item);

                temp = RemoveFileHeader(temp);

                IList<byte> t = (IList<byte>)temp;

                tempList.AddRange(t);
            }

            this.file = tempList.ToArray();

            this.destinyFolder = _destinyFolder;
        }

        #endregion constructors

        private byte[] RemoveFileHeader(byte[] _file)
        {
            /*
                Byte: #FE (Type of file)
                Word: Begin address of file
                Word: End address of file
                Word: Start address of file (Only important when ",R" parameter is defined with BLOAD)             
            */

            // remove the 7 bytes header, if present
            if (_file[0] == 0xfe)
            {
                var temp = _file.ToList<byte>();
                for (int i = 0; i < 7; i++)
                {
                    temp.RemoveAt(0);
                }
                
                return temp.ToArray<byte>();
            }

            return _file;
        }

        public void Run(
            int startX,
            int startY,
            int width,
            int height,
            string megaROMpage,
            string characterName,
            string position,
            string side,
            int frameNumber,
            bool clearDataFile = false
            )
        {
            if ((startX + width) > 128) throw new ArgumentException("startX + width cannot be over 128 bytes (256 pixels)");

            var frameFullName_underscores = String.Format("{0}_{1}_{2}_frame_{3}", characterName, position.Replace('-', '_'), side, frameNumber);
            var frameFullName_underscores_pascalCase = String.Format("{0}_{1}_{2}_Frame_{3}", characterName.ToPascalCase(), position.Replace('-', '_').ToPascalCase(true), side.ToPascalCase(), frameNumber);
            
            Console.WriteLine("Starting frame " + frameFullName_underscores);

            int endX = startX + width;
            int endY = startY + height;


            if (side == "right")
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
            //int dataAddress = outputDataBytesAll.Count;
            bool newSlice = true;
            bool firstListEntry = true;

            // when two or more positions come from the same image source, dataFile should be reset:
            if (clearDataFile)
            {
                outputDataBytesAll.Clear();
            }

            StringBuilder outputHeader = new StringBuilder();
            StringBuilder outputList = new StringBuilder();
            //StringBuilder outputData = new StringBuilder();

            IList<int> colorsUsed = new List<int>();
            int slicesCount = 0;
            int totalSliceData = 0;


            IDictionary<int, int> colorsCount = new Dictionary<int, int>();

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

                        if (!colorsCount.Keys.Contains(hiNibbleAdjusted))
                        {
                            colorsCount.Add(hiNibbleAdjusted, 1);
                        }
                        else
                        {
                            colorsCount[hiNibbleAdjusted]++;
                        }

                        if (!colorsCount.Keys.Contains(lowNibble))
                        {
                            colorsCount.Add(lowNibble, 1);
                        }
                        else
                        {
                            colorsCount[lowNibble]++;
                        }

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

                                //var tempArray = name.Split('_');
                                ////var frameNumber = tempArray[tempArray.Length - 1];

                                //var characterName = tempArray[0].ToPascalCase();

                                //dw yOffset; db width; db height; db MegaROM page number; dw List Address
                                outputHeader.AppendLine(";\t\tyOffset\twidth\theight\tmegaROM page\tList Address");
                                outputHeader.AppendLine(String.Format("\tdw\t{0}\tdb\t{1},\t{2},\t{3}\tdw\t{4}",
                                    yOffset,
                                    width * 2, // width in pixels
                                    height,
                                    megaROMpage,
                                    //characterName + "_" + position + "_" + side + "_Frame_" + frameNumber + ".List"
                                    frameFullName_underscores_pascalCase + ".List"
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

                            var temp = (dataOffset_AddFile == null) ? "" : " + " + dataOffset_AddFile;

                            if (currentSlice.Count > width)
                            {
                                throw new Exception("Warning: slice size greater than width");
                            }

                            outputList.AppendLine(String.Format("\tdb\t{0},\t{1}\tdw\t{2}",
                                currentIncrement,
                                currentSlice.Count, // TODO: low byte of address of unrolled OUTIs list 
                                DATA_BASE_ADDRESS + temp + " + "  + this.dataAddress));

                            slicesCount++;
                            totalSliceData += currentSlice.Count;

                            outputListTotalSize += 4; // 4 = size of each list entry

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

                            //// check if current slice is already in the data
                            //bool found = false;
                            //for (int i = 0; i < outputDataBytesOptimized.Count - currentSlice.Count; i++)
                            //{
                            //    for (int j = 0; j < currentSlice.Count; j++)
                            //    {
                            //        if (currentSlice[j] == outputDataBytesOptimized[i + j])
                            //        {
                            //            found = true;
                            //        }
                            //        else
                            //        {
                            //            found = false;
                            //            break;
                            //        }
                            //    }
                            //}
                            //if (!found)
                            //{
                            //    //Console.Write("-");
                            //    for (int j = 0; j < currentSlice.Count; j++)
                            //    {
                            //        outputDataBytesOptimized.Add(currentSlice[j]);
                            //    }
                            //}
                            //else
                            //{
                            //    //Console.Write("V");
                            //}

                            newSlice = true;
                            this.dataAddress += currentSlice.Count;
                            currentSlice = new List<byte>();
                        }
                    }
                }
            }

            outputList.AppendLine("\tdb  0 ; end of frame");
            this.outputListTotalSize += 1;

            File.WriteAllText(destinyFolder + frameFullName_underscores + "_header.s", outputHeader.ToString());
            File.WriteAllText(destinyFolder + frameFullName_underscores + "_list.s", outputList.ToString());

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
            Console.Write("Colors used (" + colorsUsed.Count + " colors):");
            foreach (var color in colorsUsed.OrderBy(x => x)) Console.Write(" " + color);
            Console.WriteLine();
            foreach (var item in colorsCount.OrderBy(x => x.Key))
            {
                Console.WriteLine(item.Key + ": " + item.Value);
            }

            Console.WriteLine("Slices count: " + slicesCount);
            Console.WriteLine("Average slice size: " + ((double)totalSliceData / (double)slicesCount));

            Console.WriteLine("--- Stats for all frames so far:");
            //Console.WriteLine("Data optimized size: " + outputDataBytesOptimized.Count + " bytes");
            Console.WriteLine("Data all size: " + outputDataBytesAll.Count + " bytes");

            Console.WriteLine("Total list size: " + outputListTotalSize + " bytes");

            var megaRomSize = this.dataAddress + this.outputListTotalSize;

            Console.WriteLine("Space remaining on MegaROM page: " + (16384 - megaRomSize) + " bytes");

            if (megaRomSize > 16384)
            {
                throw new Exception("Data + list will not fit into a MegaROM page.");
            }


            Console.WriteLine("Done.");
            Console.WriteLine();
        }

        public void SaveReferenceFiles(string name, int firstFrame, int lastFrame, int animationRepeatFrames)
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

            var nameWithUnderscores = name.Replace('-', '_');

            var data_fileName = destinyFolder + nameWithUnderscores.ToLower() + String.Format("_frames_{0}_to_{1}_data.s", firstFrame, lastFrame);
            File.WriteAllText(data_fileName, outputData.ToString());

            Console.WriteLine("Data saved to file: " + data_fileName);
            Console.WriteLine();

            //-------------------------------------------------------------

            StringBuilder outputHeaders = new StringBuilder();
            //StringBuilder outputData = new StringBuilder();
            StringBuilder outputList = new StringBuilder();
            StringBuilder outputAnimation = new StringBuilder();

            //outputData1.AppendLine(String.Format("{0}_Frames_{1}_to_{2}_Data:", nameWithUnderscores.ToPascalCase(true), firstFrame, lastFrame));

            var folderName = name.ToLower().Replace('_', '/');

            //outputData1.AppendLine(String.Format("\tINCLUDE \"Data/{0}/{1}_frames_{2}_to_{3}_data.s\"", folderName, nameWithUnderscores.ToLower(), firstFrame, lastFrame));

            //outputDataAndList.AppendLine("; ------------------------------------------------------------------------");



            outputAnimation.AppendLine(String.Format("{0}_Animation_Headers:", nameWithUnderscores.ToPascalCase(true)));



            for (int i = firstFrame; i <= lastFrame; i++)
            {
                outputHeaders.AppendLine(String.Format("{0}_Frame_{1}_Header:      INCLUDE \"Data/{2}/{3}_frame_{1}_header.s\"", nameWithUnderscores.ToPascalCase(true), i, folderName, nameWithUnderscores.ToLower()));
                outputHeaders.AppendLine();

                outputList.AppendLine(String.Format("{0}_Frame_{1}:", nameWithUnderscores.ToPascalCase(true), i));
                outputList.AppendLine(String.Format("\t\t.List:      INCLUDE \"Data/{0}/{1}_frame_{2}_list.s\"", folderName, nameWithUnderscores.ToPascalCase(true).ToLower(), i));
                outputList.AppendLine();

                outputAnimation.Append(String.Format("\tdw {0}_Frame_{1}_Header", nameWithUnderscores.ToPascalCase(true), i));
                if (animationRepeatFrames > 1)
                {
                    for (int j = 1; j < animationRepeatFrames; j++)
                    {
                        outputAnimation.Append(String.Format(", {0}_Frame_{1}_Header", nameWithUnderscores.ToPascalCase(true), i));
                    }
                }
                outputAnimation.AppendLine();
                //outputAnimation.AppendLine();
            }

            outputAnimation.AppendLine("\tdw 0x0000 ; end of data");

            var list_fileName = destinyFolder + nameWithUnderscores.ToLower() + String.Format("_frames_{0}_to_{1}_list.s", firstFrame, lastFrame);
            var header_fileName = destinyFolder + nameWithUnderscores.ToLower() + "_frame_headers.s";
            var animation_fileName = destinyFolder + nameWithUnderscores.ToLower() + "_animation.s";

            File.WriteAllText(list_fileName, outputList.ToString());
            File.WriteAllText(header_fileName, outputHeaders.ToString());
            File.WriteAllText(animation_fileName, outputAnimation.ToString());

            Console.WriteLine("List saved to file: " + list_fileName);
            Console.WriteLine("Headers saved to file: " + header_fileName);
            Console.WriteLine("Animation saved to file: " + animation_fileName);
            Console.WriteLine();
        }

        /// <summary>
        /// Add image, keeping current pointers (to add two or more source images going to separate animations, on the same MegaROM page)
        /// </summary>
        /// <param name="_inputFile"></param>
        public void AddImage(string _inputFile)
        {
            this.inputFile = _inputFile;

            this.file = File.ReadAllBytes(inputFile);

            this.file = RemoveFileHeader(this.file);




            this.dataOffset_AddFile = this.outputDataBytesAll.Count;

            this.outputDataBytesAll.Clear();
            //this.outputDataBytesOptimized.Clear();
        }
    }
}
