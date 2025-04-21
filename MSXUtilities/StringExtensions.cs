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

        public static string ToPascalCase(this string input, bool keepUnderscores = false)
        {
            var arrayTmp = input.Split('_');

            var sb = new StringBuilder();
            bool first = true;
            foreach (var item in arrayTmp)
            {
                string c0 = "";
                if (item.Length > 0) { c0 = item.Substring(0, 1).ToUpper(); }
                string cn = "";
                if (item.Length > 1) { cn = item.Substring(1).ToLower(); }

                if (!first && keepUnderscores)
                {
                    sb.Append("_");
                }
                first = false;

                sb.Append(c0 + cn);
            }

            return sb.ToString();
        }
    }
}