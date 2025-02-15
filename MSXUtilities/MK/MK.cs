using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MSXUtilities.MK
{
    public class MK_Class
    {
        string inputFile;

        public MK_Class(string _inputFile)
        {
            this.inputFile = _inputFile;
        }

        public void Run(int startX, int startY, int width, int height, string name)
        {

            //int startX = 0; // x in bytes // second frame: 0x1e (30)
            //int startY = 0; // y in pixels
            int endX = startX + width;
            int endY = startY + height;

            int currentPosition = 0;
            int currentIncrement = 0;
            int lastPosition = 0;
            int dataAddress = 0; // 0x8000; // base address
            bool newSlice = true;

            StringBuilder outputList = new StringBuilder();
            StringBuilder outputData = new StringBuilder();

            byte[] file = File.ReadAllBytes(inputFile);

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
                            // set increment, length, and address
                            if (currentIncrement > 255)
                            {
                                currentIncrement = 0xff & currentIncrement;

                                //throw new Exception("currentIncrement should be less than 256");
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

            File.WriteAllText(name + "_list.s", outputList.ToString());
            File.WriteAllText(name + "_data.s", outputData.ToString());

            Console.WriteLine(name +  " done.");
        }
    }
}
