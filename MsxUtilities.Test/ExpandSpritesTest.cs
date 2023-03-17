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
            Assert.AreEqual(factor * 16, output.Count);			// number of lines on output
            Assert.AreEqual(factor * 16, output[0].Length);		// number of columns on line

        }

        [TestMethod]
        public void Test_ExpandSprite_Factor_3()
        {
            // Arrange
            int factor = 3;

            // Act
            var output = ExpandSprites_Class.ExpandSprites(input, factor);

            // Assert
            Assert.AreEqual(factor * 16, output.Count);			// number of lines on output
            Assert.AreEqual(factor * 16, output[0].Length);		// number of columns on line
        }

        [TestMethod]
        public void Test_ExpandSprite_Factor_4()
        {
            // Arrange
            int factor = 4;

            // Act
            var output = ExpandSprites_Class.ExpandSprites(input, factor);

            // Assert
            Assert.AreEqual(factor * 16, output.Count);			// number of lines on output
            Assert.AreEqual(factor * 16, output[0].Length);		// number of columns on line
        }
    }
}
