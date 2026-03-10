using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter Binary Number: ");
        string input1 = Console.ReadLine();

        Binary2Decimal obj = new Binary2Decimal();
        int output = obj.BinaryToDecimal(input1);

        Console.WriteLine("Output: " + output);
    }
}
