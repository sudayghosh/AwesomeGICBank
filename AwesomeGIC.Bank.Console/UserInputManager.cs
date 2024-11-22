using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Console
{
    public class UserInputManager
    {
        public UserInputManager()
        {
        }

        public void ShowPrimaryQuestions()
        {
            System.Console.WriteLine("[T] Input transactions");
            System.Console.WriteLine("[I] Define interest rules");
            System.Console.WriteLine("[P] Print statement");
            System.Console.WriteLine("[Q] Quit");
        }

        public bool IsQuit(string inp)
        {
            if (string.Equals(inp, "Q", StringComparison.OrdinalIgnoreCase))
            {
                System.Console.WriteLine("Thank you for banking with AwesomeGIC Bank.\r\nHave a nice day!");
                return true;
            }
            return false;
        }

        public bool DoProcess(string? inputType)
        {
            switch (inputType)
            {
                case "T":
                case "t":
                    System.Console.WriteLine("Please enter transaction details in <Date> <Account> <Type> <Amount> format \n(or enter blank to go back to main menu):");
                    ProcessTransactions();
                    break;
                case "I":
                case "i":
                    System.Console.WriteLine("Please enter interest rules details in <Date> <RuleId> <Rate in %> format \n(or enter blank to go back to main menu):");
                    DefineInterestRules();
                    break;
                case "P":
                case "p":
                    System.Console.WriteLine("Please enter account and month to generate the statement <Account> <Year><Month> \n(or enter blank to go back to main menu):");
                    PrintStatement();
                    break;
                default:
                    break;
            }
            return true;
        }

        /// <summary>
        /// Process Transactions
        /// </summary>
        private void ProcessTransactions()
        {
            var input = System.Console.ReadLine();
            do
            {
                do
                {
                    if (string.IsNullOrEmpty(input))
                    {
                        ShowPrimaryQuestions();
                        break;
                    }
                    else if(IsQuit(input) == false)
                    {
                        if (SaveTransactions())
                        {
                            System.Console.WriteLine("Transactions Saved successful.");

                            //Displaying the statement of the account
                            //To-Do

                            System.Console.WriteLine("Is there anything else you'd like to do?");
                            ShowPrimaryQuestions();
                            break;
                        }
                    }
                } while (true);
            } while (IsQuit(input));
        }

        private bool SaveTransactions()
        {
            return true;
        }

        /// <summary>
        /// Define Interest Rules
        /// </summary>
        private void DefineInterestRules()
        {
            var input = System.Console.ReadLine();
            do
            {
                do
                {
                    if (string.IsNullOrEmpty(input))
                    {
                        ShowPrimaryQuestions();
                        break;
                    }
                    else if (IsQuit(input) == false)
                    {
                        if (SaveTransactions())
                        {
                            System.Console.WriteLine("Interest rule defined successful.");

                            //Displaying the statement of the account
                            //To-Do

                            System.Console.WriteLine("Is there anything else you'd like to do?");
                            ShowPrimaryQuestions();
                            break;
                        }
                    }
                } while (true);
            } while (IsQuit(input));
        }

        /// <summary>
        /// Print Statement
        /// </summary>
        private void PrintStatement()
        {
            var input = System.Console.ReadLine();
            do
            {
                do
                {
                    if (string.IsNullOrEmpty(input))
                    {
                        ShowPrimaryQuestions();
                        break;
                    }
                    else if (IsQuit(input) == false)
                    {
                        if (SaveTransactions())
                        {
                            System.Console.WriteLine("Print Statement successful.");

                            //Displaying the statement of the account
                            //To-Do

                            System.Console.WriteLine("Is there anything else you'd like to do?");
                            ShowPrimaryQuestions();
                            break;
                        }
                    }
                } while (true);
            } while (IsQuit(input));
        }
    }
}
