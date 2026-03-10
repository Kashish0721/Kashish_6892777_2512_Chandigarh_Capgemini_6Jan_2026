using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter number of elements: ");
        int n = int.Parse(Console.ReadLine());

        string[] arr = new string[n];
        Console.WriteLine("Enter elements (Employee, Designation pairs):");
        for (int i = 0; i < n; i++)
            arr[i] = Console.ReadLine();

        Console.Write("Enter Designation to search: ");
        string desig = Console.ReadLine();

        string[] result = UserProgramCode.getEmployee(arr, desig);

        if (result.Length == 1 &&
            (result[0] == "Invalid Input" ||
             result[0].StartsWith("No employee") ||
             result[0].StartsWith("All employees")))
        {
            Console.WriteLine(result[0]);
        }
        else
        {
            foreach (string s in result)
                Console.Write(s + " ");
        }
    }
}
