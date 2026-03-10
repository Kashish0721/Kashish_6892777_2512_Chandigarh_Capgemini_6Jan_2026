using System;

class Multiply
{
    static void Main()
    {
        int[] arr = { 1, 2, 3, 4 };
        int product = 1;

        foreach (int x in arr)
        {
            if (x > 0)
                product *= x;
        }

        Console.WriteLine("Output: " + product);
    }
}