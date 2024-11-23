// See https://aka.ms/new-console-template for more information

using Bank.Console;
using System;
using Microsoft.Extensions.DependencyInjection;
using AwesomeGIC.Bank.UI.Service;
using RestSharp;
using Microsoft.Extensions.Configuration;

class Program
{
    static async Task Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var userInputManager = serviceProvider.GetRequiredService<UserInputManager>();

        System.Console.WriteLine("Welcome to AwesomeGIC Bank! What would you like to do?");
        userInputManager.ShowPrimaryQuestions();

        string? inp = Console.ReadLine();
        if (userInputManager.IsQuit(inp)) return;
        var isContinue = true;
        do
        {
            await userInputManager.DoProcess(inp);
            if (isContinue)
            {
                inp = Console.ReadLine();
                if (userInputManager.IsQuit(inp)) isContinue = false;
            }
        } while (isContinue);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IConfiguration>(sp =>
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            return configurationBuilder.Build();
        });

        // Register services and classes
        services.AddSingleton<IRestClient, RestClient>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IInterestRuleService, InterestRuleService>();
        services.AddTransient<UserInputManager>();


    }
}