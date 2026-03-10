using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a string: ");
        string input = Console.ReadLine();

        Logic obj = new Logic();
        int output = obj.CountVowels(input);

        Console.WriteLine("Vowel Count: " + output);
    }
}
