namespace coding_tracker
{
    internal class UserInput
    {
        internal void MainMenu(string connectionString)
        {
            bool isClosed = false;

            DatabaseManager dbController = new();


            while (!isClosed)
            {
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1: Add");
                Console.WriteLine("2: View");
                Console.WriteLine("3: Update");
                Console.WriteLine("4: Delete");
                Console.WriteLine("0: Quit");
                Console.WriteLine("---------------------------------------------------------");

                switch (Console.ReadLine())
                {
                    case "1":
                        dbController.AddNewData(connectionString);
                        break;
                        case "2":
                        dbController.ViewAllData(connectionString);
                            break;
                        /*case "3":
                        dbController.UpdateData();
                            break;
                        case "4":
                        dbController.DeleteData();
                        break;*/
                    case "0":
                        isClosed = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Input. Please Try again");
                        break;
                }
            }
        }
    }
}