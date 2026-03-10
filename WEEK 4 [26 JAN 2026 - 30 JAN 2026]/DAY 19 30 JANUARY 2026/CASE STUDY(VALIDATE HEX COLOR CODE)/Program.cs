using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter hex color code: ");
        string color = Console.ReadLine();

        int result = Logic.ValidateHex(color);
        Console.WriteLine("Output: " + result);
    }
}
