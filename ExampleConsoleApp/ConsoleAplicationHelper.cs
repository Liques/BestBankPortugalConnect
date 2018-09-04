using BestBankPortugalConnect;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleConsoleApp
{
    public static class ConsoleAplicationHelper
    {
        public static Application GetApplication()
        {
            Console.Clear();
            Console.Write("Do you already have registred an Application at the Banco Best Developer website?");

            var isAppRegistered = Common.AskYesNo();

            while (!isAppRegistered)
            {
                Console.Clear();
                Console.WriteLine("You need to register your application at the Banco Best Developer website.");
                Console.WriteLine(@"It will provide you the ""Consumer Key"" and the ""Consumer Secret Key"".");
                Console.WriteLine("Without these keys is not possible to continue. Please, register your self if you are not registred and register an application at the website.\n");

                Console.WriteLine("Please do it now. I'm waiting you here :-)\n\n");

                Console.ReadKey();

                Console.WriteLine("Did you register your application at the Banco Best website?");

                isAppRegistered = Common.AskYesNo();
            }

            string consumerKey = String.Empty;
            string consumerSecret = String.Empty;
            bool isSandbox = true;

            bool confirmData = false;

            do
            {

                Console.Clear();
                Console.WriteLine("Please type your application data below.\n");

                Console.WriteLine("\nConsumer Key: ");
                consumerKey = Common.ReadLine();

                Console.WriteLine("\nSecret Key: ");
                consumerSecret = Common.ReadLine();

                Console.Write("\n\nIs it a sandbox application? ");
                isSandbox = Common.AskYesNo();

                Console.Clear();

                Console.Write("Consumer Key:\n{0}\n\nSecret Key:\n{1}\n\nIs it a sandbox application?\n{2}\n\n\nIs it right?", consumerKey, consumerSecret, isSandbox ? "YES" : "NO");
                confirmData = Common.AskYesNo();


            } while (!confirmData);

            Console.Clear();

            BestBankPortugalConnect.Environment environment = isSandbox ? BestBankPortugalConnect.Environment.Sandbox : BestBankPortugalConnect.Environment.Production;

            return new Application(consumerKey, consumerSecret, environment);

        }

    }
}
