using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter Salary: ");
        int input1 = int.Parse(Console.ReadLine());

        Console.Write("Enter Working Days: ");
        int input2 = int.Parse(Console.ReadLine());

        Logic obj = new Logic();
        int output = obj.CalculateSavings(input1, input2);

        Console.WriteLine("Output: " + output);
    }
}
