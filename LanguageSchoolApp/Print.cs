using System;
using System.Collections.Generic;
using System.Text;

namespace LanguageSchoolApp
{
    public interface PrintColourStrategy /// The 'Strategy' abstract class
    {
        public void Print(string message);
    }
    public class Printer
    {        
        private PrintColourStrategy _printstrategy; //Strategy
        public void PrintColour(PrintColourStrategy printstrategy, string message)
        {
            this._printstrategy = printstrategy;
            _printstrategy.Print(message);
        }
    }
    public class PrintRed : PrintColourStrategy
    {
        public void Print(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(message);
            Console.ResetColor();
        }
    }
    public class PrintGreen : PrintColourStrategy
    {
        public void Print(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(message);
            Console.ResetColor();
        }
    }

    public class PrintYellow : PrintColourStrategy
    {
        public void Print(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(message);
            Console.ResetColor();
        }
    }
    public class PrintCyan : PrintColourStrategy
    {
        public void Print(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(message);
            Console.ResetColor();
        }
    }
    public class PrintMagenta : PrintColourStrategy
    {
        public void Print(string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(message);
            Console.ResetColor();
        }
    }
    public class PrintBlue : PrintColourStrategy
    {
        public void Print(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(message);
            Console.ResetColor();
        }
    }
}
