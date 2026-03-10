using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter date (e.g. 12/05/2022):");
        string date = Console.ReadLine();

        Console.WriteLine("Enter number of years to add:");
        int years = int.Parse(Console.ReadLine());

        string result = Logic.AddYearsToDate(date, years);

        if (result == "-1")
            Console.WriteLine("Invalid date format");
        else if (result == "-2")
            Console.WriteLine("Years cannot be negative");
        else
            Console.WriteLine("Resulting Date: " + result);
    }
}
