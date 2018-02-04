using System;
using System.Linq;
using System.Threading;
using HexMin.Financial.Config;
using HexMin.Financial.Services;

namespace HexMin.Financial
{
    class Program
    {
        private const string ExitCode = "99";
        private const string Exit = "EXIT";

        private static int decimalRound = 2;
        private static bool anyOptionSelected;

        static void Main()
        {
            Console.WriteLine("Welcome to HexMin Financial!");
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

            while ((inputValue = Console.ReadLine()?.ToUpper()) != Exit)
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
            }
        }

        private static void WordifyNumbersOption()
        {
            const string PreviousMenuText = "To return to the previous menu, please type 'exit' command.";
            anyOptionSelected = true;

            Console.Clear();
            Console.WriteLine("Please enter a number below to be 'Wordified'.");
            Console.WriteLine(PreviousMenuText);
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
                    const string NoValueEntered = "No value entered!";

                    var defaultMessage = $"'{inputValue}' is not a valid number!";

                    if (string.IsNullOrWhiteSpace(inputValue))
                    {
                        defaultMessage = NoValueEntered;
                    }

                    Console.WriteLine($"{defaultMessage} Please repeat entering a valid number.{(defaultMessage == NoValueEntered ? " " + PreviousMenuText : string.Empty)}");
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

            while ((inputValue = Console.ReadLine()?.ToUpper()) != Exit)
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
            }
        }

        private static void DigitGroupLimitOption()
        {
            Console.Clear();
            Console.WriteLine("Digit Group Limit");
            Console.WriteLine("-----------------");

            foreach (var digitGroup in NumberConfig.AvailableDigitGroups.Select((dg, idx) => new { Idx = idx, Name = dg.ToUpper() }).ToArray())
            {
                Console.WriteLine($"{digitGroup.Idx + 1}) {digitGroup.Name}{(digitGroup.Idx + 1 == NumberConfig.DigitGroups.Length ? " (Current)" : string.Empty)}");
            }

            Console.WriteLine($"{ExitCode}) Exit");
            Console.WriteLine();

            var inputValue = Console.ReadLine();

            if (inputValue != ExitCode)
            {
                var indexDigitGroup = Convert.ToInt32(inputValue) - 1;

                NumberConfig.SetDigitGroups(indexDigitGroup);

                Console.WriteLine();
                Console.WriteLine($"Digit Group Limit has been set to '{NumberConfig.AvailableDigitGroups[indexDigitGroup].ToUpper()}'.");

                Thread.Sleep(2000);
                SetupOption();
            }
            else
            {
                SetupOption();
            }
        }

        private static void DecimalRoundingOption()
        {
            Console.Clear();
            Console.WriteLine("Decimal Rounding");
            Console.WriteLine("----------------");

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{i}) {i} Decimal{(i > 1 ? "s" : string.Empty)}{(i == decimalRound ? " (Current)" : string.Empty)}");
            }

            Console.WriteLine($"{ExitCode}) Exit");
            Console.WriteLine();

            var inputValue = Console.ReadLine();

            if (inputValue != ExitCode)
            {
                decimalRound = Convert.ToInt32(inputValue);

                Console.WriteLine();
                Console.WriteLine($"Decimal Rounding has been set to '{decimalRound}'.");

                Thread.Sleep(2000);
                SetupOption();
            }
            else
            {
                SetupOption();
            }
        }
        
        private static void TerminateOption()
        {
            Console.Clear();
            Console.WriteLine("Thank you for using HexFin Financial!");
            ProductInfo();

            Thread.Sleep(4000);

            Environment.Exit(0);
        }

        private static void ProductInfo()
        {
            Console.WriteLine("Version 1.0.0");
            Console.WriteLine("© 2018 Matt Steffens.");
            Console.WriteLine();
        }
    }
}