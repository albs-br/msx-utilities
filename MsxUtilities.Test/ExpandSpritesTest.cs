using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSXUtilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MsxUtilities.Test
{
    [TestClass]
    public class ExpandSpritesTest
    {
        // ; -------------------------char #0 -------------------------
        // ; ------pattern 0
        string inputPattern = @"
					db	00000000 b
					db	00000000 b
					db	00111111 b
					db	01111111 b
					db	01111111 b
					db	01111100 b
					db	01111100 b
					db	01111100 b
					db	01111100 b
					db	01111100 b
					db	01111100 b
					db	01111100 b
					db	01111111 b
					db	01111111 b
					db	00111111 b
					db	00000000 b
					db	00000000 b
					db	00000000 b
					db	11111000 b
					db	11111100 b
					db	11111100 b
					db	01111100 b
					db	01111100 b
					db	01111100 b
					db	01111100 b
					db	01111100 b
					db	01111100 b
					db	01111100 b
					db	11111100 b
					db	11111100 b
					db	11111000 b
					db	00000000 b
				 ";

        string inputColors = @"
	                db	0x05
	                db	0x05
	                db	0x0f
	                db	0x0f
	                db	0x09
	                db	0x09
	                db	0x0d
	                db	0x0d
	                db	0x04
	                db	0x04
	                db	0x0c
	                db	0x0c
	                db	0x08
	                db	0x08
	                db	0x0d
	                db	0x0d
                 ";

        [TestMethod]
        public void Test_ExpandSprite_Factor_2()
        {
            // Arrange
            int factor = 2;

            // Act
            var output = ExpandSprites_Class.ExpandSprites(inputPattern, inputColors, factor);

            // Assert
            Assert.AreEqual(factor * 16, output.Lines.Count);			// number of lines on output
            Assert.AreEqual(factor * 16, output.Lines[0].Length);		// number of columns on line
            Assert.AreEqual("00000000000000000000000000000000", output.Lines[0]);
            Assert.AreEqual("00000000000000000000000000000000", output.Lines[1]);
            Assert.AreEqual("00000000000000000000000000000000", output.Lines[2]);
            Assert.AreEqual("00000000000000000000000000000000", output.Lines[3]);
            Assert.AreEqual("00001111111111111111111111000000", output.Lines[4]);
            Assert.AreEqual("00001111111111111111111111000000", output.Lines[5]);
            Assert.AreEqual("00111111111111111111111111110000", output.Lines[6]);
            Assert.AreEqual("00111111111111111111111111110000", output.Lines[7]);
            Assert.AreEqual("00111111111111111111111111110000", output.Lines[8]);
            Assert.AreEqual("00111111111111111111111111110000", output.Lines[9]);
            Assert.AreEqual("00111111111100000011111111110000", output.Lines[10]);
            Assert.AreEqual("00111111111100000011111111110000", output.Lines[11]);
            Assert.AreEqual("00111111111100000011111111110000", output.Lines[12]);
            Assert.AreEqual("00111111111100000011111111110000", output.Lines[13]);
            Assert.AreEqual("00111111111100000011111111110000", output.Lines[14]);
            Assert.AreEqual("00111111111100000011111111110000", output.Lines[15]);
            Assert.AreEqual("00111111111100000011111111110000", output.Lines[16]);
            Assert.AreEqual("00111111111100000011111111110000", output.Lines[17]);
            Assert.AreEqual("00111111111100000011111111110000", output.Lines[18]);
            Assert.AreEqual("00111111111100000011111111110000", output.Lines[19]);
            Assert.AreEqual("00111111111100000011111111110000", output.Lines[20]);
            Assert.AreEqual("00111111111100000011111111110000", output.Lines[21]);
            Assert.AreEqual("00111111111100000011111111110000", output.Lines[22]);
            Assert.AreEqual("00111111111100000011111111110000", output.Lines[23]);
            Assert.AreEqual("00111111111111111111111111110000", output.Lines[24]);
            Assert.AreEqual("00111111111111111111111111110000", output.Lines[25]);
            Assert.AreEqual("00111111111111111111111111110000", output.Lines[26]);
            Assert.AreEqual("00111111111111111111111111110000", output.Lines[27]);
            Assert.AreEqual("00001111111111111111111111000000", output.Lines[28]);
            Assert.AreEqual("00001111111111111111111111000000", output.Lines[29]);
            Assert.AreEqual("00000000000000000000000000000000", output.Lines[30]);
            Assert.AreEqual("00000000000000000000000000000000", output.Lines[31]);

            Assert.AreEqual((factor*factor) * 32, output.PatternLines.Count);			// number of lines on output
            
            // sprite #0
            Assert.AreEqual("\tdb 00000000 b", output.PatternLines[0]);
            Assert.AreEqual("\tdb 00000000 b", output.PatternLines[1]);
            Assert.AreEqual("\tdb 00000000 b", output.PatternLines[2]);
            Assert.AreEqual("\tdb 00000000 b", output.PatternLines[3]);
            Assert.AreEqual("\tdb 00001111 b", output.PatternLines[4]);
            Assert.AreEqual("\tdb 00001111 b", output.PatternLines[5]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[6]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[7]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[8]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[9]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[10]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[11]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[12]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[13]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[14]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[15]);

            Assert.AreEqual("\tdb 00000000 b", output.PatternLines[16]);
            Assert.AreEqual("\tdb 00000000 b", output.PatternLines[17]);
            Assert.AreEqual("\tdb 00000000 b", output.PatternLines[18]);
            Assert.AreEqual("\tdb 00000000 b", output.PatternLines[19]);
            Assert.AreEqual("\tdb 11111111 b", output.PatternLines[20]);
            Assert.AreEqual("\tdb 11111111 b", output.PatternLines[21]);
            Assert.AreEqual("\tdb 11111111 b", output.PatternLines[22]);
            Assert.AreEqual("\tdb 11111111 b", output.PatternLines[23]);
            Assert.AreEqual("\tdb 11111111 b", output.PatternLines[24]);
            Assert.AreEqual("\tdb 11111111 b", output.PatternLines[25]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[26]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[27]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[28]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[29]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[30]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[31]);

            // sprite #1
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[32]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[33]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[34]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[35]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[36]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[37]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[38]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[39]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[40]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[41]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[42]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[43]);
            Assert.AreEqual("\tdb 00001111 b", output.PatternLines[44]);
            Assert.AreEqual("\tdb 00001111 b", output.PatternLines[45]);
            Assert.AreEqual("\tdb 00000000 b", output.PatternLines[46]);
            Assert.AreEqual("\tdb 00000000 b", output.PatternLines[47]);

            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[48]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[49]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[50]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[51]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[52]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[53]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[54]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[55]);
            Assert.AreEqual("\tdb 11111111 b", output.PatternLines[56]);
            Assert.AreEqual("\tdb 11111111 b", output.PatternLines[57]);
            Assert.AreEqual("\tdb 11111111 b", output.PatternLines[58]);
            Assert.AreEqual("\tdb 11111111 b", output.PatternLines[59]);
            Assert.AreEqual("\tdb 11111111 b", output.PatternLines[60]);
            Assert.AreEqual("\tdb 11111111 b", output.PatternLines[61]);
            Assert.AreEqual("\tdb 00000000 b", output.PatternLines[62]);
            Assert.AreEqual("\tdb 00000000 b", output.PatternLines[63]);

            // sprite #2
            Assert.AreEqual("\tdb 00000000 b", output.PatternLines[64]);
            Assert.AreEqual("\tdb 00000000 b", output.PatternLines[65]);
            Assert.AreEqual("\tdb 00000000 b", output.PatternLines[66]);
            Assert.AreEqual("\tdb 00000000 b", output.PatternLines[67]);
            Assert.AreEqual("\tdb 11111111 b", output.PatternLines[68]);
            Assert.AreEqual("\tdb 11111111 b", output.PatternLines[69]);
            Assert.AreEqual("\tdb 11111111 b", output.PatternLines[70]);
            Assert.AreEqual("\tdb 11111111 b", output.PatternLines[71]);
            Assert.AreEqual("\tdb 11111111 b", output.PatternLines[72]);
            Assert.AreEqual("\tdb 11111111 b", output.PatternLines[73]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[74]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[75]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[76]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[77]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[78]);
            Assert.AreEqual("\tdb 00111111 b", output.PatternLines[79]);

            Assert.AreEqual("\tdb 00000000 b", output.PatternLines[80]);
            Assert.AreEqual("\tdb 00000000 b", output.PatternLines[81]);
            Assert.AreEqual("\tdb 00000000 b", output.PatternLines[82]);
            Assert.AreEqual("\tdb 00000000 b", output.PatternLines[83]);
            Assert.AreEqual("\tdb 11000000 b", output.PatternLines[84]);
            Assert.AreEqual("\tdb 11000000 b", output.PatternLines[85]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[86]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[87]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[88]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[89]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[90]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[91]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[92]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[93]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[94]);
            Assert.AreEqual("\tdb 11110000 b", output.PatternLines[95]);

            Assert.AreEqual(factor * 16, output.ColorLines.Count);			// number of color lines on output
        }

        [TestMethod]
        public void Test_ExpandSprite_Factor_3()
        {
            // Arrange
            int factor = 3;

            // Act
            var output = ExpandSprites_Class.ExpandSprites(inputPattern, inputColors, factor);

            // Assert
            Assert.AreEqual(factor * 16, output.Lines.Count);			// number of lines on output
            Assert.AreEqual(factor * 16, output.Lines[0].Length);		// number of columns on line

            //TODO: check if bit patterns are ok
            Assert.AreEqual((factor * factor) * 32, output.PatternLines.Count);			// number of lines on output

            Assert.AreEqual(factor * 16, output.ColorLines.Count);			// number of color lines on output
        }

        [TestMethod]
        public void Test_ExpandSprite_Factor_4()
        {
            // Arrange
            int factor = 4;

            // Act
            var output = ExpandSprites_Class.ExpandSprites(inputPattern, inputColors, factor);

            // Assert
            Assert.AreEqual(factor * 16, output.Lines.Count);			// number of lines on output
            Assert.AreEqual(factor * 16, output.Lines[0].Length);		// number of columns on line


            //TODO: check if bit patterns are ok
            Assert.AreEqual((factor * factor) * 32, output.PatternLines.Count);			// number of lines on output

            Assert.AreEqual(factor * 16, output.ColorLines.Count);			// number of color lines on output
        }
    }
}
