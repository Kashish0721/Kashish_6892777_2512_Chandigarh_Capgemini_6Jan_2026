using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter email:");
        string email = Console.ReadLine();

        Console.WriteLine(
            Logic.ValidateEmail(email) == 1 ? "Valid" : "Invalid"
        );
    }
}
