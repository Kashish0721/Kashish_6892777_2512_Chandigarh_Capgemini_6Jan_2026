using System;

class Program
{
    static void Main()
    {
        string s1 = Console.ReadLine();
        string s2 = Console.ReadLine();

        Logic obj = new Logic();
        string result = obj.ProcessString(s1, s2);

        Console.WriteLine(result);
    }
}
