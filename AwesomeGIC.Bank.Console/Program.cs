// See https://aka.ms/new-console-template for more information

using Bank.Console;

System.Console.WriteLine("Welcome to AwesomeGIC Bank! What would you like to do?");

UserInputManager userInputManager = new();
userInputManager.ShowPrimaryQuestions();

string? inp = Console.ReadLine();
if (userInputManager.IsQuit(inp)) return;
var isContinue = true;
do
{
    userInputManager.DoProcess(inp);
    if (isContinue)
    {
        inp = Console.ReadLine();
        if (userInputManager.IsQuit(inp)) isContinue = false;
    }
} while(isContinue);
