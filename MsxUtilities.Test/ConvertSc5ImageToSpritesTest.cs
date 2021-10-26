using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSXUtilities;
using System.IO;

namespace MsxUtilities.Test
{
    [TestClass]
    public class ConvertSc5ImageToSpritesTest
    {
        [TestMethod]
        public void Test_DoConversion_2_Sprites_Offset_0_0()
        {
            // Arrange
            int sprite0_offsetX = 0, sprite0_offsetY = 0, sprite0_width = 16, sprite0_height = 16;
            var paletteBytes = new byte[32];
            var patternBytes = new byte[64];
            var colorsBytes = new byte[32];
            var fileName = @"InputFiles\sprites 2 planes.bak.SC5";

            // Act
            using (var input = File.OpenRead(fileName))
            using (var reader = new BinaryReader(input))
            {
                ConvertSc5ImageToSprites.DoConversion_2_Sprites_Offset_0_0(
                sprite0_offsetX, sprite0_offsetY,
                sprite0_width, sprite0_height,
                paletteBytes, patternBytes, colorsBytes,
                input, reader
                );
            }

            // Assert
            //using (var paletteFile = File.Create(outputFileBaseName + ".pal"))
            //using (var patternsFile = File.Create(outputFileBaseName + ".pat"))
            //using (var colorsFile = File.Create(outputFileBaseName + ".col"))
            //{
            //    CollectionAssert.AreEqual(paletteBytes, paletteBytes);
            //}
        }
    }
}
