using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSXUtilities;
using System.Collections.Generic;
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
            int sprite0_offsetX = 0, sprite0_offsetY = 0, sprite1_offsetX = 0, sprite1_offsetY = 0;
            int sprite0_width = 16, sprite0_height = 16;
            var paletteBytes = new byte[32];
            var patternBytes = new byte[64];
            var colorsBytes = new byte[32];
            var fileName = @"MsxWings\sprites - less colors.SC5";

            // Act
            using (var input = File.OpenRead(fileName))
            using (var reader = new BinaryReader(input))
            {
                ConvertSc5ImageToSprites.DoConversion_2_Sprites(
                    sprite0_offsetX, sprite0_offsetY,
                    sprite1_offsetX, sprite1_offsetY,
                    sprite0_width, sprite0_height,
                    paletteBytes, patternBytes, colorsBytes,
                    input, reader
                );
            }

            // Assert
            byte[] assertPaletteFile = File.ReadAllBytes(@"AssertFiles\player_plane_0.pal");
            byte[] assertPatternsFile = File.ReadAllBytes(@"AssertFiles\player_plane_0.pat");
            byte[] assertColorsFile = File.ReadAllBytes(@"AssertFiles\player_plane_0.col");

            // discover substitutions from one palette to another
            var substitions = new List<int>();
            for (int i = 0; i < 32; i+=2)
            {
                for (int j = 0; j < 32; j+=2)
                {
                    if (assertPaletteFile[i] == paletteBytes[j] && assertPaletteFile[i + 1] == paletteBytes[j + 1])
                    {
                        substitions.Add(j/2);
                    }
                }
            }
            Assert.AreEqual(16, substitions.Count);

            CollectionAssert.AreEqual(assertPatternsFile, patternBytes);

            // check colors, using substitutions list
            for (int i = 0; i < 32; i++)
            {
                var assertColor = (assertColorsFile[i] >= 64) ? assertColorsFile[i] - 64 : assertColorsFile[i];
                var color = (colorsBytes[i] >= 64) ? colorsBytes[i] - 64 : colorsBytes[i];

                if (substitions[assertColor] != color)
                {
                    Assert.Fail("Color table is different at index " + i);
                }
            }
        }

        [TestMethod]
        public void Test_DoConversion_2_Sprites_Offset_0_8()
        {
            // Arrange
            int sprite0_offsetX = 21, sprite0_offsetY = 0, sprite1_offsetX = 0, sprite1_offsetY = 8;
            int sprite0_width = 16, sprite0_height = 16;
            var paletteBytes = new byte[32];
            var patternBytes = new byte[64];
            var colorsBytes = new byte[32];
            var fileName = @"MsxWings\sprites - less colors.SC5";
            var bruteForcePalette = false;

            // Act
            using (var input = File.OpenRead(fileName))
            using (var reader = new BinaryReader(input))
            {
                ConvertSc5ImageToSprites.DoConversion_2_Sprites(
                    sprite0_offsetX, sprite0_offsetY,
                    sprite1_offsetX, sprite1_offsetY,
                    sprite0_width, sprite0_height,
                    paletteBytes, patternBytes, colorsBytes,
                    input, reader,
                    bruteForcePalette
                );
            }

            // Assert
            byte[] assertPaletteFile = File.ReadAllBytes(@"AssertFiles\enemy_plane_0.pal");
            byte[] assertPatternsFile = File.ReadAllBytes(@"AssertFiles\enemy_plane_0.pat");
            byte[] assertColorsFile = File.ReadAllBytes(@"AssertFiles\enemy_plane_0.col");

            CollectionAssert.AreEqual(assertPaletteFile, paletteBytes);
            CollectionAssert.AreEqual(assertPatternsFile, patternBytes);
            CollectionAssert.AreEqual(assertColorsFile, colorsBytes);
        }

        [TestMethod]
        public void Test_DoConversion_2_Sprites_Offset_5_0()
        {
            // Arrange
            int sprite0_offsetX = 0, sprite0_offsetY = 16, sprite1_offsetX = 5, sprite1_offsetY = 0;
            int sprite0_width = 16, sprite0_height = 16;
            var paletteBytes = new byte[32];
            var patternBytes = new byte[64];
            var colorsBytes = new byte[32];
            var fileName = @"MsxWings\sprites - less colors.SC5";
            var bruteForcePalette = false;

            // Act
            using (var input = File.OpenRead(fileName))
            using (var reader = new BinaryReader(input))
            {
                ConvertSc5ImageToSprites.DoConversion_2_Sprites(
                    sprite0_offsetX, sprite0_offsetY,
                    sprite1_offsetX, sprite1_offsetY,
                    sprite0_width, sprite0_height,
                    paletteBytes, patternBytes, colorsBytes,
                    input, reader,
                    bruteForcePalette
                );
            }

            // Assert
            byte[] assertPaletteFile = File.ReadAllBytes(@"AssertFiles\player_plane_bottom.pal");
            byte[] assertPatternsFile = File.ReadAllBytes(@"AssertFiles\player_plane_bottom.pat");
            byte[] assertColorsFile = File.ReadAllBytes(@"AssertFiles\player_plane_bottom.col");

            //CollectionAssert.AreEqual(assertPaletteFile, paletteBytes);
            CollectionAssert.AreEqual(assertPatternsFile, patternBytes);
            CollectionAssert.AreEqual(assertColorsFile, colorsBytes);
        }
    }
}
