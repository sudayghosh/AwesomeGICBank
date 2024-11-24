using AwesomeGIC.Bank.UI.Dto;
using AwesomeGIC.Bank.UI.Service;
using System;
using System.ComponentModel.DataAnnotations;

namespace Bank.Console
{
    public class UserInputManager
    {
        private const string ANYTHING_ELSE_TEXT = "Is there anything else you'd like to do?";
        private const string TRAY_AGAIN_TEXT = "Please try again...";

        private readonly IAccountService _accountService;
        private readonly IInterestRuleService _interestRuleService;

        public UserInputManager(IAccountService accountService, IInterestRuleService interestRuleService)
        {
            _accountService = accountService;
            _interestRuleService = interestRuleService;
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

        public async Task<bool> DoProcess(string? inputType)
        {
            switch (inputType.ToUpper())
            {
                case "T":
                    System.Console.WriteLine("Please enter transaction details in <Date> <Account> <Type> <Amount> format \n(or enter blank to go back to main menu):");
                    await ProcessTransactions();
                    break;
                case "I":
                    System.Console.WriteLine("Please enter interest rules details in <Date> <RuleId> <Rate in %> format \n(or enter blank to go back to main menu):");
                    await ProcessInterestRules();
                    break;
                case "P":
                    System.Console.WriteLine("Please enter account and month to generate the statement <Account> <Year><Month> \n(or enter blank to go back to main menu):");
                    ProcessStatement();
                    break;
                default:
                    System.Console.BackgroundColor = ConsoleColor.DarkYellow;
                    System.Console.WriteLine("Please enter vaid input... Try again...");
                    System.Console.BackgroundColor = ConsoleColor.Black;
                    break;
            }
            return true;
        }

        /// <summary>
        /// Process Transactions
        /// </summary>
        private async Task ProcessTransactions()
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
                            System.Console.WriteLine(ANYTHING_ELSE_TEXT);
                            ShowPrimaryQuestions();
                            break;
                        }
                        else
                        {
                            System.Console.WriteLine(TRAY_AGAIN_TEXT);
                            input = System.Console.ReadLine();
                        }
                    }
                } while (true);
            } while (IsQuit(input));
        }

        private async Task<bool> SaveTransactions(string input)
        {
            try
            {
                string[] splitInp = input.Split(" ");
                var dto = new AccountReqDto
                {
                    TxnDate = splitInp.Length > 0 ? splitInp[0] : "",
                    AccountNo = splitInp.Length > 1 ? splitInp[1] : "",
                    Type = splitInp.Length > 2 ? splitInp[2].ToUpper() : "",
                    Amount = splitInp.Length > 3 ? Convert.ToDecimal(splitInp[3]) : 0,
                };

                if (ValidateDto(dto) == false) { return false; }

                var result = await _accountService.UpsertAccount(dto);
                if (result?.ErrorResponse != null)
                {
                    System.Console.BackgroundColor = ConsoleColor.Red;
                    System.Console.WriteLine(result?.ErrorResponse.Message);
                    System.Console.BackgroundColor = ConsoleColor.Black;
                    return false;
                }

                System.Console.WriteLine("Transactions saved successful.\n==============================");
                System.Console.WriteLine($"Account: {result.AccountNo}");
                System.Console.WriteLine($"| Date\t\t| Txn Id\t| Type\t| Amount|");
                foreach (var trans in result.Transactions)
                {
                    System.Console.WriteLine($"|{trans.TxnDate.ToString("yyyyMMdd")}\t| {trans.TxnId}\t| {trans.Type}\t| {trans.Amount.ToString("0.00")}|");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Upsert Interest Rules
        /// </summary>
        private async Task ProcessInterestRules()
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
                        if (await SaveInterestRule(input))
                        {
                            System.Console.WriteLine(ANYTHING_ELSE_TEXT);
                            ShowPrimaryQuestions();
                            break;
                        }
                        else
                        {
                            System.Console.WriteLine(TRAY_AGAIN_TEXT);
                            input = System.Console.ReadLine();
                        }
                    }
                } while (true);
            } while (IsQuit(input));
        }

        private async Task<bool> SaveInterestRule(string input)
        {
            try
            {
                string[] splitInp = input.Split(" ");
                var dto = new InterestRuleReqDto
                {
                    DateTime = splitInp.Length > 0 ? splitInp[0] : "",
                    RuleId = splitInp.Length > 1 ? splitInp[1] : "",
                    Rate = splitInp.Length > 2 ? Convert.ToDecimal(splitInp[2]) : 0,
                };

                if (ValidateDto(dto) == false) { return false; }

                var rule = await _interestRuleService.UpsertInterestRule(dto);
                if (rule?.ErrorResponse != null)
                {
                    System.Console.BackgroundColor = ConsoleColor.Red;
                    System.Console.WriteLine(rule?.ErrorResponse.Message);
                    System.Console.BackgroundColor = ConsoleColor.Black;
                    return false;
                }

                System.Console.WriteLine("Interest Rule saved successful.\n==============================");
                System.Console.WriteLine($"Interest rules:");
                System.Console.WriteLine($"| Date\t\t| RuleId\t| Rate (%)|");
                foreach (var ir in rule.Rules)
                {
                    System.Console.WriteLine($"|{ir.DateTime.ToString("yyyyMMdd")}\t| {ir.RuleId}\t| {ir.Rate.ToString("0.00")}|");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// Print Statement
        /// </summary>
        private async void ProcessStatement()
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
                        if (await PrintStatement(input))
                        {
                            //System.Console.WriteLine("Print Statement successful.");

                            System.Console.WriteLine(ANYTHING_ELSE_TEXT);
                            ShowPrimaryQuestions();
                            break;
                        }
                        else
                        {
                            System.Console.WriteLine(TRAY_AGAIN_TEXT);
                            input = System.Console.ReadLine();
                        }
                    }
                } while (true);
            } while (IsQuit(input));
        }

        private async Task<bool> PrintStatement(string input)
        {
            try
            {
                string[] splitInp = input.Split(" ");
                var dto = new AccountStatementReqDto
                {
                    AccountNo = splitInp.Length > 0 ? splitInp[0] : "",
                    TxnDate = splitInp.Length > 1 ? splitInp[1] : "",
                };

                if (ValidateDto(dto) == false) { return false; }

                var result = await _accountService.GetAccountStatement(dto);
                if (result?.ErrorResponse != null)
                {
                    System.Console.BackgroundColor = ConsoleColor.Red;
                    System.Console.WriteLine(result?.ErrorResponse.Message);
                    System.Console.BackgroundColor = ConsoleColor.Black;
                    return false;
                }

                if (string.IsNullOrEmpty(result.AccountNo))
                {
                    System.Console.WriteLine($"No Record Found!!!");
                    return true;
                }

                System.Console.WriteLine($"Account: {result.AccountNo}");
                System.Console.WriteLine($"| Date\t\t| Txn Id\t| Type\t| Amount\t| Balance|");
                if (result.Transactions != null)
                {
                    foreach (var trans in result.Transactions)
                    {
                        System.Console.WriteLine($"|{trans.TxnDate.ToString("yyyyMMdd")}\t| {trans.TxnId}\t| {trans.Type}\t| {trans.Amount.ToString("0.00")}\t| {trans.Balance.ToString("0.00")}|");
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
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
