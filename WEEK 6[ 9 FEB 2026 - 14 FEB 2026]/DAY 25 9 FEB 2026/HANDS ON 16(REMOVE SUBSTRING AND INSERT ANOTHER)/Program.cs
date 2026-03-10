using System;

class Program
{
    static void Main()
    {
        string str = "HelloWorld";

        Console.Write("Substring to remove: ");
        string remove = Console.ReadLine();

        Console.Write("Insert new substring: ");
        string insert = Console.ReadLine();

        int index = str.IndexOf(remove);

        if (index != -1)
        {
            str = str.Remove(index, remove.Length)
                     .Insert(index, insert);
        }

        Console.WriteLine(str);
    }
}
