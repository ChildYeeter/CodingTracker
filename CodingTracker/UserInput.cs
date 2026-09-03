using Spectre.Console;

namespace coding_tracker
{
    internal class UserInput
    {
        internal void MainMenu(string connectionString)
        {
            bool isClosed = false;

            while (!isClosed)
            {

                /*Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1: Add");
                Console.WriteLine("2: View");
                Console.WriteLine("3: Update");
                Console.WriteLine("4: Delete");
                Console.WriteLine("0: Quit");
                Console.WriteLine("---------------------------------------------------------");*/

                var myTable = new Table()
                    .AddColumn("What would you like to do?")
                    .AddRow("[green]1: Add[/]")
                    .AddRow("[green]2: View[/]")
                    .AddRow("[green]3: Update[/]")
                    .AddRow("[green]4: Delete[/]")
                    .AddRow("[green]0: Quit[/]")
                    .BorderColor(Color.BlueViolet);

                AnsiConsole.Write(myTable);

                switch (Console.ReadLine())
                {
                    case "1":
                        CRUDManager.AddNewData(connectionString);
                        break;
                    case "2":
                        CRUDManager.ViewAllData(connectionString);
                        break;
                    case "3":
                        CRUDManager.UpdateData(connectionString);
                        break;
                    case "4":
                        CRUDManager.DeleteData(connectionString);
                        break;
                    case "0":
                        AnsiConsole.MarkupLine("[bold blue]Goodbye![/]\n[bold slowblink](Press any key to quit)[/]");
                        Console.ReadKey(true);
                        isClosed = true;
                        break;
                    default:
                        AnsiConsole.MarkupLine("[red]Invalid Input. Please Try again[/]");
                        break;
                }
            }
        }
    }
}