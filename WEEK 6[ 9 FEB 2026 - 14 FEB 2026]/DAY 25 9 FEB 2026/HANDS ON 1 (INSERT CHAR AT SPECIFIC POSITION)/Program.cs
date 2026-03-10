using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a string: ");
        string str = Console.ReadLine();

        Console.WriteLine("Enter Character to insert: ");
        char ch = Convert.ToChar(Console.ReadLine());

        Console.WriteLine("Enter Position ");
        int pos = Convert.ToInt32(Console.ReadLine());

        string result = str.Insert(pos, ch.ToString());
        Console.WriteLine("updated string:" + result);
    }
}