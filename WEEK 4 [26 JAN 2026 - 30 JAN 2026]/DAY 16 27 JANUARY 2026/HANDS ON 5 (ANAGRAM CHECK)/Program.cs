using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter first string: ");
        string s1 = Console.ReadLine();

        Console.Write("Enter second string: ");
        string s2 = Console.ReadLine();

        Logic obj = new Logic();
        string output = obj.IsAnagram(s1, s2);

        Console.WriteLine("Result: " + output);
    }
}
