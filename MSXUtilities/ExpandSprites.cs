using System;
using System.Collections.Generic;
using System.Text;

namespace MSXUtilities
{
    public static class ExpandSprites_Class
    {
		/// <summary>
		/// Expand a 16x16 MSX 2 sprite to a factor, e.g. factor 2 will return
		/// a 32x32 sprite, factor 3 a 48x48, and so on
		/// </summary>
		/// <param name="factor">2, 3, 4, 5</param>
		/// <exception cref="Exception"></exception>
		public static IList<string> ExpandSprites(string input, int factor)
		{
			
			// convert input to list of strings with only the bits
			var tempArray = input.Split(Environment.NewLine);
			var listInput = new List<string>();
            for (int i = 0; i < tempArray.Length; i++)
			{
				var tempItem = tempArray[i].Replace("db", "").Replace("b", "").Trim();

				if (tempItem != "")
				{
                    if (tempItem.Length != 8) throw new Exception("Lines on input must be 8 bits long");

                    listInput.Add(tempItem); 
				}
			}

			if (listInput.Count != 32) throw new Exception("Number of lines on input must be 32");


            // convert list from 32 lines of 8 bits to 16 lines of 16 bits
            var listInput_16x16 = new List<string>();
            for (int i = 0; i < 16; i++)
			{
				listInput_16x16.Add(listInput[i] + listInput[i+16]);
			}



            // do the scaling
			var listaOutput = new List<string>();
			for (int i = 0; i < 16; i++)
			{

				var tempOutputLine = "";
				for (int j = 0; j < 16; j++)
				{
					for (int k = 0; k < factor; k++)
					{
						tempOutputLine += listInput_16x16[i][j];
					}
				}

				for (int k = 0; k < factor; k++)
				{
					listaOutput.Add(tempOutputLine);
				}
			}

			return listaOutput;
        }
    }
}
