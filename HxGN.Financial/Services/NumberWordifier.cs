using System;
using System.Collections.Generic;
using System.Linq;
using HxGN.Financial.Config;

namespace HxGN.Financial.Services
{
    public class NumberWordifier
    {
        public string Wordify(decimal number, int decimalRound = 2)
        {
            const string zero = "zero";

            var isNegative = number < 0;
            var decimalStep = (long)Math.Pow(10, decimalRound);

            //number = Math.Truncate(decimalStep * number) / decimalStep;
            number = Math.Round(Math.Abs(number), decimalRound);

            var integralValue = (long)Math.Truncate(number);
            var fractionValue = (int)((number - integralValue) * decimalStep);

            var integralSize = Math.Floor(Math.Log10(integralValue) + 1);
            var fractionSize = Math.Floor(Math.Log10(fractionValue) + 1);

            var maxNumberSize = NumberConfig.DigitGroups.Length * 3;

            if (integralSize > maxNumberSize)
            {
                throw new Exception($"Integral segment of the number entered exceeds compatible number size of {maxNumberSize} digits!");
            }

            if (fractionSize > maxNumberSize)
            {
                throw new Exception($"Fraction segment of the number entered exceeds compatible number size of {maxNumberSize} decimals!");
            }

            var integralWords = integralValue != 0 ? Wordify(integralValue).Trim() : zero;
            var fractionWords = fractionValue != 0 ? Wordify(fractionValue, onlyCents: true, decimalRound: decimalRound).Trim() : zero;

            return $"{(isNegative ? "NEGATIVE " : string.Empty)}{integralWords} dollar{(integralWords != "one" ? "s" : string.Empty)} and {fractionWords} cent{(fractionWords != "one" ? "s" : string.Empty)}".ToUpper();
        }

        private static string Wordify(long number, bool onlyCents = false, int decimalRound = 2)
        {
            var powerLimit = !onlyCents ? NumberConfig.DigitGroups.Length * 3 : decimalRound;
            var numberItems = new List<NumberItem>();

            for (var power = powerLimit - 1; power >= 0; power--)
            {
                var tenPower = (long)Math.Pow(10, power);
                var quotient = (int)(number / tenPower);

                numberItems.Add(new NumberItem { Value = quotient, Power = power });
                number %= tenPower;
            }

            AdjustNumberItemsForDigitGroupsIfRequired(numberItems);

            return ConvertNumberItemsToWords(numberItems);
        }

        private static void AdjustNumberItemsForDigitGroupsIfRequired(IList<NumberItem> numberItems)
        {
            while (numberItems.Count % 3 != 0)
            {
                numberItems.Insert(0, new NumberItem {Value = 0, Power = numberItems.Max(ni => ni.Power) + 1});
            }
        }

        private static string ConvertNumberItemsToWords(IEnumerable<NumberItem> numberItems)
        {
            var word = string.Empty;

            var numberDigitGroups =
                numberItems
                .GroupBy(ni =>
                    ni.Power / 3, // Group key
                    (key, grps) => new { Key = key, GroupNumItems = grps.ToList() });

            foreach (var digitGroup in numberDigitGroups)
            {
                var groupNumItems = digitGroup.GroupNumItems;
                var firstInGroup = groupNumItems.First().Value;
                var secondInGroup = groupNumItems.Skip(1).First().Value;
                var thirdInGroup = groupNumItems.Skip(2).FirstOrDefault()?.Value ?? 0;

                if (firstInGroup > 0) // First item in a group with the value.
                {
                    var unitWord = NumberConfig.Units[firstInGroup - 1];
                    word += $"{(!string.IsNullOrWhiteSpace(word) ? " " : string.Empty)}{unitWord} {NumberConfig.DigitGroups[0]}";
                }

                if (secondInGroup > 0) // Second item in a group with the value.
                {
                    if (secondInGroup >= 1 && thirdInGroup == 0)
                    {
                        var tenWord = NumberConfig.Tens[secondInGroup - 1];
                        word += $"{GetConjuctionWord(word)} {tenWord}";
                    }
                    else if (secondInGroup > 1)
                    {
                        var tenWord = NumberConfig.Tens[secondInGroup - 1];
                        var unitWord = NumberConfig.Units[thirdInGroup - 1];
                        word += $"{GetConjuctionWord(word)} {tenWord} {unitWord}";
                    }
                    else
                    {
                        var teensWord = NumberConfig.Teens[thirdInGroup - 1];
                        word += $"{GetConjuctionWord(word)} {teensWord}";
                    }
                }
                else if (thirdInGroup > 0) // Only third item in a group with the value.
                {
                    var unitWord = NumberConfig.Units[thirdInGroup - 1];
                    word += $"{GetConjuctionWord(word)} {unitWord}";
                }

                var anyGroupDigits = firstInGroup > 0 || secondInGroup > 0 || thirdInGroup > 0;

                if (anyGroupDigits && digitGroup.Key > 0)
                {
                    // Append a word of a digit group if applicable.
                    word += $" {NumberConfig.DigitGroups[digitGroup.Key]}";
                }
            }

            return word;
        }

        private static string GetConjuctionWord(string word)
        {
            return !string.IsNullOrWhiteSpace(word) ? " and" : string.Empty;
        }

        private class NumberItem
        {
            public int Power { get; set; }

            public int Value { get; set; }
        }
    }
}