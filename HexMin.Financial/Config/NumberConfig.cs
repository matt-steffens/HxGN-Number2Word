using System.Linq;

namespace HexMin.Financial.Config
{
    public class NumberConfig
    {
        public static string[] Units => new[] { UnitNames.One, UnitNames.Two, UnitNames.Three, UnitNames.Four, UnitNames.Five, UnitNames.Six, UnitNames.Seven, UnitNames.Eight, UnitNames.Nine };

        public static string[] Teens => new[] { TeenNames.Eleven, TeenNames.Twelve, TeenNames.Thirteen, TeenNames.Fourteen, TeenNames.Fifteen, TeenNames.Sixteen, TeenNames.Seventeen, TeenNames.Eighteen, TeenNames.Nineteen };

        public static string[] Tens => new[] { TenNames.Ten, TenNames.Twenty, TenNames.Thirty, TenNames.Forty, TenNames.Fifty, TenNames.Sixty, TenNames.Seventy, TenNames.Eighty, TenNames.Ninety };

        public static string[] AvailableDigitGroups => new[] { DigitGroupNames.Hundred, DigitGroupNames.Thousand, DigitGroupNames.Million, DigitGroupNames.Billion, DigitGroupNames.Trillion, DigitGroupNames.Quadrillion };

        public static string[] DigitGroups { get; private set; } = SetDigitGroups(3);

        public static string[] SetDigitGroups(int indexDigitGroup)
        {
            DigitGroups = AvailableDigitGroups.Select((dg, idx) => new { idx, Name = dg }).Where(x => x.idx <= indexDigitGroup).Select(x => x.Name).ToArray();

            return DigitGroups;
        }
    }
}