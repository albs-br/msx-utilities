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
            for (int i = 0; i <= 90; i++)
            {
                var strFormat = "dw {0} b ; cos of {1} degrees = {2}";

                var cos = Math.Cos(i * Math.PI / 180.0);

                Console.WriteLine(
                    String.Format(
                        strFormat,
                        Convert.ToString(Convert.ToUInt16(cos / (1/Math.Pow(2, 8))), 2),
                        i,
                        cos
                        )
                    );
            }
        }
    }
}
