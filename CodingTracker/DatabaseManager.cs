using Dapper;
using Microsoft.Data.Sqlite;


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
    }
}