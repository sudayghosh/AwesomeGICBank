using AwesomeGIC.Bank.UI.Dto;
using AwesomeGIC.Bank.UI.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Console
{
    public class UserInputManager
    {
        private readonly IAccountService _accountService;

        public UserInputManager(IAccountService accountService)
        {
            _accountService = accountService;
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
            switch (inputType.ToUpper())
            {
                case "T":
                    System.Console.WriteLine("Please enter transaction details in <Date> <Account> <Type> <Amount> format \n(or enter blank to go back to main menu):");
                    ProcessTransactions();
                    break;
                case "I":
                    System.Console.WriteLine("Please enter interest rules details in <Date> <RuleId> <Rate in %> format \n(or enter blank to go back to main menu):");
                    DefineInterestRules();
                    break;
                case "P":
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
        private async void ProcessTransactions()
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
                        if (await SaveTransactions(input))
                        {
                            System.Console.WriteLine("Is there anything else you'd like to do?");
                            ShowPrimaryQuestions();
                            break;
                        }
                        else
                        {
                            System.Console.WriteLine("Please try again...");
                            input = System.Console.ReadLine();
                        }
                    }
                } while (true);
            } while (IsQuit(input));
        }

        private async Task<bool> SaveTransactions(string input)
        {
            string[] splitInp = input.Split(" ");
            var dto = new AccountReqDto
            {
                TxnDate = splitInp.Length > 0 ? splitInp[0] : "",
                AccountNo = splitInp.Length > 1 ? splitInp[1] : "",
                Type = splitInp.Length > 2 ? splitInp[2] : "",
                Amount = splitInp.Length > 3 ? Convert.ToDecimal(splitInp[3]) : 0,
            };

            if(ValidateDto(dto) == false) { return false; }

            var result = await _accountService.UpsertAccount(dto);
            if(result == null) { return false; }

            System.Console.WriteLine("Transactions saved successful.\n==============================");
            System.Console.WriteLine($"Account: {result.AccountNo}");
            System.Console.WriteLine($"| Date\t\t| Txn Id\t| Type\t| Amount|");
            foreach (var trans in result.Transactions)
            {
                System.Console.WriteLine($"|{trans.TxnDate.ToString("yyyyMMdd")}\t| {trans.TxnId}\t| {trans.Type}\t| {trans.Amount.ToString("0.00")}|");
            }

            return result != null;
        }

        /// <summary>
        /// Define Interest Rules
        /// </summary>
        private async void DefineInterestRules()
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
                        if (await SaveTransactions(input))
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
        private async void PrintStatement()
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
                        if (await SaveTransactions(input))
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

        private bool ValidateDto(object dto)
        {
            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(dto, context, results, true))
            {
                System.Console.BackgroundColor = ConsoleColor.Red;
                foreach (var validationResult in results)
                {
                    System.Console.WriteLine(validationResult.ErrorMessage);
                }
                System.Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                return true;
            }
            return false;
        }
    }
}
