using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter date (dd/MM/yyyy):");
        string date = Console.ReadLine();

        string result = Logic.AddYearFindDay(date);

        if (result == "-1")
            Console.WriteLine("Invalid date format");
        else
            Console.WriteLine("Day after adding 1 year: " + result);
    }
}
