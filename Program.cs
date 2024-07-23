using System;
using System.Diagnostics;
using System.Drawing;

namespace TelCo.ColorCoder
{
    /// <summary>
    /// The 25-pair color code, originally known as even-count color code, 
    /// is a color code used to identify individual conductors in twisted-pair 
    /// wiring for telecommunications.
    /// This class provides the color coding and 
    /// mapping of pair number to color and color to pair number.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Array of Major colors
        /// </summary>
        private static Color[] colorMapMajor;
        /// <summary>
        /// Array of minor colors
        /// </summary>
        private static Color[] colorMapMinor;
        /// <summary>
        /// data type defined to hold the two colors of clor pair
        /// </summary>
         internal class ColorPair
        {
            internal Color majorColor;
            internal Color minorColor;
            public override string ToString()
            {
                return string.Format("MajorColor:{0}, MinorColor:{1}", majorColor.Name, minorColor.Name);
            }
        }
        /// <summary>
        /// Static constructor required to initialize static variable
        /// </summary>
       static Program()
        {
            colorMapMajor = new Color[] { Color.White, Color.Red, Color.Black, Color.Yellow, Color.Violet };
            colorMapMinor = new Color[] { Color.Blue, Color.Orange, Color.Green, Color.Brown, Color.SlateGray };
        }
        /// <summary>
        /// Given a pair number function returns the major and minor colors in that order
        /// </summary>
        /// <param name="pairNumber">Pair number of the color to be fetched</param>
        /// <returns></returns>
         public static ColorPair GetColorFromPairNumber(int pairNumber)
        {
            ValidatePairNumber(pairNumber);

            int minorSize = colorMapMinor.Length;
            int majorSize = colorMapMajor.Length;

            int zeroBasedPairNumber = pairNumber - 1;
            int majorIndex = zeroBasedPairNumber / minorSize;
            int minorIndex = zeroBasedPairNumber % minorSize;

            return new ColorPair() { majorColor = colorMapMajor[majorIndex], minorColor = colorMapMinor[minorIndex] };
        }

        /// <summary>
        /// Given the two colors the function returns the pair number corresponding to them
        /// </summary>
        /// <param name="pair">Color pair with major and minor color</param>
        /// <returns></returns>
         public static int GetPairNumberFromColor(ColorPair pair)
        {
            int majorIndex = Array.IndexOf(colorMapMajor, pair.majorColor);
            int minorIndex = Array.IndexOf(colorMapMinor, pair.minorColor);

            if (majorIndex == -1 || minorIndex == -1)
            {
                throw new ArgumentException($"Unknown Colors: {pair}");
            }

            return (majorIndex * colorMapMinor.Length) + (minorIndex + 1);
        }

        private static void ValidatePairNumber(int pairNumber)
        {
            int maxPairNumber = colorMapMajor.Length * colorMapMinor.Length;
            if (pairNumber < 1 || pairNumber > maxPairNumber)
            {
                throw new ArgumentOutOfRangeException(
                    $"Argument PairNumber:{pairNumber} is outside the allowed range");
            }
        }

        /// <summary>
        /// Test code for the class
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            TestGetColorFromPairNumber();
            TestGetPairNumberFromColor();
        }
         private static void TestGetColorFromPairNumber()
        {
            var tests = new[]
            {
                new { pairNumber = 4, majorColor = Color.White, minorColor = Color.Brown },
                new { pairNumber = 5, majorColor = Color.White, minorColor = Color.SlateGray },
                new { pairNumber = 23, majorColor = Color.Violet, minorColor = Color.Green }
            };

            foreach (var test in tests)
            {
                ColorPair result = GetColorFromPairNumber(test.pairNumber);
                Console.WriteLine($"[In]Pair Number: {test.pairNumber}, [Out] Colors: {result}");
                Debug.Assert(result.majorColor == test.majorColor);
                Debug.Assert(result.minorColor == test.minorColor);
            }
        }

        private static void TestGetPairNumberFromColor()
        {
            var tests = new[]
            {
                new { majorColor = Color.Yellow, minorColor = Color.Green, expectedPairNumber = 18 },
                new { majorColor = Color.Red, minorColor = Color.Blue, expectedPairNumber = 6 }
            };

            foreach (var test in tests)
            {
                ColorPair colorPair = new ColorPair() { majorColor = test.majorColor, minorColor = test.minorColor };
                int result = GetPairNumberFromColor(colorPair);
                Console.WriteLine($"[In]Colors: {colorPair}, [Out] PairNumber: {result}");
                Debug.Assert(result == test.expectedPairNumber);
            }
        }
    }
}
