using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter number of records: ");
        int n = int.Parse(Console.ReadLine());

        string[] arr = new string[n];
        Console.WriteLine("Enter records (e.g. 123111241):");

        for (int i = 0; i < n; i++)
            arr[i] = Console.ReadLine();

        Console.Write("Enter Location Code: ");
        int loc = int.Parse(Console.ReadLine());

        int result = UserProgramCode.getDonation(arr, loc);
        Console.WriteLine("Output: " + result);
    }
}
