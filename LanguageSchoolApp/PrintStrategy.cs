using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageSchoolApp
{
    public interface PrintColourStrategy /// The 'Strategy' abstract class
    {
        public void Print(string message);
    }
    public class PrintRed : PrintColourStrategy
    {
        public void Print(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine("");
        }
    }
    public class PrintGreen : PrintColourStrategy
    {
        public void Print(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine("");
        }
    }

    public class PrintYellow : PrintColourStrategy
    {
        public void Print(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine("");
        }
    }
}
