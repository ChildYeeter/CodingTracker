using Dapper;
using Microsoft.Data.Sqlite;

namespace coding_tracker
{
    static internal class CRUDManager
    {
        static internal void ViewAllData(string connectionString)
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

        static internal void AddNewData(string connectionString)
        {
            Console.Clear();

            DateTime startTime;
            DateTime endTime;

            startTime = GetDateInput("Please enter when you started your task: (dd-MM-yyyy HH:mm:ss)");
            endTime = GetDateInput("Please enter when you ended your task: (dd-MM-yyyy HH:mm:ss)");


            while (!Validation.CheckTimeSpanValidation(startTime, endTime))
            {
                startTime = GetDateInput("Please enter the correct startTime: (dd-MM-yyyy HH:mm:ss)");
                endTime = GetDateInput("Please enter the correct endTime: (dd-MM-yyyy HH:mm:ss)");
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
            Console.WriteLine(message);
            string dateInput = Console.ReadLine();

            return Validation.CheckDateValidation(dateInput);
        }

        static internal void DeleteData(string connectionString)
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

        static internal void UpdateData(string connectionString)
        {
            Console.Clear();
            ViewAllData(connectionString);

            int toUpdate = GetInteger("Please enter the session you want to update: ");

            DateTime newStartTime = GetDateInput("Please enter the updated start time:(dd-MM-yyyy HH:mm:ss)");
            DateTime newEndTime = GetDateInput("Please enter the updated end time:(dd-MM-yyyy HH:mm:ss)");


            if (newEndTime < newStartTime)
            {
                Console.WriteLine("End time cannot be before the start time");
                UpdateData(connectionString);
            }

            TimeSpan newDuration = newEndTime - newStartTime;

            using (var connection = new SqliteConnection(connectionString))
            {
                var sql = @$"UPDATE coding
                            SET StartTime = @StartTime,
                            EndTime = @EndTime,
                            Duration = @Duration
                            WHERE ID = {toUpdate}";

                connection.Execute(sql, new CodingSession
                {
                    StartTime = newStartTime,
                    EndTime = newEndTime,
                    Duration = newDuration.ToString()
                });
            }
        }

        static internal int GetInteger(string message)
        {
            Console.WriteLine(message);
            int val = Validation.CheckIntegerValitaion(Console.ReadLine());

            return val;
        }
    }
}