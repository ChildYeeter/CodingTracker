using Dapper;
using Microsoft.Data.Sqlite;
using System.Globalization;


namespace coding_tracker
{
    internal class DatabaseManager
    {
        public DatabaseManager()
        {

        }

        internal void CreateTable(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                string sql = """
                    CREATE TABLE IF NOT EXISTS coding(
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        StartTime TEXT NOT NULL, 
                        EndTime TEXT NOT NULL,
                        Duration TEXT NOT NULL
                        );
                    """;

                connection.Execute(sql);
            }
        }

        internal void AddNewData(string connectionString)
        {
            DateTime startTime = GetDateInput("Please enter when you started your task: (dd-MM-yyyy HH:mm:ss)");
            DateTime endTime = GetDateInput("Please enter when you ended your task: (dd-MM-yyyy HH:mm:ss)");

            TimeSpan Duration = (startTime - endTime) < TimeSpan.Zero? -(startTime - endTime) : (startTime - endTime);

            using (var connection = new SqliteConnection(connectionString))
            {
                var sql = $"INSERT INTO coding (StartTime, EndTime, Duration) VALUES ('{startTime}', '{endTime}', '{Duration}')";

                connection.Execute(sql);
            }
        }

        internal DateTime GetDateInput(string message)
        {
            Console.WriteLine(message);
            string dateInput = Console.ReadLine();

            DateTime value;

            while(!DateTime.TryParseExact(dateInput, "dd-MM-yyyy HH:mm:ss", new CultureInfo("en-IN"), DateTimeStyles.None, out value))
            {
                Console.WriteLine("Invalid date format, please make sure it's in dd-MM-yyyy HH:mm:ss.");
                dateInput = Console.ReadLine();
            }

            return value;
        }
    }
}