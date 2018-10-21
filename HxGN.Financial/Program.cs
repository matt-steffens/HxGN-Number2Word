using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using HexMin.Financial.Config;
using HexMin.Financial.Services;

namespace HexMin.Financial
{
    class Program
    {
        private const string ProductName = "HexMin Financial";
        private const string ExitCode = "99";
        private const string Exit = "EXIT";

        private static int decimalRound = 2;
        private static bool anyOptionSelected;

        static void Main()
        {
            Console.Clear();
            Console.WriteLine($"Welcome to {ProductName}!");
            ProductInfo();

            MainMenuOption();
        }

        private static void MainMenuOption()
        {
            if (anyOptionSelected)
            {
                Console.Clear();
            }

            Console.WriteLine("Main Menu");
            Console.WriteLine("---------");
            Console.WriteLine("1) Wordify Numbers");
            Console.WriteLine("2) Setup");
            Console.WriteLine($"{ExitCode}) Exit");
            Console.WriteLine();

            string inputValue;

            while ((inputValue = Console.ReadLine()?.ToUpper()) != $"{Exit}*")
            {
                if (inputValue == "1")
                {
                    WordifyNumbersOption();
                }
                else if (inputValue == "2")
                {
                    SetupOption();
                }
                else if (inputValue == ExitCode)
                {
                    TerminateOption();
                }
                else
                {
                    ShowInvalidInputMessage(inputValue);
                }
            }
        }

        private static void WordifyNumbersOption()
        {
            const string previousMenuText = "To return to the previous menu, please type 'exit' command.";
            anyOptionSelected = true;

            Console.Clear();
            Console.WriteLine("Please enter a number below to be 'Wordified'.");
            Console.WriteLine(previousMenuText);
            Console.WriteLine();

            string inputValue;

            while ((inputValue = Console.ReadLine()?.ToUpper()) != Exit)
            {
                try
                {
                    var result = new NumberWordifier().Wordify(Convert.ToDecimal(inputValue), decimalRound: decimalRound);

                    Console.WriteLine(result);
                    Console.WriteLine();
                }
                catch (FormatException)
                {
                    const string noValueEntered = "No value entered!";

                    var defaultMessage = $"'{inputValue}' is not a valid number!";

                    if (string.IsNullOrWhiteSpace(inputValue))
                    {
                        defaultMessage = noValueEntered;
                    }

                    Console.WriteLine($"{defaultMessage} Please retry entering a valid number.{(defaultMessage == noValueEntered ? " " + previousMenuText : string.Empty)}");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    Console.WriteLine();
                }
            }

            MainMenuOption();
        }

        private static void SetupOption()
        {
            anyOptionSelected = true;

            Console.Clear();
            Console.WriteLine("Setup");
            Console.WriteLine("-----");
            Console.WriteLine("1) Digit Group Limit");
            Console.WriteLine("2) Decimal Rounding");
            Console.WriteLine($"{ExitCode}) Exit");
            Console.WriteLine();

            string inputValue;

            while ((inputValue = Console.ReadLine()?.ToUpper()) != $"{Exit}*")
            {
                if (inputValue == "1")
                {
                    DigitGroupLimitOption();
                }
                else if (inputValue == "2")
                {
                    DecimalRoundingOption();
                }
                else if (inputValue == ExitCode)
                {
                    MainMenuOption();
                }
                else
                {
                    ShowInvalidInputMessage(inputValue);
                }
            }
        }

        private static void DigitGroupLimitOption()
        {
            Console.Clear();
            Console.WriteLine("Digit Group Limit");
            Console.WriteLine("-----------------");

            var digitGroups = NumberConfig.AvailableDigitGroups.Select((dg, idx) => new { Idx = idx, Name = dg.ToUpper() }).ToArray();
            var validValues = digitGroups.Select(x => x.Idx + 1).ToList();

            foreach (var digitGroup in digitGroups)
            {
                Console.WriteLine($"{digitGroup.Idx + 1}) {digitGroup.Name}{(digitGroup.Idx + 1 == NumberConfig.DigitGroups.Length ? " (Current)" : string.Empty)}");
            }

            Console.WriteLine($"{ExitCode}) Exit");
            Console.WriteLine();

            string inputValue;

            while ((inputValue = Console.ReadLine()?.ToUpper()) != $"{Exit}*")
            {
                var input = ConvertInputToNumber(inputValue);

                if (!input.HasValue)
                {
                    continue;
                }

                if (validValues.Contains(input.Value))
                {
                    var indexDigitGroup = Convert.ToInt32(inputValue) - 1;

                    NumberConfig.SetDigitGroups(indexDigitGroup);

                    Console.WriteLine();
                    Console.WriteLine($"Digit Group Limit has been set to '{NumberConfig.AvailableDigitGroups[indexDigitGroup].ToUpper()}'.");

                    Thread.Sleep(2000);
                    SetupOption();
                }
                else if (inputValue == ExitCode)
                {
                    SetupOption();
                }
                else
                {
                    ShowInvalidInputMessage(inputValue);
                }
            }
        }

        private static void DecimalRoundingOption()
        {
            Console.Clear();
            Console.WriteLine("Decimal Rounding");
            Console.WriteLine("----------------");

            var validValues = Enumerable.Range(2, 4).ToList();

            foreach (var value in validValues)
            {
                Console.WriteLine($"{value}) {value} Decimal{(value > 1 ? "s" : string.Empty)}{(value == decimalRound ? " (Current)" : string.Empty)}");
            }

            Console.WriteLine($"{ExitCode}) Exit");
            Console.WriteLine();

            string inputValue;

            while ((inputValue = Console.ReadLine()?.ToUpper()) != $"{Exit}*")
            {
                var input = ConvertInputToNumber(inputValue);

                if (!input.HasValue)
                {
                    continue;
                }

                if (validValues.Contains(input.Value))
                {
                    decimalRound = input.Value;

                    Console.WriteLine();
                    Console.WriteLine($"Decimal Rounding has been set to '{decimalRound}'.");

                    Thread.Sleep(2000);
                    SetupOption();
                }
                else if (inputValue == ExitCode)
                {
                    SetupOption();
                }
                else
                {
                    ShowInvalidInputMessage(inputValue);
                }
            }
        }

        private static void TerminateOption()
        {
            Console.Clear();
            Console.WriteLine($"Thank you for using {ProductName}!");
            ProductInfo();

            Thread.Sleep(4000);

            Environment.Exit(0);
        }

        private static int? ConvertInputToNumber(string inputValue)
        {
            int? result = null;

            try
            {
                result = Convert.ToInt32(inputValue);
            }
            catch (FormatException)
            {
                ShowInvalidInputMessage(inputValue);
            }

            return result;
        }

        private static void ShowInvalidInputMessage(string inputValue)
        {
            Console.WriteLine(!string.IsNullOrEmpty(inputValue) ? $"'{inputValue}' is not a valid input! Please retry." : "No value entered!");
            Console.WriteLine();
        }

        private static void ProductInfo()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            var fileVesionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);

            Console.WriteLine($"Version {version.Major}.{version.Minor}.{version.Build}");
            Console.WriteLine($"{fileVesionInfo.LegalCopyright} {fileVesionInfo.CompanyName}.");
            Console.WriteLine();
        }
    }
}