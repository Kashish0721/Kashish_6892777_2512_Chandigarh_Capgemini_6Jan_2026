using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter limit: ");
        int input1 = int.Parse(Console.ReadLine());

        Average obj = new Average();
        int output = obj.AvgDivBy5(input1);

        Console.WriteLine("Output: " + output);
    }
}
