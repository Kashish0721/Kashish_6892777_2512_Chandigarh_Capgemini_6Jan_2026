using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter number of elements: ");
        int n = int.Parse(Console.ReadLine());

        string[] arr = new string[n];
        Console.WriteLine("Enter elements:");
        for (int i = 0; i < n; i++)
            arr[i] = Console.ReadLine();

        int result = Logic.IsNumericArray(arr);
        Console.WriteLine("Output: " + result);
    }
}
