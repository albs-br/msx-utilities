using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace MSXUtilities.MsxWings
{
    public static class SC11_Compressor
    {
        /// <summary>
        /// Chunk size: 4 bytes
        /// Dictionary:
        ///    1-254       one-byte index      (254 values)
        ///    255, 0-254  two-byte index(255 values)
        /// Literals: 5 bytes
        ///     0, 4 bytes literal
        /// Ratio aprox: 0.5 of original size
        /// </summary>
        public static void Method_1()
        {
            const int CHUNK_SIZE = 4;
            const int ONE_BYTE_INDEX_MAX = 254;
            const int TWO_BYTES_INDEX_MAX = 255;

            IDictionary<byte[], int> dict = new Dictionary<byte[], int>();

            int inputSize = 0;

            for (int n = 0; n <= 15; n++)
            {
                byte[] input = File.ReadAllBytes(String.Format(@"C:\Users\XDAD\source\repos\msx-wings\Graphics\Bitmaps\Level_1\level1_{0}.sra.new", n));
                inputSize += input.Length;

                for (int i = 0; i < input.Length; i += CHUNK_SIZE)
                {
                    var found = dict.FirstOrDefault(x =>
                        x.Key[0] == input[i] &&
                        x.Key[1] == input[i + 1] &&
                        x.Key[2] == input[i + 2] &&
                        x.Key[3] == input[i + 3]
                        );

                    //IList<byte[], int> list = new List<byte[], int>();
                    //list = dict.Where(x => x.Key[0] == input[i]);
                    //for (int j = 1; j < CHUNK_SIZE; j++)
                    //{
                    //    list = list.Where(x => x.Key[j] == input[i+j]);
                    //}

                    //var found = list.FirstOrDefault();

                    if (found.Key == null)
                    {
                        //dict.Add(new byte[] { input[i], input[i + 1], input[i + 2], input[i + 3] }, 1);

                        byte[] temp = new byte[CHUNK_SIZE];
                        for (int j = 0; j < CHUNK_SIZE; j++)
                        {
                            temp[j] = input[i + j];
                        }

                        dict.Add(temp, 1);
                    }
                    else
                    {
                        int qtd = found.Value + 1;
                        dict[found.Key] = qtd;
                    }

                }
            }

            var result = dict.OrderByDescending(x => x.Value).ToList();

            //byte[] output = Array.Empty<byte>();
            int outputSize = 0;
            for (int i = 0; i < result.Count; i++)
            {
                // first 253 indexes (the ones with most repetitions) will become one-byte indexes
                if (i <= ONE_BYTE_INDEX_MAX - 1)
                {
                    outputSize += result[i].Value;
                }

                // two-byte indexes
                else if (i > (ONE_BYTE_INDEX_MAX - 1) && i <= (TWO_BYTES_INDEX_MAX + ONE_BYTE_INDEX_MAX - 1))
                {
                    outputSize += result[i].Value * 2;
                }

                // literals
                else
                {
                    outputSize += result[i].Value * (CHUNK_SIZE + 1);
                }

            }

            var resultWithotQtd_1 = result.Where(x => x.Value > 1).ToList();
            int dictSize = resultWithotQtd_1.Count * 4;
            double ratio = (outputSize + dictSize) / (double)inputSize;


            Console.WriteLine("inputSize: " + inputSize);
            Console.WriteLine("outputSize: " + outputSize);
            Console.WriteLine("dict size: " + dictSize);
            Console.WriteLine("ratio: " + ratio);
        }

        /// <summary>
        /// 
        /// 64kb(or 48kb) of VRAM used as sliding window for packing.
        /// packing a new line(256 bytes) by searching blocks of repeated bytes(4 up to 127)
        /// 
        /// format:
        /// 
        /// size of block/size of literal:  1 byte
        ///     bit 7: 1: literal; 0: repeat block
        /// 
        /// offset addr:			2 bytes(1 word)
        /// 
        /// 
        /// 
        /// ex:
        /// 0x07, 1, 4, 190, 0, 0, 1, 19 : 	7 literal bytes(1 to 19)
        /// 1000 1111 b, 0x409f :		repeat 15 bytes started at 0x409f
        /// 
        /// Ratio aprox: 0.44 of original size (line of 256 bytes packed to 113 bytes)
        /// </summary>
        public static void Method_2()
        {
            // 16 pages of 16 kb each
            byte[] input = File.ReadAllBytes(@"C:\Users\XDAD\source\repos\msx-wings\Graphics\Bitmaps\Level_1\level_1_all.sca");
            //byte[] output = Array.Empty<byte>();
            IList<byte> output = new List<byte>();

            int windowStart = 16 * 1024 * 12;       // start of page 12
            int windowEnd = (16 * 1024 * 16) - 1;   // end of last page
            

            // provisory; debug: testing just one line compression

            int inputCurrentPosition = windowStart - 256; // start of last line of page 11
            while (inputCurrentPosition < windowStart)
            {
                Console.WriteLine();
                Console.WriteLine("Input current position: " + inputCurrentPosition);
                Console.WriteLine("Input current position (inside window): " + (inputCurrentPosition - (windowStart - 256)));

                // calc block size max (check end of line)
                int remainingBytesInInput = windowStart - inputCurrentPosition;
                int blockMaxSize = (remainingBytesInInput > 127) ? 127 : remainingBytesInInput;


                bool found_2 = false;
                for (int blockSize = blockMaxSize; blockSize >= 4; blockSize--)
                {
                    //Console.WriteLine("  Block size: " + blockSize);

                    byte[] block = new byte[blockSize];

                    //populate block array
                    for (int i = 0; i < blockSize; i++)
                    {
                        block[i] = input[inputCurrentPosition + i];
                    }

                    // loop through all window looking for a sequence equal block array
                    bool found_1 = false;
                    for (int i = windowStart; i < windowEnd - blockSize; i++)
                    {
                        //Console.WriteLine("  searching window position: " + i);

                        bool found = true;
                        for (int j = 0; j < blockSize; j++)
                        {
                            if (block[j] != input[i + j])
                            {
                                found = false;
                            }
                        }

                        if(found)
                        {
                            Console.WriteLine("  Block size: " + blockSize);

                            Console.WriteLine();
                            Console.WriteLine("    Found: input position: " + i);
                            Console.Write("      ");
                            for (int j = 0; j < blockSize; j++)
                            {
                                Console.Write(input[i + j] + ",");
                            }

                            Console.WriteLine();
                            Console.WriteLine("    new line block: " + inputCurrentPosition);
                            Console.Write("      ");
                            for (int j = 0; j < blockSize; j++)
                            {
                                Console.Write(block[j] + ",");
                            }

                            // populate output, update vars
                            output.Add((byte)block.Length); // block header
                            output.Add((byte)(i & 0x000ff)); // address low byte
                            output.Add((byte)((i & 0x0ff00) >> 8)); // address hi byte
                            inputCurrentPosition += blockSize;
                            found_1 = true;
                            found_2 = true;

                            Console.WriteLine();
                            Console.WriteLine("    output current size: " + output.Count);

                            break;
                        }
                    }
                    if (found_1) break;
                }
                if (!found_2)
                {
                    // not found, make it literal of 4 bytes
                    //int literalSize = 4;

                    output.Add(0b10000000 & 4); // block header (bit 7 set, bits 6-0: size of literal)
                    output.Add(input[inputCurrentPosition]);
                    output.Add(input[inputCurrentPosition + 1]);
                    output.Add(input[inputCurrentPosition + 2]);
                    output.Add(input[inputCurrentPosition + 3]);
                    inputCurrentPosition += 1 + 4;

                    Console.WriteLine();
                    Console.WriteLine("  not found, literal of 4 bytes");
                    Console.WriteLine("    output current size: " + output.Count);
                }
            }
        }

        /// <summary>
        /// Idem to Method 2, but trying the bigger block size on all input, before going to a smaller block size
        /// Ratio aprox: 0.5 of original size (line of 256 bytes packed to 69 bytes, but missing 32 bytes, which would have to be literals)
        /// </summary>
        public static void Method_3()
        {
            // 16 pages of 16 kb each
            byte[] input = File.ReadAllBytes(@"C:\Users\XDAD\source\repos\msx-wings\Graphics\Bitmaps\Level_1\level_1_all.sca");
            //byte[] output = Array.Empty<byte>();
            IList<byte> output = new List<byte>();

            int windowStart = 16 * 1024 * 12;       // start of page 12
            int windowEnd = (16 * 1024 * 16) - 1;   // end of last page


            // debug: test just one line
            int inputCurrentPosition = windowStart - 256; // start of last line of page 11
            bool[] newLinePositions = new bool[256]; // array indicating positions already compressed
            for (int i = 0; i < 256; i++)
            {
                newLinePositions[i] = false;
            }
            for (int blockSize = 32; blockSize >= 4; blockSize--)
            {
                Console.WriteLine();
                Console.WriteLine("Trying block size " + blockSize);

                // try this block size on all positions of the input (new line)
                for (int k = inputCurrentPosition, positionOnNewLine = 0; k < windowStart - blockSize; k++, positionOnNewLine++)
                {
                    byte[] block = new byte[blockSize];

                    //populate block array
                    bool positionsAlreadyResolved = false;
                    for (int i = 0; i < blockSize; i++)
                    {
                        if (newLinePositions[positionOnNewLine + i]) positionsAlreadyResolved = true;
                        block[i] = input[k + i];
                    }

                    if (!positionsAlreadyResolved)
                    {
                        for (int i = windowStart; i < windowEnd - blockSize; i++)
                        {
                            bool found = true;
                            for (int j = 0; j < blockSize; j++)
                            {
                                if (block[j] != input[i + j])
                                {
                                    found = false;
                                }
                            }

                            if (found)
                            {
                                Console.WriteLine("  Block size: " + blockSize);

                                Console.WriteLine();
                                Console.WriteLine("    Found: input position: " + i);
                                Console.Write("      ");
                                for (int j = 0; j < blockSize; j++)
                                {
                                    Console.Write(input[i + j] + ",");
                                }

                                Console.WriteLine();
                                Console.WriteLine("    new line position absolute: " + k);
                                Console.WriteLine("    new line position relative: " + positionOnNewLine);
                                Console.Write("      ");
                                for (int j = 0; j < blockSize; j++)
                                {
                                    Console.Write(block[j] + ",");
                                }


                                // set positions resolved to be ignored on next iterations
                                for (int j = 0; j < blockSize; j++)
                                {
                                    newLinePositions[positionOnNewLine + j] = true;
                                }

                                // populate output, update vars
                                output.Add((byte)block.Length); // block header
                                output.Add((byte)(i & 0x000ff)); // address low byte
                                output.Add((byte)((i & 0x0ff00) >> 8)); // address hi byte
                                                                        //inputCurrentPosition += blockSize;
                                                                        //found_1 = true;
                                                                        //found_2 = true;

                                Console.WriteLine();
                                Console.WriteLine("    output current size: " + output.Count);

                                //Console.ReadKey();

                                break;
                            }

                        }
                    }
                }

            }

            // check if line is resolved
            Console.WriteLine("solved positions: " + newLinePositions.Where(x => x == true).ToList().Count);

        }

    }
}
