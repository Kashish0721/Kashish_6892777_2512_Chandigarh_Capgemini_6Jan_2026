using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a string: ");
        string input = Console.ReadLine();

        Logic obj = new Logic();
        string output = obj.ReverseString(input);

        Console.WriteLine("Reversed String: " + output);
    }
}
