using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter file name: ");
        string file = Console.ReadLine();

        string ext = Logic.GetExtension(file);
        Console.WriteLine("Output: " + ext);
    }
}
