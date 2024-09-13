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

                Console.WriteLine(
                    String.Format(
                        strFormat,
                        Convert.ToString(Convert.ToInt16(cos / (1/Math.Pow(2, 8))), 2),
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

                Console.WriteLine(
                    String.Format(
                        strFormat,
                        Convert.ToString(Convert.ToInt16(sin / (1 / Math.Pow(2, 8))), 2),
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
                var strFormat = "\tdw \t{0} \t; {1} ^ 2 = {2}, two higher bytes value = {0}";

                int power = (int)(Math.Pow(i, 2));


                //int highByte = (power & 0b11111111_00000000_00000000) >> 16;
                //int middleByte = (power & 0b00000000_11111111_00000000) >> 8;
                //int lowByte = (power & 0b00000000_0000000011111111); // ignore less significant byte

                int twoHigherBytes = (power & 0b11111111_11111111_00000000) >> 8;

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
    }
}
