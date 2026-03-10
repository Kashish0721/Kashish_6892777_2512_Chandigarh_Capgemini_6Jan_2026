using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter string: ");
        string str = Console.ReadLine();

        Console.Write("Character to replace: ");
        char oldChar = Convert.ToChar(Console.ReadLine());

        Console.Write("Replace with: ");
        char newChar = Convert.ToChar(Console.ReadLine());

        int index = str.IndexOf(oldChar);

        if (index != -1)
        {
            str = str.Remove(index, 1)
                     .Insert(index, newChar.ToString());
        }

        Console.WriteLine(str);
    }
}
