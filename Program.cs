using System;
using System.Diagnostics;
using System.Drawing;

namespace TelCo.ColorCoder
{
    class Program
    {
        private static readonly Color[] colorMapMajor;
        private static readonly Color[] colorMapMinor;

        internal class ColorPair
        {
            internal Color majorColor;
            internal Color minorColor;

            public override string ToString()
            {
                return string.Format("MajorColor:{0}, MinorColor:{1}", majorColor.Name, minorColor.Name);
            }
        }

        static Program()
        {
            colorMapMajor = new Color[] { Color.White, Color.Red, Color.Black, Color.Yellow, Color.Violet };
            colorMapMinor = new Color[] { Color.Blue, Color.Orange, Color.Green, Color.Brown, Color.SlateGray };
        }

        public static ColorPair GetColorFromPairNumber(int pairNumber)
        {
            ValidatePairNumber(pairNumber);

            int zeroBasedPairNumber = pairNumber - 1;
            int majorIndex = zeroBasedPairNumber / colorMapMinor.Length;
            int minorIndex = zeroBasedPairNumber % colorMapMinor.Length;

            return new ColorPair() { majorColor = colorMapMajor[majorIndex], minorColor = colorMapMinor[minorIndex] };
        }

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

        private static void Main(string[] args)
        {
            TestGetColorFromPairNumber();
            TestGetPairNumberFromColor();
        }

        private static void TestGetColorFromPairNumber()
        {
            var tests = new[]
            {
                new { pairNumber = 1, majorColor = Color.White, minorColor = Color.Blue },
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
                new { majorColor = Color.White, minorColor = Color.Blue, expectedPairNumber = 1 },
                new { majorColor = Color.Yellow, minorColor = Color.Green, expectedPairNumber = 18 },
                new { majorColor = Color.Red, minorColor = Color.Blue, expectedPairNumber = 6 },
                new { majorColor = Color.Black, minorColor = Color.Brown, expectedPairNumber = 14 }
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
