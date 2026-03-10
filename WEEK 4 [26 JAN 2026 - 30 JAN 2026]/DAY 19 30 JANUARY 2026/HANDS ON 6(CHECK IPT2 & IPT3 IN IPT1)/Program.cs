using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter main string:");
        string input1 = Console.ReadLine();

        Console.WriteLine("Enter input2:");
        string input2 = Console.ReadLine();

        Console.WriteLine("Enter input3:");
        string input3 = Console.ReadLine();

        int res = Logic.CheckOrder(input1, input2, input3);

        if (res == 1)
            Console.WriteLine("Present and input3 is after input2");
        else
            Console.WriteLine("Condition not satisfied");
    }
}
