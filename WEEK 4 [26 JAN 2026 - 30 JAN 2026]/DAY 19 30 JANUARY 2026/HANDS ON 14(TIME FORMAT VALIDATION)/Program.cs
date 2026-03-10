using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter time (hh:mm am/pm):");
        string time = Console.ReadLine();

        int res = Logic.ValidateTime(time);

        if (res == 1)
            Console.WriteLine("Valid time format");
        else
            Console.WriteLine("Invalid time format");
    }
}
