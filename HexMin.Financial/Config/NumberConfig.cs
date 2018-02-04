using System.Linq;

namespace HexMin.Financial.Config
{
    public class NumberConfig
    {
        public static string[] Units => new[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        public static string[] Teens => new[] { "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

        public static string[] Tens => new[] { "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        public static string[] AvailableDigitGroups => new[] { "hundred", "thousand", "million", "billion", "trillion", "quadrillion" };

        public static string[] DigitGroups { get; private set; } = SetDigitGroups(3);

        public static string[] SetDigitGroups(int indexDigitGroup)
        {
            DigitGroups = AvailableDigitGroups.Select((dg, idx) => new { idx, Name = dg }).Where(x => x.idx <= indexDigitGroup).Select(x => x.Name).ToArray();

            return DigitGroups;
        }
    }
}