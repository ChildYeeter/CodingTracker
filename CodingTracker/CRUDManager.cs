using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace coding_tracker
{
    static internal class CRUDManager
    {
        static internal void ViewAllData(string connectionString)
        {
            AnsiConsole.Clear();

            using (var connection = new SqliteConnection(connectionString))
            {
                List<CodingSession> myList = connection.Query<CodingSession>("SELECT * FROM coding;").ToList();

                if (myList.Count == 0)
                {
                    AnsiConsole.MarkupLine("[bold DarkOrange]No data was found[/]");
                }

                else
                {
                    var table = new Table();
                    table.HeavyBorder();
                    table.BorderColor(Color.Magenta3);
                    table.AddColumn("[bold blue]ID[/]");
                    table.AddColumn("[bold blue]Duration[/]");

                    foreach (CodingSession session in myList)
                    {
                        /*AnsiConsole.MarkupLine($"[bold]Session:[/] {session.Id} | [bold]Duration:[/] {session.Duration}");*/
                        table.AddRow($"{session.Id}", $"{session.Duration}");
                    }
                    AnsiConsole.Write(table);
                }
            }
        }

        static internal void AddNewData(string connectionString)
        {
            AnsiConsole.Clear();

            DateTime startTime;
            DateTime endTime;

            startTime = GetDateInput("[bold]Please enter when you started your task: (dd-MM-yyyy HH:mm:ss)[/]");
            endTime = GetDateInput("[bold]Please enter when you ended your task: (dd-MM-yyyy HH:mm:ss)[/]");


            while (!Validation.CheckTimeSpanValidation(startTime, endTime))
            {
                startTime = GetDateInput("[bold]Please enter the correct startTime: (dd-MM-yyyy HH:mm:ss)[/]");
                endTime = GetDateInput("[bold]Please enter the correct endTime: (dd-MM-yyyy HH:mm:ss)[/]");
            }

            TimeSpan Duration = endTime - startTime;



            using (var connection = new SqliteConnection(connectionString))
            {
                var sql = "INSERT INTO coding (StartTime, EndTime, Duration) VALUES (@StartTime, @EndTime, @Duration)";

                connection.Execute(sql, new CodingSession()
                {
                    StartTime = startTime,
                    EndTime = endTime,
                    Duration = Duration.ToString()
                });
            }
        }

        static internal DateTime GetDateInput(string message)
        {
            AnsiConsole.MarkupLine(message);
            string dateInput = Console.ReadLine();

            return Validation.CheckDateValidation(dateInput);
        }

        static internal void DeleteData(string connectionString)
        {
            AnsiConsole.Clear();
            ViewAllData(connectionString);

            int toDelete = GetInteger("[bold]Please select the session you want to delete[/]");

            using (var connection = new SqliteConnection(connectionString))
            {
                var sql = "DELETE FROM coding WHERE ID = @Id";

                int deletedRows = connection.Execute(sql, new {Id = toDelete});

                if (deletedRows == 0)
                {
                    AnsiConsole.MarkupLine("[bold DarkOrange]The row doesn't exist[/]");
                }
                else
                    AnsiConsole.MarkupLine("[DarkOrange bold]The row has been deleted[/]");
            }

        }

        static internal void UpdateData(string connectionString)
        {
            Console.Clear();
            ViewAllData(connectionString);
            int toUpdate = GetInteger("[bold]Please enter the session you want to update: [/]");


            using (var connection = new SqliteConnection(connectionString))
            {


                var checkQuery = "SELECT EXISTS(SELECT 1 FROM coding WHERE ID = @Id)";

                int exists = connection.ExecuteScalar<int>(checkQuery, new {Id = toUpdate});

                if (exists == 0)
                    AnsiConsole.MarkupLine($"[bold yellow]The session no. {toUpdate} doesn't exist.[/]");

                else
                {

                    DateTime newStartTime = GetDateInput("[bold]Please enter the updated start time:(dd-MM-yyyy HH:mm:ss)[/]");
                    DateTime newEndTime = GetDateInput("[bold]Please enter the updated end time:(dd-MM-yyyy HH:mm:ss)[/]");


                    while (newEndTime < newStartTime)
                    {
                        AnsiConsole.MarkupLine("[bold yellow]End time cannot be before the start time[/]");
                        newStartTime = GetDateInput("[bold]Please enter the updated start time:(dd-MM-yyyy HH:mm:ss)[/]");
                        newEndTime = GetDateInput("[bold]Please enter the updated end time:(dd-MM-yyyy HH:mm:ss)[/]");
                    }

                    TimeSpan newDuration = newEndTime - newStartTime;

                    var sql = @"UPDATE coding
                            SET StartTime = @StartTime,
                            EndTime = @EndTime,
                            Duration = @Duration
                            WHERE ID = @Id";

                    connection.Execute(sql, new CodingSession
                    {
                        StartTime = newStartTime,
                        EndTime = newEndTime,
                        Duration = newDuration.ToString(),
                        Id = toUpdate
                    });
                    AnsiConsole.MarkupLine($"[green bold]Your data for session no.{toUpdate} has been updated[/]");
                }
            }
        }

        static internal int GetInteger(string message)
        {
            AnsiConsole.MarkupLine(message);
            int val = Validation.CheckIntegerValidation(Console.ReadLine());

            return val;
        }
    }
}