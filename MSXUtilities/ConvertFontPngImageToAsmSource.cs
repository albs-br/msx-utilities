using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace MSXUtilities
{
    public static class ConvertFontPngImageToAsmSource
    {
        public static void Execute(string filePath)
        {
            // Load the PNG image into a Bitmap object
            Bitmap image = new Bitmap(filePath);

            // Define the size of each chunk
            int chunkSize = 8;

            // Define the number of chunks in each dimension
            int numChunksX = image.Width / chunkSize;
            int numChunksY = image.Height / chunkSize;

            // Open a new StreamWriter to write to the output file
            StreamWriter writer = new StreamWriter("output.txt");

            var index = 0;

            // Loop through each chunk and write its pixels to the output file
            for (int y = 0; y < numChunksY; y++)
            {
                for (int x = 0; x < numChunksX; x++)
                {
                    writer.WriteLine("; -------------------- char #" + index);
                    index++;

                    // Define the location and size of the current chunk
                    int chunkX = x * chunkSize;
                    int chunkY = y * chunkSize;
                    int chunkWidth = chunkSize;
                    int chunkHeight = chunkSize;

                    // Create a new byte to hold the pixel data for the current chunk
                    byte chunkData = 0;

                    // Loop through each pixel in the chunk and set the corresponding bit in the chunkData byte
                    for (int dy = 0; dy < chunkHeight; dy++)
                    {
                        var line = "";
                        for (int dx = 0; dx < chunkWidth; dx++)
                        {
                            int pixelX = chunkX + dx;
                            int pixelY = chunkY + dy;
                            Color pixelColor = image.GetPixel(pixelX, pixelY);
                            bool isBlack = (pixelColor.R + pixelColor.G + pixelColor.B) / 3 == 0; // Check if the pixel is black
                            //int bitIndex = dy * chunkWidth + dx; // Calculate the index of the bit in the chunkData byte
                            //chunkData |= (byte)((isBlack ? 0 : 1) << bitIndex); // Set the corresponding bit in the chunkData byte

                            line += (isBlack) ? "0" : "1";
                        }

                        // Convert the chunkData byte to a binary string
                        //string binaryString = Convert.ToString(chunkData, 2).PadLeft(8, '0'); // Convert the byte to a binary string with leading zeros

                        // Write the binary string to the output file with "db " at the beginning of the line
                        writer.WriteLine("db " + line + " b");
                    }


                }
            }

            // Close the StreamWriter
            writer.Close();

            // Dispose of the original image Bitmap to free up memory
            image.Dispose();
        }
    }
}
