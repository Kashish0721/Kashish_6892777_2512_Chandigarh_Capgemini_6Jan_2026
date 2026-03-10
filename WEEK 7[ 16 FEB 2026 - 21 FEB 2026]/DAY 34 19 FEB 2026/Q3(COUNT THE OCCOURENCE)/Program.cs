using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        int[] arr = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

        Dictionary<int, int> freq = new Dictionary<int, int>();

        foreach (int x in arr)
        {
            if (freq.ContainsKey(x))
                freq[x]++;
            else
                freq[x] = 1;
        }

        foreach (var kv in freq)
        {
            Console.WriteLine($"{kv.Key} occurs {kv.Value} times");
        }
    }
}
