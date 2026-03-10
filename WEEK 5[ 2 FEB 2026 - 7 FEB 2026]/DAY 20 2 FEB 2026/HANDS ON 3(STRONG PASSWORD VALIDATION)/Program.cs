using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter password:");
        string pwd = Console.ReadLine();

        Console.WriteLine(
            Logic.IsStrongPassword(pwd) ? "Strong" : "Weak"
        );
    }
}
