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

            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[32]);
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[33]);
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[34]);
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[35]);
            Assert.AreEqual("\tdb 11111111 b", output.FormattedLines[36]);
            Assert.AreEqual("\tdb 11111111 b", output.FormattedLines[37]);
            Assert.AreEqual("\tdb 11111111 b", output.FormattedLines[38]);
            Assert.AreEqual("\tdb 11111111 b", output.FormattedLines[39]);

            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[64]);
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[65]);
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[66]);
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[67]);
            Assert.AreEqual("\tdb 11111111 b", output.FormattedLines[68]);
            Assert.AreEqual("\tdb 11111111 b", output.FormattedLines[69]);
            Assert.AreEqual("\tdb 11111111 b", output.FormattedLines[70]);
            Assert.AreEqual("\tdb 11111111 b", output.FormattedLines[71]);

            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[96]);
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[97]);
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[98]);
            Assert.AreEqual("\tdb 00000000 b", output.FormattedLines[99]);
            Assert.AreEqual("\tdb 11000000 b", output.FormattedLines[100]);
            Assert.AreEqual("\tdb 11000000 b", output.FormattedLines[101]);
            Assert.AreEqual("\tdb 11110000 b", output.FormattedLines[102]);
            Assert.AreEqual("\tdb 11110000 b", output.FormattedLines[103]);
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
