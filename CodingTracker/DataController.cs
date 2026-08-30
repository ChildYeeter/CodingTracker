using Dapper;
using Microsoft.Data.Sqlite;

namespace coding_tracker
{
    internal class DataController
    {
        internal void AddNewData(string connectionString)
        {
            using(var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
            }
        }
    }
}