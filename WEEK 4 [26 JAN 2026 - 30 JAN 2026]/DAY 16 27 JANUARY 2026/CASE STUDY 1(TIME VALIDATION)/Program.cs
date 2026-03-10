using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter time in format (hh:mm am/pm)");
        Console.WriteLine("Example: 09:59 pm");
        Console.Write("Input: ");

        string str = Console.ReadLine();

        int ans = UserProgramCode.validateTime(str);

        if (ans == 1)
            Console.WriteLine("Valid time format");
        else
            Console.WriteLine("Invalid time format");
    }
}
