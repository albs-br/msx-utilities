using System;
using System.Collections.Generic;
using System.Drawing;

namespace MSXUtilities
{
    public static class CreateGradientImage
    {
        public static void Execute()
        {
            const int SCR_WIDTH = 256;
            const int SCR_HEIGHT = 192;


            const int NUMBER_OF_COLORS = 14;

            //const Color EMPTY_COLOR = Color.Magenta;

            Color[] colors = new Color[] {
                Color.White, Color.Black, Color.Red, Color.Blue, Color.Yellow, Color.Pink, Color.Green,
                Color.Gray, Color.Purple, Color.Cyan, Color.Brown, Color.Beige, Color.LightBlue, Color.DarkBlue
            };

            //Color[] colors = new Color[NUMBER_OF_COLORS];
            //for (int i = 0; i < NUMBER_OF_COLORS; i++)
            //{
            //    var c = (256 / NUMBER_OF_COLORS) * i;
            //    colors[i] = Color.FromArgb(c, c, c);
            //}

            Bitmap bmp = new Bitmap(SCR_WIDTH, SCR_HEIGHT);
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                Rectangle ImageSize = new Rectangle(0, 0, SCR_WIDTH, SCR_HEIGHT);
                // var brush = new Brush();
                graph.FillRectangle(Brushes.Magenta, ImageSize);
            }


            /*

            Stripe # 0
            
                Color #         Weight
                0               100%
                1               (100% / 13) * (13 - colorIndex)
                ...
                12              (100% / 13) * (13 - colorIndex)
                13              0%

            Stripe # 1
            
                Color #         Weight
                0               100%
                1               100%
                ...
                12              (100% / 13) * (13 - colorIndex)
                13              0%

             */

            int stripeHeight = (int)Math.Floor((decimal)(SCR_HEIGHT / NUMBER_OF_COLORS));

            int totalStripePixels = SCR_WIDTH * stripeHeight;

            Random rnd = new Random();

            for (int stripe = 0; stripe < NUMBER_OF_COLORS; stripe++)
            {
                Console.WriteLine("Stripe #" + stripe);

                int sumOfAllDistances = 0;
                for (int colorIndex = 0; colorIndex < NUMBER_OF_COLORS; colorIndex++)
                {
                    sumOfAllDistances += Math.Abs(colorIndex - stripe);
                }
                Console.WriteLine("  Sum of all distances: " + sumOfAllDistances);

                decimal sumOfAllWeights = 0;
                decimal minWeight = decimal.MaxValue, maxWeight = decimal.MinValue;
                IList<decimal> weights = new List<decimal>();
                for (int colorIndex = 0; colorIndex < NUMBER_OF_COLORS; colorIndex++)
                {
                    int distance = Math.Abs(colorIndex - stripe);
                    decimal weight = (decimal)(sumOfAllDistances - distance) / sumOfAllDistances;

                    weights.Add(weight);

                    sumOfAllWeights += weight;
                    if (weight < minWeight) minWeight = weight;
                    if (weight > maxWeight) maxWeight = weight;

                    Console.WriteLine(String.Format("    Stripe #{0}, Color #{1}: Weight: {2}", stripe, colorIndex, weight));
                }
                Console.WriteLine("  min weight: " + minWeight);
                Console.WriteLine("  max weight: " + maxWeight);
                Console.WriteLine("  Sum of all weights: " + sumOfAllWeights);

                decimal sumOfAllAdjustedWeights = 0;
                IList<decimal> adjustedWeights = new List<decimal>();
                for (int colorIndex = 0; colorIndex < NUMBER_OF_COLORS; colorIndex++)
                {
                    decimal adjustedWeight = ((weights[colorIndex] - minWeight) / ((maxWeight - minWeight)));

                    adjustedWeights.Add(adjustedWeight);

                    sumOfAllAdjustedWeights += adjustedWeight;
                    Console.WriteLine(String.Format("    Stripe #{0}, Color #{1}: Ajusted weight: {2}", stripe, colorIndex, adjustedWeight));
                }
                Console.WriteLine("  Sum of all adjusted weights: " + sumOfAllAdjustedWeights);

                decimal sumOfAllPercentages = 0;
                IList<decimal> percentages = new List<decimal>();
                for (int colorIndex = 0; colorIndex < NUMBER_OF_COLORS; colorIndex++)
                {
                    decimal percentage = adjustedWeights[colorIndex] / sumOfAllAdjustedWeights;

                    percentages.Add(percentage);

                    sumOfAllPercentages += percentage;

                    Console.WriteLine(String.Format("    Stripe #{0}, Color #{1}: Percentage: {2}", stripe, colorIndex, percentage));
                }
                Console.WriteLine("  Sum of all percentages: " + sumOfAllPercentages);



                int totalPixelsSet = 0;
                for (int colorIndex = 0; colorIndex < colors.Length; colorIndex++)
                {
                    Console.WriteLine("  Color index: " + colorIndex);

                    int distance = Math.Abs(colorIndex - stripe);

                    double weight = (sumOfAllDistances - distance) / sumOfAllDistances;

                    int numberOfPixelsOfThisColor = (int)Math.Floor((SCR_WIDTH * stripeHeight) * percentages[colorIndex]);

                    Console.WriteLine(String.Format("    setting {0} pixels of color {1}: ", numberOfPixelsOfThisColor, colorIndex));

                    for (int j = 0; j < numberOfPixelsOfThisColor; j++)
                    {
                        int x = 0, y = 0;
                        do
                        {
                            x = rnd.Next(0, SCR_WIDTH);
                            y = rnd.Next(stripe * stripeHeight, (stripe * stripeHeight) + stripeHeight);

                            var t = bmp.GetPixel(x, y);

                            if (t.Name != "ffff00ff")
                            {
                            }

                        } while (bmp.GetPixel(x, y).Name != "ffff00ff");
                        bmp.SetPixel(x, y, colors[colorIndex]);
                        totalPixelsSet++;
                        //Console.WriteLine("    Pixels set: " + totalPixelsSet);
                    }
                }

            }


            bmp.Save("test.bmp");

            Console.WriteLine("Done.");
        }

        public static void Convert2ColorImageIntoImageForPaletteCycling()
        {
            const int SCR_WIDTH = 256;
            const int SCR_HEIGHT = 192;
            string BG_COLOR = "ffffffff";

            var sourceImgFileName = @"MsxWings\msx-wings title screen 1.png";
            var imgCyclingFileName = @"MsxWings\palette cycling base.bmp";
            var destImgFileName = @"msx-wings title with cycling palette.bmp";

            Bitmap bmpSource = (Bitmap)Image.FromFile(sourceImgFileName);
            Bitmap bmpCycling = (Bitmap)Image.FromFile(imgCyclingFileName);
            Bitmap bmpDestiny = new Bitmap(SCR_WIDTH, SCR_HEIGHT);

            //int xOffset = (bmpSource.Width - bmpCycling.Width) / 2;
            int? yOffset = null;

            for (int y = 0; y < SCR_HEIGHT; y++)
            {
                for (int x = 0; x < SCR_WIDTH; x++)
                {
                    var pixel = bmpSource.GetPixel(x, y);

                    if (pixel.Name != BG_COLOR)
                    {
                        if (yOffset == null) yOffset = y;

                        int yOffSetCapped = (y - (int)yOffset <= bmpCycling.Height - 1) ? y - (int)yOffset : bmpCycling.Height - 1;

                        var pixelCycling = bmpCycling.GetPixel(x, yOffSetCapped);
                        bmpDestiny.SetPixel(x, y, pixelCycling);
                    }
                    else
                    {
                        bmpDestiny.SetPixel(x, y, pixel);
                    }
                }
            }

            bmpDestiny.Save(destImgFileName);

            //Console.WriteLine("Done.");
        }
    }
}

