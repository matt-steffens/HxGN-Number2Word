namespace HexMin.Financial.Config
{
    public class NumberConfig
    {
        public static string[] Units => new[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        public static string[] Teens => new[] { "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

        public static string[] Tens => new[] { "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        // public static string[] DigitGroups => new[] { "hundred", "thousand", "million", "billion" };

        public static string[] DigitGroups => new[] { "hundred", "thousand", "million", "billion", "trillion", "quadrillion" };
    }
}