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
        string input = @"
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

        [TestMethod]
        public void Test_ExpandSprite_Factor_2()
        {
            // Arrange
            int factor = 2;

            // Act
            var output = ExpandSprites_Class.ExpandSprites(input, factor);

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

            //TODO: fix it (the 16x16 sprites are not organized this way)

            Assert.AreEqual((factor*factor) * 32, output.FormattedLines.Count);			// number of lines on output
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[0]);
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[1]);
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[2]);
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[3]);
            Assert.AreEqual("\tdb 00001111 b", output.FormattedLines[4]);
            Assert.AreEqual("\tdb 00001111 b", output.FormattedLines[5]);
            Assert.AreEqual("\tdb 00111111 b", output.FormattedLines[6]);
            Assert.AreEqual("\tdb 00111111 b", output.FormattedLines[7]);
            Assert.AreEqual("\tdb 00111111 b", output.FormattedLines[8]);
            Assert.AreEqual("\tdb 00111111 b", output.FormattedLines[9]);
            Assert.AreEqual("\tdb 00111111 b", output.FormattedLines[10]);
            Assert.AreEqual("\tdb 00111111 b", output.FormattedLines[11]);
            Assert.AreEqual("\tdb 00111111 b", output.FormattedLines[12]);
            Assert.AreEqual("\tdb 00111111 b", output.FormattedLines[13]);
            Assert.AreEqual("\tdb 00111111 b", output.FormattedLines[14]);
            Assert.AreEqual("\tdb 00111111 b", output.FormattedLines[15]);

            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[16]);
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[17]);
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[18]);
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[19]);
            Assert.AreEqual("\tdb 11111111 b", output.FormattedLines[20]);
            Assert.AreEqual("\tdb 11111111 b", output.FormattedLines[21]);
            Assert.AreEqual("\tdb 11111111 b", output.FormattedLines[22]);
            Assert.AreEqual("\tdb 11111111 b", output.FormattedLines[23]);
            Assert.AreEqual("\tdb 11111111 b", output.FormattedLines[24]);
            Assert.AreEqual("\tdb 11111111 b", output.FormattedLines[25]);
            Assert.AreEqual("\tdb 11110000 b", output.FormattedLines[26]);
            Assert.AreEqual("\tdb 11110000 b", output.FormattedLines[27]);
            Assert.AreEqual("\tdb 11110000 b", output.FormattedLines[28]);
            Assert.AreEqual("\tdb 11110000 b", output.FormattedLines[29]);
            Assert.AreEqual("\tdb 11110000 b", output.FormattedLines[30]);
            Assert.AreEqual("\tdb 11110000 b", output.FormattedLines[31]);

            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[32]);
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[33]);
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[34]);
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[35]);
            Assert.AreEqual("\tdb 11111111 b", output.FormattedLines[36]);
            Assert.AreEqual("\tdb 11111111 b", output.FormattedLines[37]);
            Assert.AreEqual("\tdb 11111111 b", output.FormattedLines[38]);
            Assert.AreEqual("\tdb 11111111 b", output.FormattedLines[39]);
            Assert.AreEqual("\tdb 11111111 b", output.FormattedLines[40]);
            Assert.AreEqual("\tdb 11111111 b", output.FormattedLines[41]);
            Assert.AreEqual("\tdb 00111111 b", output.FormattedLines[42]);
            Assert.AreEqual("\tdb 00111111 b", output.FormattedLines[43]);
            Assert.AreEqual("\tdb 00111111 b", output.FormattedLines[44]);
            Assert.AreEqual("\tdb 00111111 b", output.FormattedLines[45]);
            Assert.AreEqual("\tdb 00111111 b", output.FormattedLines[46]);
            Assert.AreEqual("\tdb 00111111 b", output.FormattedLines[47]);

            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[48]);
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[49]);
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[50]);
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[51]);
            Assert.AreEqual("\tdb 11000000 b", output.FormattedLines[52]);
            Assert.AreEqual("\tdb 11000000 b", output.FormattedLines[53]);
            Assert.AreEqual("\tdb 11110000 b", output.FormattedLines[54]);
            Assert.AreEqual("\tdb 11110000 b", output.FormattedLines[55]);
            Assert.AreEqual("\tdb 11110000 b", output.FormattedLines[56]);
            Assert.AreEqual("\tdb 11110000 b", output.FormattedLines[57]);
            Assert.AreEqual("\tdb 11110000 b", output.FormattedLines[58]);
            Assert.AreEqual("\tdb 11110000 b", output.FormattedLines[59]);
            Assert.AreEqual("\tdb 11110000 b", output.FormattedLines[60]);
            Assert.AreEqual("\tdb 11110000 b", output.FormattedLines[61]);
            Assert.AreEqual("\tdb 11110000 b", output.FormattedLines[62]);
            Assert.AreEqual("\tdb 11110000 b", output.FormattedLines[63]);
        }

        [TestMethod]
        public void Test_ExpandSprite_Factor_3()
        {
            // Arrange
            int factor = 3;

            // Act
            var output = ExpandSprites_Class.ExpandSprites(input, factor);

            // Assert
            Assert.AreEqual(factor * 16, output.Lines.Count);			// number of lines on output
            Assert.AreEqual(factor * 16, output.Lines[0].Length);		// number of columns on line
        }

        [TestMethod]
        public void Test_ExpandSprite_Factor_4()
        {
            // Arrange
            int factor = 4;

            // Act
            var output = ExpandSprites_Class.ExpandSprites(input, factor);

            // Assert
            Assert.AreEqual(factor * 16, output.Lines.Count);			// number of lines on output
            Assert.AreEqual(factor * 16, output.Lines[0].Length);		// number of columns on line
        }
    }
}
