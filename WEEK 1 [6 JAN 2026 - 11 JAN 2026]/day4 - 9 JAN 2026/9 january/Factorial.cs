using System;

class Factorial
{
    public static int FindFactorial(int number)
    {
        int output1 = 1;

        if (number < 0)
        {
            output1 = -1;
        }
        else if (number > 7)
        {
            output1 = -2;
        }
        else
        {
            for (int i = 1; i <= number; i++)
            {
                output1 = output1 * i;
            }
        }

        return output1;
    }
}

class Program
{
    static void Main()
    {
        int number, result;

        Console.Write("Enter a number: ");
        number = int.Parse(Console.ReadLine());

        result = Factorial.FindFactorial(number);

        Console.WriteLine("Output: " + result);
    }
}