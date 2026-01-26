using System;
using System.Collections.Generic;
using System.Text;

namespace MSXUtilities.Helpers
{
    public static class MathHelpers
    {
        public static double DegreesToRadians(double angleInDegrees)
        {
            return (angleInDegrees * Math.PI) / 180.0;
        }
    }
}
