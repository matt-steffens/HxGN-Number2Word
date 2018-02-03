using System;
using HexMin.Financial.Services;

namespace HexMin.Financial
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to HexMin Financial! (to terminate the console, please enter 'exit' command)");
            Console.WriteLine();
            Console.WriteLine("Please enter a number to be 'Wordified':");

            string inputValue;

            while ((inputValue = Console.ReadLine()) != "exit")
            {
                try
                {
                    var result = new NumberWordifier().Wordify(Convert.ToDecimal(inputValue), decimalRound: 2);

                    Console.WriteLine(result);
                    Console.WriteLine();
                }
                catch (FormatException)
                {
                    var defaultMessage = $"'{inputValue}' is not a valid number!";

                    if (string.IsNullOrWhiteSpace(inputValue))
                    {
                        defaultMessage = "No value entered!";
                    }

                    Console.WriteLine($"{defaultMessage} Please repeat entering a valid number.");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    Console.WriteLine();
                }
            }
        }
    }
}