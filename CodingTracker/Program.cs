using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace coding_tracker
{
    class Program
    {
        static string connectionString;
        static void Main(string[] args)
        {
            Console.Title = "Coding Tracker";
            AnsiConsole.MarkupLine("[bold blue]Welcome[/] to [green]Coding Tracker[/]!");

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            connectionString = config.GetConnectionString("DefaultConnection");

            DatabaseCreator dbManager = new DatabaseCreator();
            UserInput userInput = new UserInput();

            dbManager.CreateTable(connectionString);
            userInput.MainMenu(connectionString);
        }
    }
}