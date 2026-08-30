namespace coding_tracker
{
    internal class UserInput
    {
        internal void MainMenu(string connectionString)
        {
            bool isClosed = false;

            DataController dataController = new();


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
                        dataController.AddNewData(connectionString);
                        break;
                    /*case "2":
                        ViewAllData();
                        break;
                    case "3":
                        UpdateData();
                        break;
                    case "4":
                        DeleteData();*/
                        break;
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