using System.Globalization;
using Spectre;
using Spectre.Console;

namespace coding_tracker
{
    internal static class Validation
    {

        internal static int CheckIntegerValidation(string x)
        {
            int.TryParse(x, out int val);
            return val;
        }
        internal static DateTime CheckDateValidation(string dateInput)
        {
            DateTime value;
            while (!DateTime.TryParseExact(dateInput, "dd-MM-yyyy HH:mm:ss", new CultureInfo("en-IN"), DateTimeStyles.None, out value))
            {
                AnsiConsole.MarkupLine("[bold red]Invalid date format[/], [bold]please make sure it's in[/] [bold yellow]dd-MM-yyyy HH:mm:ss.[/]");
                dateInput = AnsiConsole.Ask<string>("Date:");
            }

            return value;
        }

        internal static bool CheckTimeSpanValidation(DateTime startTime, DateTime endTime)
        {
            if (endTime < startTime)
            {
                AnsiConsole.MarkupLine("[bold red]Incorrect![/] [yellow bold]End Time cannot be less than the Start Time[/].");
                return false;
            }
            else
                return true;
        }

    }
}
