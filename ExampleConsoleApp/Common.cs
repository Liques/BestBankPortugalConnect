using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleConsoleApp
{
    public static class Common
    {

        public static bool AskYesNo()
        {
            Console.Write(" (");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Y");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("/");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("N");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(") ");

            while (true)
            {
                var key = Console.ReadKey();

                if (key.Key == ConsoleKey.Y)
                {
                    return true;
                }
                else if (key.Key == ConsoleKey.N)
                {
                    return false;
                }
            }
        }

        public static string ReadLine()
        {
            var value = Console.ReadLine();

            while (String.IsNullOrEmpty(value)) 
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nIt's not a valid data. Please write again:\n\n");
                Console.ForegroundColor = ConsoleColor.White;
                value = Console.ReadLine();
            }

            return value;
        }

        public static void Wait()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\nType any key to continue...\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.ReadKey();
        }
    }
}
