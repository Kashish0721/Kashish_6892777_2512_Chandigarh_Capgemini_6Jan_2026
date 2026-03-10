using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int N = int.Parse(Console.ReadLine());

        Console.WriteLine(MinOperations(N));
    }

    static int MinOperations(int N)
    {
        int start = 10;

        if (start == N) return 0;

        Queue<(int value, int steps)> q = new Queue<(int, int)>();
        HashSet<int> visited = new HashSet<int>();

        q.Enqueue((start, 0));
        visited.Add(start);

        int limit = Math.Max(N * 3, 100);

        while (q.Count > 0)
        {
            var (current, steps) = q.Dequeue();

            int[] next = {
                current + 2,
                current - 1,
                current * 3
            };

            foreach (int nx in next)
            {
                if (nx == N)
                    return steps + 1;

                if (nx >= 0 && nx <= limit && !visited.Contains(nx))
                {
                    visited.Add(nx);
                    q.Enqueue((nx, steps + 1));
                }
            }
        }                                                                                    lll         v  
        return -1; 
    }
}
