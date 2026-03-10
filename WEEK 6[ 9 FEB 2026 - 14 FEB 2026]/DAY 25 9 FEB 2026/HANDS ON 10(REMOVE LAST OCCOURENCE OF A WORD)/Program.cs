using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter sentence: ");
        string str = Console.ReadLine();

        Console.Write("Word to remove: ");
        string word = Console.ReadLine();

        int index = str.LastIndexOf(word);

        if (index != -1)
        {
            str = str.Remove(index, word.Length);
        }

        Console.WriteLine("Updated string:");
        Console.WriteLine(str);
    }
}
