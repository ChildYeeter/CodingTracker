using Dapper;
using Microsoft.Data.Sqlite;
using System.Globalization;


namespace coding_tracker
{
    internal class DatabaseManager
    {
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

        internal void ViewAllData(string connectionString)
        {
            Console.Clear();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                List<CodingSession> myList = connection.Query<CodingSession>("SELECT * FROM coding;").ToList();

                if (myList.Count == 0)
                {
                    Console.WriteLine("No data was found");
                }

                foreach (CodingSession session in myList)
                {
                    Console.WriteLine($"Session: {session.Id} | Duration: {session.Duration}");
                }
            }
        }

        internal void AddNewData(string connectionString)
        {
            Console.Clear();

            DateTime startTime = GetDateInput("Please enter when you started your task: (dd-MM-yyyy HH:mm:ss)");
            DateTime endTime = GetDateInput("Please enter when you ended your task: (dd-MM-yyyy HH:mm:ss)");

            TimeSpan Duration = endTime - startTime;

            if(endTime < startTime)
            {
                Console.WriteLine("End time cannot be before the start time");
            }

            using (var connection = new SqliteConnection(connectionString))
            {
                var sql = $"INSERT INTO coding (StartTime, EndTime, Duration) VALUES (@StartTime, @EndTime, @Duration)";

                connection.Execute(sql, new CodingSession()
                {
                    StartTime = startTime,
                    EndTime = endTime,
                    Duration = Duration.ToString()
                });
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

        internal void DeleteData(string connectionString)
        {
            Console.Clear();
            ViewAllData(connectionString);

            int toDelete = GetInteger("Please select the session you want to delete");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var sql = $"DELETE FROM coding WHERE ID = {toDelete}";
                int deletedRows = connection.Execute(sql);

                if (deletedRows == 0)
                {
                    Console.WriteLine("The row doesn't exist");
                }
                else
                    Console.WriteLine("The row has been deleted");
                connection.Close();
            }

        }

        internal void UpdateData(string connectionString)
        {

        }

        internal int GetInteger(string message)
        {
            Console.WriteLine(message);
            int.TryParse(Console.ReadLine(), out int value);

            return value;
        }
    }
}