using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace MSXUtilities.MsxDoom
{
    public static class Precalc_LUTs
    {
        public static void CreateCosTable()
        {
            for (int i = 0; i < 360; i++)
            {
                var strFormat = "\tdw {0} b\t; cos of {1} degrees = {2}";

                var cos = Math.Cos(i * Math.PI / 180.0);

                var cosFixedPoint = Convert.ToInt16(cos / (1 / Math.Pow(2, 8))); // convert cos value to binary fixed point 8.8

                var cosFixedPoint_1 = Convert.ToInt16(cosFixedPoint >> 1); // right right one bit (divide by 2)

                Console.WriteLine(
                    String.Format(
                        strFormat,
                        Convert.ToString(cosFixedPoint_1, 2),
                        i,
                        cos
                        )
                    );
            }
        }

        public static void CreateSinTable()
        {
            for (int i = 0; i < 360; i++)
            {
                var strFormat = "\tdw {0} b\t; sin of {1} degrees = {2}";

                var sin = Math.Sin(i * Math.PI / 180.0) * (-1);  // * (-1) because coordinate system of screen is on fourth quadrant (Y grows from top to bottom)

                var sinFixedPoint = Convert.ToInt16(sin / (1 / Math.Pow(2, 8))); // convert sin value to binary fixed point 8.8

                var sinFixedPoint_1 = Convert.ToInt16(sinFixedPoint >> 1); // right right one bit (divide by 2)

                Console.WriteLine(
                    String.Format(
                        strFormat,
                        Convert.ToString(sinFixedPoint_1, 2),
                        i,
                        sin
                        )
                    );
            }
        }

        public static void CreatePowerOf2Table()
        {
            for (int i = 0; i < 4096; i++)
            {
                var strFormat = "\tdw \t{0} \t; {1} ^ 2 = {2}, value >> 9 = {0}";

                int power = (int)(Math.Pow(i, 2));


                //int highByte = (power & 0b11111111_00000000_00000000) >> 16;
                //int middleByte = (power & 0b00000000_11111111_00000000) >> 8;
                //int lowByte = (power & 0b00000000_0000000011111111); // ignore less significant byte

                int twoHigherBytes = (power & 0b11111111_11111111_00000000) >> (8 + 1); // +1 to use 15 bits instead of 16

                Console.WriteLine(
                    String.Format(
                        strFormat,
                        twoHigherBytes,
                        i,
                        power
                        )
                    );
            }
        }

        public static void CreateSquareRootTable()
        {
            for (int i = 0; i < 256; i++)
            {
                var strFormat = "\tdb \t{0} \t; sqrt(0x{1}) = sqrt({2}) = {0}, high byte of input: {3}";

                int result = (int)(Math.Sqrt(i * 256));

                Console.WriteLine(
                    String.Format(
                        strFormat,
                        result,
                        (i * 256).ToString("X4"),
                        (i * 256),
                        i
                        )
                    );
            }
        }
    }
}
