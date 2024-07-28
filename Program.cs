using System;
using System.Diagnostics;
using System.Drawing;
namespace TelCo.ColorCoder
{
    class Program
    {
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
                ColorPair result = ColorMapper.GetColorFromPairNumber(test.pairNumber);
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
                int result = ColorMapper.GetPairNumberFromColor(colorPair);
                Console.WriteLine($"[In]Colors: {colorPair}, [Out] PairNumber: {result}");
                Debug.Assert(result == test.expectedPairNumber);
            }
        }
    }
}
