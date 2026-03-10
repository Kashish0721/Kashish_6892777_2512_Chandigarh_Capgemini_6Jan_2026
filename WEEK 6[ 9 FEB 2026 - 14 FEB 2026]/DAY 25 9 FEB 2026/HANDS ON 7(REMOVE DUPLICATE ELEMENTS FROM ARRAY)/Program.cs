using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.Write("Enter numbers separated by space: ");
        string[] input = Console.ReadLine().Split();

        HashSet<string> unique = new HashSet<string>(input);

        Console.WriteLine("Array without duplicates:");
        foreach (var item in unique)
            Console.Write(item + " ");
    }
}
