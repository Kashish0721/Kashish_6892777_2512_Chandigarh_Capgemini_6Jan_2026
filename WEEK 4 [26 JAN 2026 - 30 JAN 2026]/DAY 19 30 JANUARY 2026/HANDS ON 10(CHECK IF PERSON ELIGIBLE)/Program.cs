using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter age:");
        int age = int.Parse(Console.ReadLine());

        int res = Logic.IsEligibleToVote(age);

        if (res == -1)
            Console.WriteLine("Invalid age");
        else if (res == 1)
            Console.WriteLine("Eligible to vote");
        else
            Console.WriteLine("Not eligible to vote");
    }
}
