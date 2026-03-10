using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.Write("Enter employee skills: ");
        int[] skills = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

        Console.Write("Enter team sizes: ");
        int[] teams = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

        Array.Sort(skills);
        Array.Sort(teams);
        Array.Reverse(teams); // bigger teams first

        int left = 0;
        int right = skills.Length - 1;

        int totalStrength = 0;

        foreach (int size in teams)
        {
            int max = skills[right--];
            int min = skills[left];

            totalStrength += max + min;

            left += size - 1; // fill remaining
        }

        Console.WriteLine("Maximum Team Strength: " + totalStrength);
    }
}
