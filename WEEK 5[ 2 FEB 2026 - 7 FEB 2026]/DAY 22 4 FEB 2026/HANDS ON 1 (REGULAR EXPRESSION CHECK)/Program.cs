using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter Greeting:");
        string input1 = Console.ReadLine();

        Console.WriteLine("Enter Name:");
        string input2 = Console.ReadLine();

        string pattern = @"^[A-Za-z]{2,}$";

        if (Regex.IsMatch(input2, pattern))
        {
            Console.WriteLine(input1 + " " + input2);
        }
        else
        {
            Console.WriteLine("Name should be more than 1 character");
        }
    }
}
