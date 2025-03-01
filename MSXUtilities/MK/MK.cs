using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MSXUtilities.MK
{
    public class MK_Class
    {
        string inputFile;
        byte[] file;

        public MK_Class(string _inputFile)
        {
            this.inputFile = _inputFile;
            
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

        public void Run(int startX, int startY, int width, int height, int megaROMpage, string name)
        {
            Console.WriteLine("Starting frame " + name);

            //int startX = 0; // x in bytes // second frame: 0x1e (30)
            //int startY = 0; // y in pixels
            int endX = startX + width;
            int endY = startY + height;

            int currentPosition;
            int currentIncrement = 0;
            int lastPosition = 0;
            int dataAddress = 0; // 0x8000; // base address
            bool newSlice = true;
            bool firstListEntry = true;

            StringBuilder outputHeader = new StringBuilder();
            StringBuilder outputList = new StringBuilder();
            StringBuilder outputData = new StringBuilder();

            IList<int> colorsUsed = new List<int>();


            IList<byte> currentSlice = new List<byte>();

            // ignore empty lines
            var yValid = startY;
            //for (int y = startY; y < endY; y++)
            //{
            //    var emptyLine = true;
            //    for (int x = startX; x < endX; x++)
            //    {
            //        if (file[(y * 128) + x] != 0x88)
            //        {
            //            emptyLine = false;
            //            break;
            //        }
            //    }
            //    if (!emptyLine)
            //    {
            //        yValid = y;
            //        break;
            //    }
            //}

            for (int y = yValid; y < endY; y++)
            {
                for (int x = startX; x < endX; x++)
                {
                    currentPosition = (y * 128) + x;
                    var b = file[currentPosition];

                    if (b != 0x88) // 0x88 = double transparent pixels
                    {
                        // convert hi or low nibbles from transparent
                        // to black pixels
                        var hiNibble = b & 0b11110000;
                        var lowNibble = b & 0b00001111;
                        if (hiNibble == 0x80)
                        {
                            b = (byte)(0x30 | lowNibble);
                        }
                        else if (lowNibble == 0x08)
                        {
                            b = (byte)(hiNibble | 0x03);
                        }

                        var hiNibbleAdjusted = hiNibble >> 4;
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

                                //dw yOffset; db width; db height; db MEgaROM page number
                                outputHeader.AppendLine(";\t\tyOffset\twidth\theight\tmegaROM page");
                                outputHeader.AppendLine(String.Format("\tdw\t{0}\tdb\t{1},\t{2},\t{3}",
                                    yOffset,
                                    width * 2, // width in pixels
                                    height,
                                    megaROMpage
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

                            //if (currentIncrement > 255)
                            //{
                            //    currentIncrement = 0b01111111 & currentIncrement; // each line is 128 bytes long
                            //    //throw new Exception("currentIncrement should be less than 256");
                            //}

                            if (firstListEntry)
                            {
                                currentIncrement -= startX;
                                firstListEntry = false;
                            }

                            outputList.AppendLine(String.Format("\tdb\t{0},\t{1}\tdw\t{2}",
                                currentIncrement,
                                currentSlice.Count,
                                dataAddress));

                            outputData.Append("\tdb");
                            var first = true;
                            foreach (var item in currentSlice)
                            {
                                if (!first) outputData.Append(",");
                                first = false;
                                outputData.Append(String.Format("\t{0}",
                                    item));
                            }
                            outputData.AppendLine();


                            newSlice = true;
                            dataAddress += currentSlice.Count;
                            currentSlice = new List<byte>();
                        }
                    }
                }
            }

            outputList.AppendLine("db  0 ; end of frame");

            File.WriteAllText(name + "_header.s", outputHeader.ToString());
            File.WriteAllText(name + "_list.s", outputList.ToString());
            File.WriteAllText(name + "_data.s", outputData.ToString());

            Console.Write("Colors used:");
            foreach (var color in colorsUsed.OrderBy(x => x)) Console.Write(" " + color);
            Console.WriteLine();

            Console.WriteLine(name +  " done.");
            Console.WriteLine();
        }
    }
}
