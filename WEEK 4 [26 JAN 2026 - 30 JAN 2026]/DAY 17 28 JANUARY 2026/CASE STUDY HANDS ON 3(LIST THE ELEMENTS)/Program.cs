using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.Write("Enter number of elements: ");
        int n = int.Parse(Console.ReadLine());

        List<int> list = new List<int>();
        Console.WriteLine("Enter elements:");
        for (int i = 0; i < n; i++)
            list.Add(int.Parse(Console.ReadLine()));

        Console.Write("Enter target value: ");
        int input2 = int.Parse(Console.ReadLine());

        List<int> result = UserProgramCode.GetElements(list, input2);

        if (result.Count == 1 && result[0] == -1)
        {
            Console.WriteLine("No element found");
        }
        else
        {
            Console.WriteLine("Output:");
            foreach (int x in result)
                Console.Write(x + " ");
        }
    }
}
