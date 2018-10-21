using NUnit.Framework;

namespace HxGN.Financial.Tests
{
    public class ValidInputsWithThreeDecimalsConfigTests : FinancialTestsBase
    {
        [TestCase(123.456, ExpectedResult = "ONE HUNDRED AND TWENTY THREE DOLLARS AND FOUR HUNDRED AND FIFTY SIX CENTS")]
        public string When_Specific_Value_Is_Entered_With_Specific_Decimal_Value(decimal inputValue)
        {
            return wrdSvc.Wordify(inputValue, decimalRound: 3);
        }

        [TestCase(999999999999.999, ExpectedResult = "NINE HUNDRED AND NINETY NINE BILLION NINE HUNDRED AND NINETY NINE MILLION NINE HUNDRED AND NINETY NINE THOUSAND NINE HUNDRED AND NINETY NINE DOLLARS AND NINE HUNDRED AND NINETY NINE CENTS")]
        public string When_Max_Billion_Value_Is_Entered_With_999_Decimal_Value(decimal inputValue)
        {
            return wrdSvc.Wordify(inputValue, decimalRound: 3);
        }
    }
}