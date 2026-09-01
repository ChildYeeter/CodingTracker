using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace coding_tracker
{
    internal static class Validation
    {

        internal static int CheckIntegerValitaion(string x)
        {
            int.TryParse(x, out int val);
            return val;
        }
        internal static DateTime CheckDateValidation(string dateInput)
        {
            DateTime value;
            while (!DateTime.TryParseExact(dateInput, "dd-MM-yyyy HH:mm:ss", new CultureInfo("en-IN"), DateTimeStyles.None, out value))
            {
                Console.WriteLine("Invalid date format, please make sure it's in dd-MM-yyyy HH:mm:ss.");
                dateInput = Console.ReadLine();
            }

            return value;
        }

        internal static bool CheckTimeSpanValidation(DateTime startTime, DateTime endTime)
        {
            if (endTime < startTime)
            {
                Console.WriteLine("Incorrect! End Time cannot be less than the Start Time.");
                return false;
            }
            else
                return true;
        }

    }
}
