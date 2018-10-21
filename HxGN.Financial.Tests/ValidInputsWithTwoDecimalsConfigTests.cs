using System;
using HxGN.Financial.Config;
using NUnit.Framework;

namespace HxGN.Financial.Tests
{
    [TestFixture]
    public class ValidInputsWithTwoDecimalsConfigTests : FinancialTestsBase
    {
        [TestCase(0.00, ExpectedResult = "ZERO DOLLARS AND ZERO CENTS")]
        public string When_Zero_Value_Is_Entered_With_Zero_Decimal_Value(decimal inputValue)
        {
            return wrdSvc.Wordify(inputValue);
        }

        [TestCase(1.00, ExpectedResult = "ONE DOLLAR AND ZERO CENTS")]
        [TestCase(2.00, ExpectedResult = "TWO DOLLARS AND ZERO CENTS")]
        [TestCase(3.00, ExpectedResult = "THREE DOLLARS AND ZERO CENTS")]
        [TestCase(4.00, ExpectedResult = "FOUR DOLLARS AND ZERO CENTS")]
        [TestCase(5.00, ExpectedResult = "FIVE DOLLARS AND ZERO CENTS")]
        [TestCase(6.00, ExpectedResult = "SIX DOLLARS AND ZERO CENTS")]
        [TestCase(7.00, ExpectedResult = "SEVEN DOLLARS AND ZERO CENTS")]
        [TestCase(8.00, ExpectedResult = "EIGHT DOLLARS AND ZERO CENTS")]
        [TestCase(9.00, ExpectedResult = "NINE DOLLARS AND ZERO CENTS")]
        public string When_Unit_Value_Is_Entered_With_Zero_Decimal_Value(decimal inputValue)
        {
            return wrdSvc.Wordify(inputValue);
        }

        [TestCase(11.00, ExpectedResult = "ELEVEN DOLLARS AND ZERO CENTS")]
        [TestCase(12.00, ExpectedResult = "TWELVE DOLLARS AND ZERO CENTS")]
        [TestCase(13.00, ExpectedResult = "THIRTEEN DOLLARS AND ZERO CENTS")]
        [TestCase(14.00, ExpectedResult = "FOURTEEN DOLLARS AND ZERO CENTS")]
        [TestCase(15.00, ExpectedResult = "FIFTEEN DOLLARS AND ZERO CENTS")]
        [TestCase(16.00, ExpectedResult = "SIXTEEN DOLLARS AND ZERO CENTS")]
        [TestCase(17.00, ExpectedResult = "SEVENTEEN DOLLARS AND ZERO CENTS")]
        [TestCase(18.00, ExpectedResult = "EIGHTEEN DOLLARS AND ZERO CENTS")]
        [TestCase(19.00, ExpectedResult = "NINETEEN DOLLARS AND ZERO CENTS")]
        public string When_Teen_Value_Is_Entered_With_Zero_Decimal_Value(decimal inputValue)
        {
            return wrdSvc.Wordify(inputValue);
        }

        [TestCase(10.00, ExpectedResult = "TEN DOLLARS AND ZERO CENTS")]
        [TestCase(20.00, ExpectedResult = "TWENTY DOLLARS AND ZERO CENTS")]
        [TestCase(30.00, ExpectedResult = "THIRTY DOLLARS AND ZERO CENTS")]
        [TestCase(40.00, ExpectedResult = "FORTY DOLLARS AND ZERO CENTS")]
        [TestCase(50.00, ExpectedResult = "FIFTY DOLLARS AND ZERO CENTS")]
        [TestCase(60.00, ExpectedResult = "SIXTY DOLLARS AND ZERO CENTS")]
        [TestCase(70.00, ExpectedResult = "SEVENTY DOLLARS AND ZERO CENTS")]
        [TestCase(80.00, ExpectedResult = "EIGHTY DOLLARS AND ZERO CENTS")]
        [TestCase(90.00, ExpectedResult = "NINETY DOLLARS AND ZERO CENTS")]
        public string When_Ten_Value_Is_Entered_With_Zero_Decimal_Value(decimal inputValue)
        {
            return wrdSvc.Wordify(inputValue);
        }

        [TestCase(1.00, ExpectedResult = "ONE DOLLAR AND ZERO CENTS")]
        [TestCase(10.00, ExpectedResult = "TEN DOLLARS AND ZERO CENTS")]
        [TestCase(100.00, ExpectedResult = "ONE HUNDRED DOLLARS AND ZERO CENTS")]
        [TestCase(1000.00, ExpectedResult = "ONE THOUSAND DOLLARS AND ZERO CENTS")]
        [TestCase(10000.00, ExpectedResult = "TEN THOUSAND DOLLARS AND ZERO CENTS")]
        [TestCase(100000.00, ExpectedResult = "ONE HUNDRED THOUSAND DOLLARS AND ZERO CENTS")]
        [TestCase(1000000.00, ExpectedResult = "ONE MILLION DOLLARS AND ZERO CENTS")]
        [TestCase(10000000.00, ExpectedResult = "TEN MILLION DOLLARS AND ZERO CENTS")]
        [TestCase(100000000.00, ExpectedResult = "ONE HUNDRED MILLION DOLLARS AND ZERO CENTS")]
        [TestCase(1000000000.00, ExpectedResult = "ONE BILLION DOLLARS AND ZERO CENTS")]
        [TestCase(10000000000.00, ExpectedResult = "TEN BILLION DOLLARS AND ZERO CENTS")]
        [TestCase(100000000000.00, ExpectedResult = "ONE HUNDRED BILLION DOLLARS AND ZERO CENTS")]
        public string When_Power_Value_Is_Entered_With_Zero_Decimal_Value(decimal inputValue)
        {
            return wrdSvc.Wordify(inputValue);
        }

        [TestCase(1.01, ExpectedResult = "ONE DOLLAR AND ONE CENT")]
        [TestCase(10.01, ExpectedResult = "TEN DOLLARS AND ONE CENT")]
        public string When_Specific_Value_Is_Entered_With_01_Decimal_Value(decimal inputValue)
        {
            return wrdSvc.Wordify(inputValue);
        }

        [TestCase(123.456, ExpectedResult = "ONE HUNDRED AND TWENTY THREE DOLLARS AND FORTY SIX CENTS")]
        public string When_Specific_Value_Is_Entered_With_Specific_Decimal_Value(decimal inputValue)
        {
            return wrdSvc.Wordify(inputValue);
        }

        [Test]
        public void When_MaxBillion_Value_Is_Entered_With_999_Decimal_Value_An_Exception_Is_Thrown()
        {
            Assert.That(() => wrdSvc.Wordify(999999999999.999m), Throws.TypeOf<Exception>());
        }

        [Test]
        public void When_Max_Billion_Value_Is_Entered_With_999_Decimal_Value_And_Trilllion_DigitGroups_Set()
        {
            // Setting Digit Groups temporary to 'Trillion' for this edge case test only.
            NumberConfig.SetDigitGroups(Array.IndexOf(NumberConfig.AvailableDigitGroups, DigitGroupNames.Trillion));

            var result = wrdSvc.Wordify(999999999999.999m);

            Assert.That(result, Is.EqualTo("ONE TRILLION DOLLARS AND ZERO CENTS"));

            // Setting Digit Groups back to 'Billion' (default) for other tests.
            NumberConfig.SetDigitGroups(Array.IndexOf(NumberConfig.AvailableDigitGroups, DigitGroupNames.Billion));
        }
    }
}