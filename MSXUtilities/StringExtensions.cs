using System;
using System.Collections.Generic;
using System.Text;

namespace MSXUtilities
{
    public static class StringExtensions
    {
        public static string ReplaceAt(this string input, int index, char newChar)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }
            char[] chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }

        public static string ToPascalCase(this string input)
        {
            string c0 = "";
            if (input.Length > 0) { c0 = input.Substring(0, 1).ToUpper(); }
            string cn = "";
            if (input.Length > 1) { cn = input.Substring(1).ToLower(); }

            return c0 + cn;
        }
    }
}