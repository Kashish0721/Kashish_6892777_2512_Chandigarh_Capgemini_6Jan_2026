using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter number of strings: ");
        int n = int.Parse(Console.ReadLine());

        string[] arr = new string[n];
        Console.WriteLine("Enter strings:");

        for (int i = 0; i < n; i++)
            arr[i] = Console.ReadLine();

        int result = UserProgramCode.sumOfDigits(arr);
        Console.WriteLine("Output: " + result);
    }
}
