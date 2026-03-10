using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter password: ");
        string pwd = Console.ReadLine();

        int result = Logic.ValidatePassword(pwd);
        Console.WriteLine("Output: " + result);
    }
}
