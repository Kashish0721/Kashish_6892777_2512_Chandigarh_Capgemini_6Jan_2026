using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Dictionary<int, int> grades = new Dictionary<int, int>
        {
            {101, 78},
            {102, 45},
            {103, 88},
            {104, 40}
        };

        // Average
        Func<double> avg = () => grades.Values.Average();
        Console.WriteLine("Average Grade: " + avg());

        int threshold = 50;
        Predicate<int> atRisk = g => g < threshold;

        Console.WriteLine("Students at Risk:");
        foreach (var s in grades)
            if (atRisk(s.Value))
                Console.WriteLine(s.Key);

        // Update a grade
        grades[104] = 65;

        Console.WriteLine("\nAfter Update, At Risk:");
        foreach (var s in grades)
            if (atRisk(s.Value))
                Console.WriteLine(s.Key);
    }
}
