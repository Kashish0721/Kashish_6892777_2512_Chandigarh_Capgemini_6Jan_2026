using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a string: ");
        string input = Console.ReadLine();

        Logic obj = new Logic();
        char output = obj.FirstNonRepeating(input);

        if (output == '\0')
            Console.WriteLine("No non-repeating character found");
        else
            Console.WriteLine("First Non-Repeating Character: " + output);
    }
}
