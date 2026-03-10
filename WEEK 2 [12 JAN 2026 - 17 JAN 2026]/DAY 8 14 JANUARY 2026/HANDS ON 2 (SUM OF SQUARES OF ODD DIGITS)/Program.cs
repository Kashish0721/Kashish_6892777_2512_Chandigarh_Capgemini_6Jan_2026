using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a number: ");
        int input1 = int.Parse(Console.ReadLine());

        SumofSquare obj = new SumofSquare();
        int output = obj.SumOfSquaresOddDigits(input1);

        Console.WriteLine("Output: " + output);
    }
}
