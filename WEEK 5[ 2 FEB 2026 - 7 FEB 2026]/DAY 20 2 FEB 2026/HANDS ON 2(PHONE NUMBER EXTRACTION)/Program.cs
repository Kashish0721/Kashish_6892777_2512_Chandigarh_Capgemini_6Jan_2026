using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter text:");
        string text = Console.ReadLine();

        Logic.ExtractPhoneNumbers(text);
    }
}
