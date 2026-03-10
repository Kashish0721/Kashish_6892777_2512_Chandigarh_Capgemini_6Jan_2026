using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter number of lines: ");
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            string line = Console.ReadLine();

            Console.WriteLine("the: " + line.IndexOf("the"));
            Console.WriteLine("of: " + line.IndexOf("of"));
        }
    }
}
