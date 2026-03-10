using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter size of array: ");
        int n = int.Parse(Console.ReadLine());

        string[] arr = new string[n];
        Console.WriteLine("Enter strings:");

        for (int i = 0; i < n; i++)
            arr[i] = Console.ReadLine();

        Console.Write("Enter a character: ");
        char ch = Console.ReadLine()[0];

        int result = UserProgramCode.GetCount(n, arr, ch);

        if (result == -1)
            Console.WriteLine("No elements Found");
        else if (result == -2)
            Console.WriteLine("Only alphabets should be given");
        else
            Console.WriteLine(result);
    }
}
