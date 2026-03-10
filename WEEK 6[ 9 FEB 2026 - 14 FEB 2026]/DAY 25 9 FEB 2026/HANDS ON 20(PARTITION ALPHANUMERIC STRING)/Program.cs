using System;
using System.Text;

class Program
{
    static void Main()
    {
        string str = Console.ReadLine();

        StringBuilder left = new StringBuilder();
        StringBuilder right = new StringBuilder();

        foreach (char c in str)
        {
            if (char.IsDigit(c))
                left.Append(c);
            else
                right.Append(c);
        }

        Console.WriteLine("Left: " + left);
        Console.WriteLine("Right: " + right);
    }
}
