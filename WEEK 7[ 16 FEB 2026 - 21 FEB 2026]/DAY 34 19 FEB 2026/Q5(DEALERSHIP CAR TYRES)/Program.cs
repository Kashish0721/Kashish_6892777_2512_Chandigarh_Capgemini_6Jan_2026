using System;

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            string[] input = Console.ReadLine().Split();

            int cars = int.Parse(input[0]);
            int bikes = int.Parse(input[1]);

            int tyres = cars * 4 + bikes * 2;

            Console.WriteLine(tyres);
        }
    }
}
