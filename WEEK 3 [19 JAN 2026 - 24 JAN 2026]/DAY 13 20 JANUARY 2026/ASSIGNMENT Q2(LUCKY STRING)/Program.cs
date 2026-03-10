using System;

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        string s = Console.ReadLine();

        Logic obj = new Logic();
        string result = obj.IsLuckyString(n, s);

        Console.WriteLine(result);
    }
}
