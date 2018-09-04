using BestBankPortugalConnect;
using System;
using System.Linq;

namespace ExampleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Welcome to the test console app!\n\n");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("This is a VERY simple console application where you can follow step by step in the code.\n\nAfter setting the basic configuration, you wil able to see balance of a Banco Bank (Best Bank) account.\n\n");
                Common.Wait();

                Application application = ConsoleAplicationHelper.GetApplication();

                Console.Write("Do you already have an ACCESS TOKEN?");

                var hasAccessToken = Common.AskYesNo();
                string accessToken = String.Empty;

                #region Authorization Flow
                if (!hasAccessToken)
                {
                    Console.Clear();
                    Console.WriteLine("Let's start the authorization flow to get a new ACCESS TOKEN.\nWe will connect now to Banco Best using your own application keys. We will make the first step now.\n");
                    Common.Wait();
                    Console.WriteLine("\n> Connecting to Banco Best API...");

                    Console.WriteLine("\n> Attempting to get an URL Login...");
                    var urlLogin = AuthorizationFlow.GetBankLoginUrl(application, "http://httpbin.org/get");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n> SUCCESS!\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    var copyUrlConfirmation = false;

                    do
                    {
                        Console.WriteLine("\nNow you MUST to copy the URL below and navigate the Banco Best Website in your browser.\n\n");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("{0}\n\n", urlLogin.AbsoluteUri);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\n\nDid you copy the URL above?");
                        copyUrlConfirmation = Common.AskYesNo();
                    } while (!copyUrlConfirmation);

                    Console.Clear();
                    Console.WriteLine("Now you need to navigate to the URL that you copied in your brownser.\n\nWe are now waiting you to complete the login process on the Banco Best website.\n\nAfter sucessful login, you will be redirected to the Httpbin.org website.\nPlease get the code that is returned in the URL and continue this flow.");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n\nDon't continue this flow before get the temporary code.\n\n", urlLogin.AbsoluteUri);
                    Console.ForegroundColor = ConsoleColor.White;
                    Common.Wait();

                    string temporaryKey = string.Empty;
                    bool isTemporaryKeyCorrect = false;

                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Write below the code returned in the URL:\n\n");
                        Console.WriteLine("Code (Temporary Code): ");
                        temporaryKey = Common.ReadLine();
                        Console.Clear();
                        Console.WriteLine("Code (Temporary Code): {0}\n\n\nIs it correct?", temporaryKey);
                        isTemporaryKeyCorrect = Common.AskYesNo();

                    } while (!isTemporaryKeyCorrect);

                    Console.Clear();

                    Console.WriteLine("It's time to change this Tempory Code to, finally, get an Access Token. We will connect to the Banco Best API.\n");
                    Common.Wait();
                    Console.WriteLine("\n> Attempting to the Access Key...");
                    accessToken = AuthorizationFlow.GetUserAccessToken(application, temporaryKey).AccessToken;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n> SUCCESS!");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n> Access TOKEN: {0}\n\nPlease copy the Access Key above if you want to make bank operations in the future.", accessToken);
                    Console.ForegroundColor = ConsoleColor.White;
                    Common.Wait();
                }
                #endregion
                else
                {
                    Console.Clear();
                    Console.WriteLine("Access Token: ");
                    accessToken = Common.ReadLine();
                }

                Console.Clear();

                var user = new User(accessToken);

                Console.WriteLine("\n> Attempting to access bank accounts...");

                BestBankConnector connector = new BestBankConnector(user, application);

                var accounts = connector.Assets();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n> SUCCESS! ");
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write("It was loaded {0} account(s) and/or credit cards.\n\n", accounts.Count);
                Common.Wait();

                while (true)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Virtual Bank Account Panel\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nAccounts/Credit Cards:\n");

                    for (int i = 0; i < accounts.Count; i++)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("{0}", i + 1);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" - {0} ID:{1}\n", accounts[i].Type, accounts[i].ID);
                    }

                    Console.WriteLine("\nType the number of the account/card which you want to get information:\n");

                    var selection = Common.ReadLine();
                    int selectionNumber = 0;

                    if(int.TryParse(selection, out selectionNumber) && selectionNumber <= accounts.Count)
                    {
                        var account = accounts[selectionNumber - 1];

                        var balance = connector.Balance().Single(s => s.ID == account.ID);
                        Console.Clear();
                        Console.WriteLine("Your current balance in this account/card is: {0} {1}\n\n", account.Currency, double.Parse(balance.Balance, System.Globalization.CultureInfo.InvariantCulture));
                        Common.Wait();

                    } else
                    {
                        Console.WriteLine("Please type a valid number");
                        Common.Wait();
                    }

                }


            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("\n> ERROR: {0}\n\nSorry an error has ocurred. Please try to fix the issues and try this flow again.", e.Message);
                Common.Wait();
                System.Environment.Exit(0);
            }

        }

    }
}
