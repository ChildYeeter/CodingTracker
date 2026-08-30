using Microsoft.Extensions.Configuration;

namespace coding_tracker
{
    class Program
    {
        static string connectionString;
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            connectionString = config.GetConnectionString("DefaultConnection");
            DatabaseManager dbManager = new DatabaseManager();
        }
    }
}