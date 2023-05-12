using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MSXUtilities
{
	public static class ExpandSprites_Class
	{
        /*

		size		factor	16x16 blocks		8x8 blocks	lines
		=====		======	============		==========	=====
		16x16		1		1					4			32
		32x32		2		2*2=4				4*4=16		16*8 = 128
		48x48		3		3*3=9				6*6=36		36*8 = 288
		64x64		4		4*4=16				8*8=64		64*8 = 288
		 
		*/

        /// <summary>
        /// Expand a 16x16 MSX 2 sprite by a factor, e.g. factor 2 will return
        /// a 32x32 sprite, factor 3 a 48x48, and so on
        /// </summary>
        /// <param name="factor">2, 3, 4, 5</param>
        /// <exception cref="Exception"></exception>
        public static ExpandSprites_Output ExpandSprites(string inputPattern, string inputColors, int factor)
		{
			var listOutput = new List<string>();
			var formattedLines = new List<string>();
			var colorLines = new List<string>();

			if (inputPattern != null) // if input pattern is missing, generate only colors
			{
				// convert input pattern to list of strings with only the pattern bits
				var tempArray = inputPattern.Split(Environment.NewLine);
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
					listInput_16x16.Add(listInput[i] + listInput[i + 16]);
				}



				// do the scaling
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
						listOutput.Add(tempOutputLine);
					}
				}

				// convert expanded sprite to 16x16 grids
				var listOutput16x16 = new List<string>();
				for (int j = 0; j < factor; j++) // values for factor = 2: 0-1
				{
					for (int i = 0; i < factor * 16; i++) // values for factor = 2: 0-31
					{
						listOutput16x16.Add(listOutput[i].Substring(j * 16, 16));
					}
				}

				// lines formatted on asm syntax (convert to a list of 8 pixels per line)
				var lineFormat = "\tdb {0} b";
				for (int i = 0; i < ((factor * 2) * (factor * 2)) * 8; i++)
				{
					formattedLines.Add("");
				}
				for (int j = 0; j < factor * factor; j++)
				{
					for (int i = 0; i < 16; i++)
					{
						// ----- f = 0
						// 0-15
						// 16-31
						// ----- f = 1
						// 32-47
						// 48-63
						formattedLines[(j * 32) + i] = String.Format(lineFormat, listOutput16x16[(j * 16) + i].Substring(0, 8));
						formattedLines[(j * 32) + i + 16] = String.Format(lineFormat, listOutput16x16[(j * 16) + i].Substring(8, 8));
					}
				}

			}

			// convert input colors to colors expanded
			foreach (var line in inputColors.Split(Environment.NewLine))
			{
				if (line.Trim() != "")
				{
					for (int i = 0; i < factor; i++)
					{
						colorLines.Add(line);
					}
				}
			}

			return new ExpandSprites_Output
			{
				Lines = listOutput,
				PatternLines = formattedLines,
				ColorLines = colorLines
			};
		}
	}

	public class ExpandSprites_Output
	{
		public IList<string> Lines { get; set; }
		public IList<string> PatternLines { get; set; }
		public IList<string> ColorLines { get; set; }

		public string GetText_Pattern()
		{
			var sbText = new StringBuilder();
			var counter = 0;
			foreach (var line in PatternLines)
            {
				if (counter % 32 == 0)
				{
					sbText.AppendLine("; ----------- Sprite pattern #" + (counter/32));
				}
				
				sbText.AppendLine(line);
				counter++;
            }
			return sbText.ToString();
		}

		public string GetText_Colors()
		{
			var sbText = new StringBuilder();
			foreach (var line in ColorLines)
			{
				sbText.AppendLine(line);
			}
			return sbText.ToString();
		}
	}
}
