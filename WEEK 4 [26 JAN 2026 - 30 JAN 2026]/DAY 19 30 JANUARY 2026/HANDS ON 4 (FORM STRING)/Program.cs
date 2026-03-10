using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter number of strings: ");
        int k = int.Parse(Console.ReadLine());

        string[] arr = new string[k];
        Console.WriteLine("Enter strings:");

        for (int i = 0; i < k; i++)
            arr[i] = Console.ReadLine();

        Console.Write("Enter n (character position): ");
        int n = int.Parse(Console.ReadLine());

        string result = UserProgramCode.formString(arr, n);
        Console.WriteLine("Output: " + result);
    }
}
