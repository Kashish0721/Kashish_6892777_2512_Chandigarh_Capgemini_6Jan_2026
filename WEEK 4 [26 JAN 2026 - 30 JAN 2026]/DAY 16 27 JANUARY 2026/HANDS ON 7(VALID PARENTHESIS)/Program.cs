using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter bracket string (e.g. {[()]}): ");
        string s = Console.ReadLine();

        Logic obj = new Logic();
        string result = obj.IsValidParentheses(s);

        Console.WriteLine(result);
    }
}
