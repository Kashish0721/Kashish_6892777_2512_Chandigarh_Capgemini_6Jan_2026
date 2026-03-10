using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter Year: ");
        int input1 = int.Parse(Console.ReadLine());

        Logic obj = new Logic();
        int output = obj.CheckLeapYear(input1);

        Console.WriteLine("Output: " + output);
    }
}
